using Contract.Common;
using Contract.DTOs;
using Contract.Event.UploadEvent;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using UploadFileService.Application.CloudinaryFiles.Commands;


namespace UploadFileService.API.EventHandlers;
[QueueName("upload-multiple-file-event-queue")]
public sealed class UploadMultipleFileConsumer : IConsumer<UploadMultipleFileEvent>
{
    private readonly ISender _sender;
    private readonly IApplicationDbContext _context;
    private readonly IFileUtility _fileUtility;

    public UploadMultipleFileConsumer(ISender sender, IApplicationDbContext context, IFileUtility fileUtility)
    {
        _sender = sender;
        _context = context;
        _fileUtility = fileUtility;
    }

    public async Task Consume(ConsumeContext<UploadMultipleFileEvent> context)
    {
        await Console.Out.WriteLineAsync("======================================");
        await Console.Out.WriteLineAsync("UploadFile-service consume the message-queue");
        var fileStreamEvents = context.Message.FileStreamEvents;
        List<IFormFile> formFiles = new List<IFormFile>(fileStreamEvents.Count);

        await Console.Out.WriteLineAsync(fileStreamEvents.Count + "list" + fileStreamEvents.Count);
        for (int i = 0; i < fileStreamEvents.Count; i++)
        {
            await Console.Out.WriteLineAsync(fileStreamEvents[i].FileName);
            await Console.Out.WriteLineAsync(fileStreamEvents[i].ContentType);
            await Console.Out.WriteLineAsync(fileStreamEvents[i].Stream.Length + "");
            await Console.Out.WriteLineAsync("**********************************************************");
        }

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

        await Console.Out.WriteLineAsync("FormFile element count:" + formFiles.Count);
        await Console.Out.WriteLineAsync("Going create multiple image command");
        var response = await _sender.Send(new CreateMultipleCloudinaryImageFileCommand
        {
            FormFiles = formFiles
        });

        var result = new UploadMultipleFileEventResponseDTO();
        result.Files = new List<UploadFileEventResponseDTO>(fileStreamEvents.Count);
        response.ThrowIfFailure();
        for (int i = 0; i < response.Value!.Count; i++)
        {
            await Console.Out.WriteLineAsync("extension tple id of :" + i + " " + response.Value[i].ExtensionTypeId);

            var extensionTypeCode = (await _context.ExtensionTypes.Where(et => et.Id == response.Value[i].ExtensionTypeId).SingleOrDefaultAsync())?.Code!;
            var fileDTO = new UploadFileEventResponseDTO
            {
                Id = response.Value[i].Id,
                Name = response.Value[i].Name,
                Size = response.Value[i].Size,
                Url = response.Value[i].Url,
                ExtensionTypeCode = extensionTypeCode,
                FileType = _fileUtility.getFileType(response.Value[i].Name)
            };
            result.Files.Add(fileDTO);
        }

        await Console.Out.WriteLineAsync("$$$$$$$$$$$$$$$$$$$$$$$$result:" + result.Files.Count);

        for (int i = 0; i < response.Value.Count; i++)
        {
            await Console.Out.WriteLineAsync("Result:" + i);
            await Console.Out.WriteLineAsync("id: " + result.Files[i].Id);
            await Console.Out.WriteLineAsync("name: " + result.Files[i].Name);
            await Console.Out.WriteLineAsync("size: " + result.Files[i].Size);
            await Console.Out.WriteLineAsync("url: " + result.Files[i].Url);
            await Console.Out.WriteLineAsync("etCode: " + result.Files[i].ExtensionTypeCode);
        }

        await Console.Out.WriteLineAsync("UploadFile-service done the request");
        await Console.Out.WriteLineAsync("======================================");
        await context.RespondAsync(result);
    }

}