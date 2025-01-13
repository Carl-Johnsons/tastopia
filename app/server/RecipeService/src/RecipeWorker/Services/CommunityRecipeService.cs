using RecipeWorker.Interfaces;
using Contract.Event.RecipeEvent;
using Contract.DTOs.RecipeDTO;
using Newtonsoft.Json;
using RecipeWorker.Utilities;

namespace RecipeWorker.Services;

// Ref: https://stackoverflow.com/questions/31209273/how-do-i-set-return-uri-for-googlewebauthorizationbroker-authorizeasync
/*
 * When you deploy this to docker you will get an exception that the operating system does not support running the process. Basically, its trying to open a browser on the Metal Box. which is not possible with docker.
   To solve this. Modify the code to use full absolute path like this:
 * 
 * ```
 * var inputFolderAbsolute = Path.Combine(AppContext.BaseDirectory, "Auth.Store");
    ...
    new FileDataStore(inputFolderAbsolute, true)
 * ```
 * Run this application as a console app on your local machine so the browser opens.
   - Select the account you want to work with
   - In the bin folder, a new folder and file will be created.
   - Copy that folder to the root path
   - Set the file to copy if newer
   - Deploy to docker
   - Because the refresh token is saved for the account you selected it will get a new access token and work.
   - NB: It is possible the refresh token expires to whatever reason. You will have to repeat the steps above
 */


public class CommunityRecipeService : IRecipeService
{
    private readonly IServiceBus _serviceBus;
    public CommunityRecipeService(IServiceBus serviceBus)
    {
        _serviceBus = serviceBus;
    }

    public async Task CheckRecipeAbuse()
    {
        await Console.Out.WriteLineAsync("CheckRecipeAbuse");
    }

    public async Task CheckRecipeIngredients()
    {
        await Console.Out.WriteLineAsync("CheckRecipeIngredients");
    }

    public async Task CheckRecipeTags(Guid recipeId, List<string> tagValues)
    {
        //var listRecipeTagCodes = new List<string>();
        //var listTagValueToBeAdded = new List<string>();

        //var requestClient = _serviceBus.CreateRequestClient<GetAllTagsEvents>();
        //var response = await requestClient.GetResponse<TagListDTO>(new GetAllTagsEvents());
        //if (response == null || response.Message == null)
        //{
        //    throw new Exception("Tags not found");
        //}

        //await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(response.Message, Formatting.Indented));

        //var tags = response.Message.Tags.Where(t => t.Status.ToString() == "Active").ToDictionary(t => t.Value);

        //var tagRequesteds = response.Message.Tags.Where(t => t.Status.ToString() != "Active").ToDictionary(t => t.Value);

        //foreach (var value in tagValues)
        //{
        //    if(!tags.ContainsKey(value))
        //    {
        //        await Console.Out.WriteLineAsync($"Tag with value: {value} is not found, so skip this value.");
        //        continue;
        //    }
        //    listRecipeTagCodes.Add(value);
        //}


        //foreach(var value in additionTagValues)
        //{
        //    if (!SpellUtility.IsCorrectSpell(value))
        //    {
        //        await Console.Out.WriteLineAsync($"Tag with value {value} is not valid to be added, so skip this value.");
        //        continue;
        //    }

        //    var isSkip = false;
        //    await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(tagRequesteds, Formatting.Indented));
        //    foreach (var tagRequested in tagRequesteds)
        //    {
        //        if (tagRequested.Value.Value.ToLower() == value.ToLower()) {
        //            await Console.Out.WriteLineAsync($"Tag with value {value} is requested, so skip this value.");
        //            listRecipeTagCodes.Add(tagRequested.Value.Code);
        //            isSkip = true;
        //            break;
        //        }
        //    }

        //    if(isSkip) continue;

        //    listTagValueToBeAdded.Add(value);
        //}

        //if(listRecipeTagCodes.Count != 0)
        //{
        //    await Console.Out.WriteLineAsync("upadte tag");
        //    await _serviceBus.Publish(new UpdateRecipeTagsEvent
        //    {
        //        RecipeId = recipeId,
        //        TagCodes = listRecipeTagCodes,
        //    });
        //}

        //if(listTagValueToBeAdded.Count != 0)
        //{
        //    await Console.Out.WriteLineAsync("Add tag");
        //    await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(listTagValueToBeAdded, Formatting.Indented));
        //    await _serviceBus.Publish(new RequestAddTagsEvent
        //    {
        //        RecipeId = recipeId,
        //        Requests = listTagValueToBeAdded,
        //    });

        //}



    }

    
}
