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
using UploadFileProto;
namespace RecipeService.Application.Tags.Commands;
public class CreateTagCommand : IRequest<Result<Tag?>>
{
    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Category { get; set; } = null!;
    public IFormFile TagImage { get; set; } = null!;
    public Guid CurrentAccountId { get; set; }

}
public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result<Tag?>>
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

    public async Task<Result<Tag?>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var rollbackUrl = new List<string>();
        try
        {
            request.Code = request.Code.ToUpper().Replace(" ", "_");
            var existTag = await _context.Tags.Where(t => t.Code == request.Code).SingleOrDefaultAsync();
            if (existTag != null)
            {
                return Result<Tag?>.Failure(TagError.AlreadyExist, $"Tag code : {request.Code} is already exist");
            }
            if (request.TagImage == null)
            {
                return Result<Tag?>.Failure(TagError.AddTagFail, "Tag image is null.");
            }

            var tagImageUrl = await CreateTagImage(request.TagImage);

            if (string.IsNullOrEmpty(tagImageUrl))
            {
                return Result<Tag?>.Failure(TagError.AddTagFail, "Upload tag image fail.");
            }

            rollbackUrl.Add(tagImageUrl);

            var tag = new Tag
            {
                Code = request.Code,
                Value = request.Value,
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
            return Result<Tag?>.Success(tag);
        }
        catch (Exception ex)
        {
            await RollBackImageGrpc(rollbackUrl);
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<Tag?>.Failure(TagError.AddTagFail, ex.Message);
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
