using Contract.Constants;
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

public class UpdateTagCommand : IRequest<Result<TagResponse?>>
{
    public Guid TagId { get; set; }
    public string Code { get; set; } = null!;
    public string En { get; set; } = null!;
    public string Vi { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!;
    public IFormFile? TagImage { get; set; }
    public Guid CurrentAccountId { get; set; }

}
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result<TagResponse?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<UpdateTagCommandHandler> _logger;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;

    public UpdateTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus, ILogger<UpdateTagCommandHandler> logger, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _logger = logger;
        _grpcUploadFileClient = grpcUploadFileClient;
    }
    public async Task<Result<TagResponse?>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tagLimit = 5;

        var rollbackUrl = new List<string>();
        try
        {
            ActivityType activityType = ActivityType.UPDATE;
            request.Code = request.Code.ToUpper().Replace(" ", "_");
            var tagId = request.TagId;
            if (tagId == Guid.Empty)
            {
                return Result<TagResponse?>.Failure(TagError.NullParameter, "TagId is null.");
            }

            var tag = _context.Tags.SingleOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return Result<TagResponse?>.Failure(TagError.NotFound, "Not found tag.");
            }

            var existTag = await _context.Tags.Where(t => t.Id != request.TagId && (t.Code == request.Code || t.Value.En == request.En || t.Value.En == request.Vi || t.Value.Vi == request.Vi || t.Value.Vi == request.En)).SingleOrDefaultAsync();
            if (existTag != null)
            {
                return Result<TagResponse?>.Failure(TagError.AlreadyExist, $"Tag code : {request.Code} is already exist");
            }
            var tagImageUrl = tag.ImageUrl;
            if (request.TagImage != null)
            {
                var result = await UpdateTagImage(request.TagImage, tag);
                if (string.IsNullOrEmpty(result))
                {
                    return Result<TagResponse?>.Failure(TagError.UpdateTagFail, "Upload tag image fail.");
                }
                tagImageUrl = result;
                rollbackUrl.Add(result);
            }

            var requestStatus = Enum.Parse<TagStatus>(request.Status);

            if (tag.Status != requestStatus)
            {
                activityType = requestStatus == TagStatus.Inactive ? ActivityType.DISABLE : ActivityType.RESTORE;
            }

            var activeTagCounts = await _context.Tags.Where(t => (t.Category.ToString() == TagCategory.DishType.ToString()
                                                                || t.Category.ToString() == TagCategory.All.ToString())
                                                                && t.Status.ToString() == TagStatus.Active.ToString()).CountAsync();

            if ((request.Category.ToString() == TagCategory.DishType.ToString()
               || request.Category.ToString() == TagCategory.All.ToString())
                && request.Status == TagStatus.Active.ToString()
                && activeTagCounts >= tagLimit)
            {
                return Result<TagResponse?>.Failure(TagError.ExceedLimitDishTypeTag);
            }

            tag.Code = request.Code;
            tag.Value = new TagValue { En = request.En, Vi = request.Vi };
            tag.UpdatedAt = DateTime.UtcNow;
            tag.Status = Enum.Parse<TagStatus>(request.Status);
            tag.Category = Enum.Parse<TagCategory>(request.Category);
            tag.ImageUrl = tagImageUrl;
            _context.Tags.Update(tag);
            await _unitOfWork.SaveChangeAsync();
            await _serviceBus.Publish(new AddActivityLogEvent
            {
                AccountId = request.CurrentAccountId,
                ActivityType = activityType,
                EntityId = tag.Id,
                EntityType = ActivityEntityType.TAG,
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
            return Result<TagResponse?>.Failure(TagError.UpdateTagFail, ex.Message);
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

    private async Task<string?> UpdateTagImage(IFormFile tagImage, Tag tag)
    {
        var streams = await FileUtility.ConvertIFormFileToGrpcFileStreamDTOAsync([tagImage]);
        var requestList = new RepeatedField<GrpcFileStreamDTO>();
        requestList.AddRange(streams);
        var result = "";
        if (!string.IsNullOrEmpty(tag.ImageUrl))
        {
            var uploadResponse = await _grpcUploadFileClient.UpdateMultipleImageAsync(new GrpcUpdateMultipleImageRequest
            {
                FileStreams = { requestList },
                DeleteUrls = { new RepeatedField<string> { tag.ImageUrl } }
            });
            if (uploadResponse.Files.Count != requestList.Count)
            {
                return null;
            }
            result = uploadResponse.Files[0].Url;
        }
        else
        {
            var uploadResponse = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
            {
                FileStreams = { requestList }
            });
            if (uploadResponse.Files.Count != requestList.Count)
            {
                return null;
            }
            result = uploadResponse.Files[0].Url;
        }
        if (string.IsNullOrEmpty(result))
        {
            return null;
        }
        return result;
    }
}
