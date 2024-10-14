using PostService.Domain.Entities;
using PostService.Infrastructure.Persistence.Mockup.Data;

namespace PostService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedTagData()
    {
        await Console.Out.WriteLineAsync("=================Begin seeding tag data=================");
        foreach (var tag in TagData.Data)
        {
            var id = tag.Id;
            var value = tag.Value;

            var isTagExisteed = _context.Tags
                                        .Where(t => t.Id == id || t.Value == value)
                                        .SingleOrDefault() != null;

            if (isTagExisteed)
            {
                await Console.Out.WriteLineAsync($"Tag {value} is already exited");
                continue;
            }

            _context.Tags.Add(tag);
            await Console.Out.WriteLineAsync($"Added Tag {value}");
        }
        await _unitOfWork.SaveChangeAsync();
        await Console.Out.WriteLineAsync("=================DONE seeding add data=================");
    }

    public async Task SeedInteractionData()
    {
        await Console.Out.WriteLineAsync("=================Begin seeding interaction data=================");
        foreach (var interaction in InteractionData.Data)
        {
            var id = interaction.Id;
            var code = interaction.Code;

            var isInteractionExited = _context.Interactions
                                        .Where(i => i.Id == id || i.Code == code)
                                        .SingleOrDefault() != null;

            if (isInteractionExited)
            {
                await Console.Out.WriteLineAsync($"Interaction {code} is already exited");
                continue;
            }

            _context.Interactions.Add(interaction);
            await Console.Out.WriteLineAsync($"Added Tag {code}");
            await _unitOfWork.SaveChangeAsync();
        }
        await Console.Out.WriteLineAsync("=================DONE seeding add data=================");
    }

    public async Task SeedPostData()
    {
        await Console.Out.WriteLineAsync("=================Begin seeding post data=================");
        foreach (var post in PostData.Data)
        {
            var id = Guid.Parse(post.Id);

            var isPostExited = _context.Posts
                                .Where(p => p.Id == id)
                                .SingleOrDefault() != null;
            if (isPostExited)
            {
                await Console.Out.WriteLineAsync($"Post {id} is already exited");
                continue;
            }

            Post post1 = new Post
            {
                Id = id,
                Content = post.Content,
                UserId = Guid.Parse(post.UserId)
            };
            _context.Posts.Add(post1);

            foreach (var postTag in post.tagIDs)
            {
                PostTag tag = new PostTag
                {
                    PostId = Guid.Parse(post.Id),
                    TagId = Guid.Parse(postTag)
                };
                _context.PostTags.Add(tag);
            };

            await Console.Out.WriteLineAsync($"Added Post {id}");
        }
        await _unitOfWork.SaveChangeAsync();
        await Console.Out.WriteLineAsync("=================DONE seeding add data=================");
    }


    public async Task SeedCommentData()
    {
        await Console.Out.WriteLineAsync("=================Begin seeding comment data=================");
        foreach (var comment in CommentData.Data)
        {
            var id = Guid.Parse(comment.Id);

            var isCommentExited = _context.Comments
                                .Where(p => p.Id == id)
                                .SingleOrDefault() != null;

            if (isCommentExited)
            {
                await Console.Out.WriteLineAsync($"Comment {id} is already exited");
                continue;
            }

            Comment comment1 = new Comment
            {
                Id = id,
                Content = comment.Content,
                UserId = Guid.Parse(comment.UserId)
            };
            _context.Comments.Add(comment1);

            PostComment comment2 = new PostComment
            {
                CommentId = id,
                PostId = Guid.Parse(comment.PostId),
            };
            _context.PostComments.Add(comment2);

            await Console.Out.WriteLineAsync($"Added Comment {id}");
        }
        await _unitOfWork.SaveChangeAsync();
        await Console.Out.WriteLineAsync("=================DONE seeding add data=================");
    }

    public async Task SeedPostInteractData()
    {
        await Console.Out.WriteLineAsync("=================Begin seeding post interact data=================");
        foreach (var interact in PostInteractData.Data)
        {
            var userId = Guid.Parse(interact.UserId);
            var postId = Guid.Parse(interact.PostId);

            var isInteractExited = _context.PostInteracts
                                .Where(p => p.PostId == postId && p.UserId == userId)
                                .SingleOrDefault() != null;

            if (isInteractExited)
            {
                await Console.Out.WriteLineAsync($"User {userId} is already interacted post {postId}");
                continue;
            }

            PostInteract interaction = new PostInteract
            {
                UserId = userId,
                PostId = postId,
                InteractionId = Guid.Parse(interact.InteractionId)
            };
            _context.PostInteracts.Add(interaction);
            await Console.Out.WriteLineAsync($"Added User {userId} interact post {postId}");
        }
        await _unitOfWork.SaveChangeAsync();
        await Console.Out.WriteLineAsync("=================DONE seeding add data=================");
    }

    public async Task SeedContentRestrictionsTypeData()
    {
        if (_context.UserContentRestrictionsTypes.Any())
        {
            return;
        }

        await Console.Out.WriteLineAsync("=================Begin seeding Content Restrictions Type data=================");
        foreach (var action in UserContentRestrictionsTypeMockup.Data)
        {
            _context.UserContentRestrictionsTypes.Add(new Domain.Entities.UserContentRestrictionsType
            {
                Code = action.Code,
            });
        }
        await _unitOfWork.SaveChangeAsync();
        await Console.Out.WriteLineAsync("=================Done seeding Content Restrictions Type data=================");
    }
}
