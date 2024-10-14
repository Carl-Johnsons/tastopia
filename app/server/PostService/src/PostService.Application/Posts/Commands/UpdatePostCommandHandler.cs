using MediatR;
using Microsoft.EntityFrameworkCore;
using PostService.Domain.Common;
using PostService.Domain.Errors;
using System.Diagnostics;
using System.Linq;
namespace PostService.Application.Posts.Commands;

public class UpdatePostCommand : IRequest<Result>
{
    public Guid PostId { get; init; }
    public string? Content { get; init; } = null!;
    public bool? Active { get; init; }
    public List<Guid>? TagIds { get; init; } = null!;
}

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePostCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _context.Posts
                        .Where(p => p.Id == request.PostId)
                        .SingleOrDefault();

        if (post == null)
        {
            return Result.Failure(PostError.NotFound);
        }
        if (request.Content != null) post.Content = request.Content;
        if (request.Active != null) post.Active = (bool)request.Active;

        _context.Posts.Update(post);
        await Console.Out.WriteLineAsync("============================ Update tag");
        if (request.TagIds != null)
        {
            var postTagIds = await _context.PostTags
                                  .Where(pt => pt.PostId == request.PostId)
                                  .Select(pt => pt.TagId)
                                  .ToArrayAsync();
            if (postTagIds != null)
            {
                var originalTagIdsSet = new HashSet<Guid>(postTagIds);
                var updateTagIdsSet = new HashSet<Guid>(request.TagIds);

                await Console.Out.WriteLineAsync("Original tag ids count: " + originalTagIdsSet.Count);
                await Console.Out.WriteLineAsync("Update tag ids count: " + updateTagIdsSet.Count);
                // Do nothing with exist tag
                foreach (var postTagId in postTagIds)
                {
                    if (updateTagIdsSet.Contains(postTagId))
                    {
                        updateTagIdsSet.Remove(postTagId);
                        originalTagIdsSet.Remove(postTagId);
                    }
                }
                await Console.Out.WriteLineAsync("Original tag ids count: " + originalTagIdsSet.Count);
                await Console.Out.WriteLineAsync("Update tag ids count: " + updateTagIdsSet.Count);
                // Remove tag if the update tag list didn't have it
                foreach (var originalTagId in originalTagIdsSet)
                {
                    var deleteTags = await _context.PostTags
                                            .Where(pt => pt.PostId == request.PostId && pt.TagId == originalTagId)
                                            .SingleOrDefaultAsync();
                    _context.PostTags.Remove(deleteTags!);
                }
                await Console.Out.WriteLineAsync("Original tag ids count: " + originalTagIdsSet.Count);
                await Console.Out.WriteLineAsync("Update tag ids count: " + updateTagIdsSet.Count);
                // Add new tag
                foreach (var remainUpdateTagIds in updateTagIdsSet)
                {
                    _context.PostTags.Add(new PostTag
                    {
                        PostId = request.PostId,
                        TagId = remainUpdateTagIds
                    });
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Add new tags: ");
                foreach (var remainUpdateTagIds in request.TagIds)
                {
                    _context.PostTags.Add(new PostTag
                    {
                        PostId = request.PostId,
                        TagId = remainUpdateTagIds
                    });
                }
            }
        }

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Success();
    }
}
