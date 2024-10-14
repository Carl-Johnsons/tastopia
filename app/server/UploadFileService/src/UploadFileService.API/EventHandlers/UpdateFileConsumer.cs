using Contract.Common;
using Contract.DTOs;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.CloudinaryFiles.Commands;


namespace UploadFileService.API.EventHandlers;
[QueueName("update-file-event-queue")]
public sealed class UpdateFileConsumer : IConsumer<UpdateFileEvent>
{
    private readonly ISender _sender;

    public UpdateFileConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UpdateFileEvent> context)
    {
        await Console.Out.WriteLineAsync("======================================");
        await Console.Out.WriteLineAsync("UploadFile-service consume the message-queue");
        var fileStreamEvent = context.Message.FileStreamEvent;
        var url = context.Message.Url;
        if (fileStreamEvent == null || fileStreamEvent.Stream.Length == 0) throw new Exception("File stream is null to update");
       
        var fileStream = new MemoryStream(fileStreamEvent.Stream);
        IFormFile formFile = new FormFile(fileStream, 0, fileStreamEvent.Stream.Length, fileStreamEvent.FileName, fileStreamEvent.FileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = fileStreamEvent.ContentType,
        };
           
        await Console.Out.WriteLineAsync("Going update image command");
        var response = await _sender.Send(new UpdateCloudinaryImageFileCommand
        {
            FormFile = formFile,
            Url = url,
        });
        var result = new UploadFileEventResponseDTO();
        response.ThrowIfFailure();
        result.Name = response.Value!.Name;
        result.ExtensionTypeCode = response.Value.ExtensionType.Code;
        result.FileType = response.Value.ExtensionType.Type;
        result.Size = response.Value.Size;
        result.Url = response.Value.Url;
        await context.RespondAsync(result);
    }

}