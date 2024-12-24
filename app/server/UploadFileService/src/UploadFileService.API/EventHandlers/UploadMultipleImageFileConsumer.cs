using Contract.Common;
using Contract.DTOs;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.CloudinaryFiles.Commands;

namespace UploadFileService.API.EventHandlers;
[QueueName("upload-multiple-image-file-event-queue")]
public sealed class UploadMultipleImageFileConsumer : IConsumer<UploadMultipleImageFileEvent>
{
    private readonly ISender _sender;

    public UploadMultipleImageFileConsumer(ISender sender, IApplicationDbContext context, IFileUtility fileUtility)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<UploadMultipleImageFileEvent> context)
    {
        await Console.Out.WriteLineAsync("======================================");
        await Console.Out.WriteLineAsync("UploadFile-service consume the message-queue");
        var fileStreamEvents = context.Message.FileStreamEvents;
        List<IFormFile> formFiles = new List<IFormFile>(fileStreamEvents.Count);

        for (int i = 0; i < fileStreamEvents.Count; i++)
        {
            var fileStreamEvent = fileStreamEvents[i];
            var fileStream = new MemoryStream(fileStreamEvent.Stream);
            IFormFile formFile = new FormFile(fileStream, 0, fileStreamEvent.Stream.Length, fileStreamEvent.FileName, fileStreamEvent.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = fileStreamEvent.ContentType,
            };
            formFiles.Add(formFile);
        }

        var response = await _sender.Send(new CreateMultipleCloudinaryImageFileCommand
        {
            FormFiles = formFiles
        });

        var result = new UploadMultipleImageFileEventResponseDTO();
        result.Files = new List<UploadImageFileEventResponseDTO>(fileStreamEvents.Count);
        response.ThrowIfFailure();
        for (int i = 0; i < response.Value!.Count; i++)
        {
            var fileDTO = new UploadImageFileEventResponseDTO
            {
                PublicId = response.Value[i].PublicId,
                Name = response.Value[i].Name,
                Size = response.Value[i].Size,
                Url = response.Value[i].Url,
            };
            result.Files.Add(fileDTO);
        }
        await Console.Out.WriteLineAsync("UploadFile-service done the request");
        await Console.Out.WriteLineAsync("======================================");
        await context.RespondAsync(result);
    }

}