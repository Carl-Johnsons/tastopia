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
using UserProto;

namespace RecipeService.Application.Tags.Commands;

public class UpdateTagCommand : IRequest<Result<Tag?>>
{
    public Guid AccountId { get; set; }
    public Guid TagId { get; set; }
    public string Code { get; set; } = null!;
    public string Value { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Status { get; set; } = null!; 
    public IFormFile? TagImage { get; set; }

}
public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result<Tag?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly ILogger<UpdateTagCommandHandler> _logger;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    public UpdateTagCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus, ILogger<UpdateTagCommandHandler> logger, GrpcUser.GrpcUserClient grpcUserClient, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _logger = logger;
        _grpcUserClient = grpcUserClient;
        _grpcUploadFileClient = grpcUploadFileClient;
    }
    public async Task<Result<Tag?>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var rollbackUrl = new List<string>();
        try
        {
            var accountId = request.AccountId;
            var tagId = request.TagId;
            if (accountId == Guid.Empty || tagId == Guid.Empty)
            {
                return Result<Tag?>.Failure(TagError.NullParameter, "AccountId or TagId is null.");
            }

            var adminResponse = await _grpcUserClient.GetUserDetailAsync(new GrpcAccountIdRequest
            {
                AccountId = accountId.ToString(),
            }, cancellationToken: cancellationToken);

            if (adminResponse == null || !adminResponse.IsAdmin)
            {
                return Result<Tag?>.Failure(TagError.PermissionDeny);
            }

            var tag = _context.Tags.SingleOrDefault(t => t.Id == tagId);
            if (tag == null)
            {
                return Result<Tag?>.Failure(TagError.NotFound, "Not found tag.");
            }

            var existTag = await _context.Tags.Where(t => t.Id != request.TagId && t.Code == request.Code).SingleOrDefaultAsync();
            if (existTag != null)
            {
                return Result<Tag?>.Failure(TagError.AddTagFail, $"Tag code : {request.Code} is already exist");
            }
            var tagImageUrl = tag.ImageUrl;
            if (request.TagImage != null)
            {
                var result = await UpdateTagImage(request.TagImage, tag);
                if (string.IsNullOrEmpty(result))
                {
                    return Result<Tag?>.Failure(TagError.UpdateTagFail, "Upload tag image fail.");
                }
                tagImageUrl = result;
                rollbackUrl.Add(result);
            }

            tag.Code = request.Code;
            tag.Value = request.Value;
            tag.UpdatedAt = DateTime.UtcNow;
            tag.Status = Enum.Parse<TagStatus>(request.Status);
            tag.Category = Enum.Parse<TagCategory>(request.Category);
            tag.ImageUrl = tagImageUrl;
            _context.Tags.Update(tag);
            await _unitOfWork.SaveChangeAsync();
            return Result<Tag?>.Success(tag);

        }
        catch (Exception ex) {
            await RollBackImageGrpc(rollbackUrl);
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result<Tag?>.Failure(TagError.UpdateTagFail, ex.Message);
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
