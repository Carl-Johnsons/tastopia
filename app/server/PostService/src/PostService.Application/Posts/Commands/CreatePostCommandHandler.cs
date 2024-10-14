using Contract.DTOs;
using Contract.Event.UploadEvent;
using Contract.Event.UploadEvent.EventModel;
using MediatR;
using Newtonsoft.Json;
using PostService.Domain.Common;
using PostService.Domain.DTOs;
using PostService.Domain.Errors;
namespace PostService.Application.Posts.Commands;

public class CreatePostCommand : IRequest<Result<Post?>>
{
    public Guid UserId { get; init; }
    public CreatePostDTO CreatePostDTO { get; init; } = null!;
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<Post?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;

    public CreatePostCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result<Post?>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreatePostDTO;

        Post post = new Post
        {
            Content = dto.Content,
            UserId = request.UserId,
        };

        //check file array has element
        if (dto.Files != null && dto.Files.Count > 0)
        {
            var fileStreamEventList = new List<FileStreamEvent>();

            foreach (var file in dto.Files)
            {
                fileStreamEventList.Add(new FileStreamEvent
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    Stream = new BinaryReader(file.OpenReadStream()).ReadBytes((int)file.Length)
                });
            }

            var requestClient = _serviceBus.CreateRequestClient<UploadMultipleFileEvent>();
            var response = requestClient.GetResponse<UploadMultipleFileEventResponseDTO>(new UploadMultipleFileEvent
            {
                FileStreamEvents = fileStreamEventList
            });
            if (response != null)
            {
                post.AttachedFilesURL = JsonConvert.SerializeObject(response.Result.Message.Files);
            }
        }


        _context.Posts.Add(post);

        if (dto.TagIds != null)
        {
            foreach (var tagId in dto.TagIds)
            {
                var tag = _context.Tags
                        .Where(p => p.Id == tagId)
                        .SingleOrDefault();

                if (tag == null)
                {
                    return Result<Post?>.Failure(TagError.NotFound);
                }

                _context.PostTags.Add(new PostTag
                {
                    PostId = post.Id,
                    TagId = tagId
                });
            }
        }


        await _unitOfWork.SaveChangeAsync();

        return Result<Post?>.Success(post);
    }
}
