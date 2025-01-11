using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UploadFileDTO;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.API.Utilities;
using UploadFileService.Application.CloudinaryFiles.Commands;

namespace UploadFileService.API.EventHandlers;
[QueueName(RabbitMQConstant.QUEUE.NAME.UPLOAD_MULTIPLE_IMAGE_FILE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPLOAD_MULTIPLE_IMAGE_FILE)]
public sealed class UploadMultipleImageFileConsumer : IConsumer<UploadMultipleImageFileEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UploadMultipleImageFileConsumer(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UploadMultipleImageFileEvent> context)
    {
        var fileStreams = context.Message.FileStreams;
        var formFiles = await FileUtility.ConvertFileStreamDTOToIFormFileAsync(fileStreams);
        var response = await _sender.Send(new CreateMultipleImageFileCommand
        {
            FormFiles = formFiles
        });
        response.ThrowIfFailure();
        var result = new ListFileDTO();
        foreach(var file in response.Value!) {
            result.Files.Add(_mapper.Map<FileDTO>(file));
        }
        await context.RespondAsync(result);
    }

}