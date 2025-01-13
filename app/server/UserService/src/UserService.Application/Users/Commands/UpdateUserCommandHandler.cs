using Contract.Utilities;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using UploadFileProto;
using UserService.Domain.Errors;

namespace UserService.Application.Users.Commands;

public record UpdateUserCommand : IRequest<Result>
{
    [Required]
    public Guid AccountId { get; set; }
    public string? DisplayName { get; set; } = null!;
    public string? Bio { get; set; } = null!;
    public string? Gender { get; set; } = null!;
    public IFormFile? Avatar { get; set; } = null!;
    public IFormFile? Background { get; set; } = null!;
}
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;

    public UpdateUserCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _grpcUploadFileClient = grpcUploadFileClient;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.AccountId == request.AccountId, cancellationToken);
        if (user == null)
        {
            return Result.Failure(UserError.NotFound);
        }

        if (request.DisplayName != null)
        {
            user.DisplayName = request.DisplayName;
        }

        if (request.Bio != null)
        {
            user.Bio = request.Bio;
        }

        if (request.Gender != null)
        {
            if (Enum.IsDefined(typeof(GenderType), request.Gender))
            {
                user.Gender = request.Gender;
            }
            else
            {
                return Result.Failure(UserError.NullParameters, "Gender must be MALE or FEMALE");
            }

        }
        var imagesList = new List<IFormFile>();
        var deleteUrls = new List<string>();

        if (request.Avatar != null)
        {
            imagesList.Add(request.Avatar);
            deleteUrls.Add(user.AvatarUrl);
        }

        if (request.Background != null)
        {
            imagesList.Add(request.Background);
            deleteUrls.Add(user.BackgroundUrl);
        }

        var grpcList = await FileUtility.ConvertIFormFileToGrpcFileStreamDTOAsync(imagesList);
        var repeatedField = new RepeatedField<GrpcFileStreamDTO>();
        repeatedField.AddRange(grpcList);

        if (imagesList.Count > 0)
        {
            var response = await _grpcUploadFileClient.UpdateMultipleImageAsync(new GrpcUpdateMultipleImageRequest
            {
                FileStreams = { repeatedField },
                DeleteUrls = { deleteUrls }
            }, cancellationToken: cancellationToken);

            if (response == null)
            {
                return Result.Failure(UserError.NullParameters, "Upload response is null");
            }

            if (request.Avatar != null && response.Files.ElementAtOrDefault(0) != null)
            {
                user.AvatarUrl = response.Files[0].Url;
            }

            if (request.Background != null && response.Files.Count > 0)
            {
                int backgroundIndex = request.Avatar != null ? 1 : 0;
                if (response.Files.ElementAtOrDefault(backgroundIndex) != null)
                {
                    user.BackgroundUrl = response.Files[backgroundIndex].Url;
                }
            }
        }

        _context.Users.Update(user);
        await _unitOfWork.SaveChangeAsync();

        return Result.Success();
    }
}
