using Contract.Event.TrackingEvent;
using Contract.Event.UploadEvent;
using Contract.Utilities;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using RecipeService.Domain.Responses;
using UploadFileProto;
namespace RecipeService.Application.Tags.Commands;
public class CreateTagCommand : IRequest<Result<TagResponse?>>
{
    public string Code { get; set; } = null!;
    public string En { get; set; } = null!;
    public string Vi { get; set; } = null!;
    public string Category { get; set; } = null!;
    public IFormFile TagImage { get; set; } = null!;
    public Guid CurrentAccountId { get; set; }

}
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result<TagResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<CreateTagCommandHandler> _logger;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    public CreateTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus, ILogger<CreateTagCommandHandler> logger, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _logger = logger;
        _grpcUploadFileClient = grpcUploadFileClient;
    }

    public async Task<Result<TagResponse?>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tagLimit = 5;
        var rollbackUrl = new List<string>();
        try
        {
            request.Code = request.Code.ToUpper().Replace(" ", "_");
            var existTag = await _context.Tags.Where(t => t.Code == request.Code || t.Value.En == request.En || t.Value.En == request.Vi || t.Value.Vi == request.Vi || t.Value.Vi == request.En).SingleOrDefaultAsync();
            if (existTag != null)
            {
                return Result<TagResponse?>.Failure(TagError.AlreadyExist, $"Tag code : {request.Code} is already exist");
            }
            if (request.TagImage == null)
            {
                return Result<TagResponse?>.Failure(TagError.AddTagFail, "Tag image is null.");
            }

            var tagImageUrl = await CreateTagImage(request.TagImage);

            if (string.IsNullOrEmpty(tagImageUrl))
            {
                return Result<TagResponse?>.Failure(TagError.AddTagFail, "Upload tag image fail.");
            }

            var activeTagCounts = await _context.Tags.Where(t => (t.Category.ToString() == TagCategory.DishType.ToString()
                                                                || t.Category.ToString() == TagCategory.All.ToString())
                                                                && t.Status.ToString() == TagStatus.Active.ToString()).CountAsync();

            if (request.Category.ToString() == TagCategory.DishType.ToString() && activeTagCounts >= tagLimit)
            {
                return Result<TagResponse?>.Failure(TagError.ExceedLimitDishTypeTag);
            }
            if ((request.Category.ToString() == TagCategory.DishType.ToString()
               || request.Category.ToString() == TagCategory.All.ToString())
                && activeTagCounts >= tagLimit)
            {
                return Result<TagResponse?>.Failure(TagError.ExceedLimitDishTypeTag);
            }

            rollbackUrl.Add(tagImageUrl);

            var tag = new Tag
            {
                Code = request.Code,
                Value = new TagValue { En = request.En, Vi = request.Vi },
                Status = TagStatus.Active,
                Category = Enum.Parse<TagCategory>(request.Category),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageUrl = tagImageUrl,
            };

            _context.Tags.Add(tag);
            await _unitOfWork.SaveChangeAsync();
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = Contract.Constants.ActivityType.CREATE,
                EntityId = tag.Id,
                EntityType = Contract.Constants.ActivityEntityType.TAG,
            });
            return Result<TagResponse?>.Success(new TagResponse
            {
                Id = tag.Id,
                Category = tag.Category.ToString(),
                Code = tag.Code,
                Vi = tag.Value.Vi,
                En = tag.Value.En,
                CreatedAt = tag.CreatedAt,
                ImageUrl = tag.ImageUrl,
                Status = tag.Status.ToString()
            });
        }
        catch (Exception ex)
        {
            await RollBackImageGrpc(rollbackUrl);
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<TagResponse?>.Failure(TagError.AddTagFail, ex.Message);
        }
    }

    private async Task RollBackImageGrpc(List<string>? urls)
    {
        if (urls == null || urls.Count == 0) return;
        await _serviceBus.Publish(new DeleteMultipleFileEvent
        {
            DeleteUrl = urls
        });
    }

    private async Task<string?> CreateTagImage(IFormFile tagImage)
    {
        var streams = await FileUtility.ConvertIFormFileToGrpcFileStreamDTOAsync([tagImage]);
        var requestList = new RepeatedField<GrpcFileStreamDTO>();
        requestList.AddRange(streams);
        var result = "";
        var uploadResponse = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
        {
            FileStreams = { requestList }
        });
        if (uploadResponse.Files.Count != requestList.Count)
        {
            return null;
        }
        result = uploadResponse.Files[0].Url;

        if (string.IsNullOrEmpty(result))
        {
            return null;
        }
        return result;
    }
}
