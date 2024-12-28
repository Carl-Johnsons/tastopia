using Contract.Common;
using Contract.DTOs;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.CloudinaryFiles.Commands;


namespace UploadFileService.API.EventHandlers;
[QueueName("delete-multiple-file-event")]
public sealed class DeleteMultipleFileConsumer : IConsumer<DeleteMultipleFileEvent>
{
    private readonly ISender _sender;

    public DeleteMultipleFileConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<DeleteMultipleFileEvent> context)
    {
        await Console.Out.WriteLineAsync("======================================");
        await Console.Out.WriteLineAsync("UploadFile-service consume the message-queue");
        var response = await _sender.Send(new DeleteMultipleCloudinaryImageFileCommand { DeleteUrls = context.Message.DeleteUrl });
        var result = new DeleteMultipleFileEventResponseDTO();
        response.ThrowIfFailure();
        result.Result = "Delete files success";
        await Console.Out.WriteLineAsync("UploadFile-service done the request");
        await Console.Out.WriteLineAsync("======================================");
        await context.RespondAsync(result);
    }

}