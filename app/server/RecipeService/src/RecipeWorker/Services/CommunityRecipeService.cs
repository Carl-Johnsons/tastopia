﻿using RecipeWorker.Interfaces;
using Contract.Event.RecipeEvent;
using RecipeWorker.Utilities;
using RecipeProto;
using Newtonsoft.Json;
using RecipeWorker.Constants;
using Contract.Interfaces;
using Contract.Constants;
using Contract.Event.NotificationEvent;
namespace RecipeWorker.Services;

public class CommunityRecipeService : IRecipeService
{
    private readonly IServiceBus _serviceBus;
    private readonly IOffensiveTextCheckerService _textCheckerService;
    private readonly GrpcRecipe.GrpcRecipeClient _grpcRecipeClient;
    private readonly ILogger<CommunityRecipeService> _logger;
    public CommunityRecipeService(GrpcRecipe.GrpcRecipeClient grpcRecipeClient, IServiceBus serviceBus, ILogger<CommunityRecipeService> logger, IOffensiveTextCheckerService textCheckerService)
    {
        _grpcRecipeClient = grpcRecipeClient;
        _serviceBus = serviceBus;
        _logger = logger;
        _textCheckerService = textCheckerService;
    }

    public async Task CheckRecipeAbuse(Guid recipeId)
    {
        try
        {
            var response = await _grpcRecipeClient.GetRecipeDetailsAsync(new GrpcRecipeIdRequest
            {
                RecipeId = recipeId.ToString(),
            });

            if (response == null)
            {
                _logger.LogError("Recipe not found");
                throw new Exception("Recipe not found");
            }
            string text = "";

            text += response.Title + "\n";
            text += response.Description + "\n";
            foreach (var i in response.Ingredients)
            {
                text += i + ", ";
            }
            text += "\n";
            foreach (var s in response.Steps)
            {
                text += s + "\n";
            }

            var result = await _textCheckerService.CheckOffensiveText(text);
            if (string.IsNullOrEmpty(result))
            {
                _logger.LogError("Cannot get text abusive percent.");
                throw new Exception("Cannot get text abusive percent.");
            }
            result = result.Replace("%", "");
            var percent = float.Parse(result);
            _logger.LogInformation("==================================================================");
            _logger.LogInformation("Offensive percent: " + result);

            if (percent < OffensiveTextCheckerConstants.OFFENSIVE_THRESHOLD)
            {
                _logger.LogInformation($"Recipe with id: {recipeId} is valid.");
                return;
            }

            _logger.LogInformation($"Recipe with id: {recipeId} is invalid so delete this recipe.");
            await _serviceBus.Publish(new UpdateRecipeIsActiveEvent
            {
                RecipeId = recipeId,
                IsActive = false,
            });

            await _serviceBus.Publish(new NotifyUserEvent
            {
                PrimaryActors = [
                        new ActorDTO
                        {
                            ActorId = recipeId.ToString(),
                            Type = EntityType.RECIPE
                        }],
                SecondaryActors = [],
                TemplateCode = NotificationTemplateCode.SYSTEM_DISABLE_RECIPE,
                Channels = [NOTIFICATION_CHANNEL.DEFAULT],
                JsonData = JsonConvert.SerializeObject(new
                {
                    redirectUri = $"{CLIENT_URI.MOBILE.NOTIFICATION}"
                }),
                ImageUrl = response.ImageUrl,
                RecipientIds = [Guid.Parse(response.AuthorId)]
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
        }

    }

    public async Task CheckRecipeIngredients()
    {
        await Console.Out.WriteLineAsync();
    }

    public async Task CheckRecipeTags(Guid recipeId, List<string> tagValues)
    {
        var listRecipeTagCodes = new List<string>();
        var listTagValueToBeAdded = new List<string>();

        var response = await _grpcRecipeClient.GetAllTagsAsync(new RecipeProto.GrpcEmpty());
        if (response == null || response.Tags == null)
        {
            _logger.LogError("Tags not found");
            throw new Exception("Tags not found");
        }
        var tagEns = new Dictionary<string, GrpcTagDTO>();
        var tagVis = new Dictionary<string, GrpcTagDTO>();
        var tagRequesteds = new Dictionary<string, GrpcTagDTO>();
        foreach (var t in response.Tags)
        {
            if (t.Status.ToString() == "Active") tagEns[t.En.ToLower()] = t;
            if (t.Status.ToString() == "Active") tagVis[t.Vi.ToLower()] = t;
            if (t.Status.ToString() != "Requested") tagRequesteds[t.En.ToLower()] = t;
        }
        List<string> additionTagValues = new List<string>();

        foreach (var value in tagValues)
        {
            var v = value.ToLower();
            if (tagEns.ContainsKey(v))
            {
                listRecipeTagCodes.Add(tagEns[v].Code);
            }
            else if (tagVis.ContainsKey(v))
            {
                listRecipeTagCodes.Add(tagVis[v].Code);
            }
            else if (tagRequesteds.ContainsKey(v))
            {
                listRecipeTagCodes.Add(tagRequesteds[v].Code);
            }
            else
            {
                additionTagValues.Add(value);
            }
        }
        foreach (var value in additionTagValues)
        {
            if (!SpellUtility.IsCorrectSpell(value))
            {
                await Console.Out.WriteLineAsync($"Tag with value {value} is not valid to be added, so skip this value.");
                continue;
            }
            listTagValueToBeAdded.Add(value);
        }

        if (listRecipeTagCodes.Count != 0)
        {
            _logger.LogInformation("upadte tag");
            await _serviceBus.Publish(new UpdateRecipeTagsEvent
            {
                RecipeId = recipeId,
                TagCodes = listRecipeTagCodes,
            });
        }

        if (listTagValueToBeAdded.Count != 0)
        {
            _logger.LogInformation("Add tag");
            await _serviceBus.Publish(new RequestAddTagsEvent
            {
                RecipeId = recipeId,
                Requests = listTagValueToBeAdded,
            });

        }
    }
}
