using Contract.Common;
using Contract.DTOs;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.CloudinaryFiles.Commands;


namespace UploadFileService.API.EventHandlers;
[QueueName("delete-multiple-file-event-queue")]
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
        var fileIds = context.Message.FileIds;
        await Console.Out.WriteLineAsync(fileIds.Count + "list");
        for (int i = 0; i < fileIds.Count; i++)
        {
            await Console.Out.WriteLineAsync(fileIds[i].ToString());
            await Console.Out.WriteLineAsync("**********************************************************");
        }
        await Console.Out.WriteLineAsync("Going delete multiple image command");
        var response = await _sender.Send(new DeleteMultipleCloudinaryImageFileCommand { FileIds = fileIds });
        var result = new DeleteMultipleFileEventResponseDTO();
        response.ThrowIfFailure();

        result.Result = "Delete files success";
        await Console.Out.WriteLineAsync("UploadFile-service done the request");
        await Console.Out.WriteLineAsync("======================================");
        await context.RespondAsync(result);
    }

}