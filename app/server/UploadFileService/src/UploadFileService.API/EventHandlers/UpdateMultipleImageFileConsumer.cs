using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UploadFileDTO;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.Files.Commands;

namespace UploadFileService.API.EventHandlers;
[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_MULTIPLE_IMAGE_FILE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_MULTIPLE_IMAGE_FILE)]
public sealed class UpdateMultipleImageFileConsumer : IConsumer<UpdateMultipleImageFileEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    private readonly IFileUtility _fileUtility;

    public UpdateMultipleImageFileConsumer(ISender sender, IMapper mapper, IFileUtility fileUtility)
    {
        _sender = sender;
        _mapper = mapper;
        _fileUtility = fileUtility;
    }

    public async Task Consume(ConsumeContext<UpdateMultipleImageFileEvent> context)
    {
        var fileStreams = context.Message.FileStreams;
        var deleteUrls = context.Message.DeleteUrls;

        var formFiles = fileStreams != null ? await _fileUtility.ConvertFileStreamDTOToIFormFileAsync(fileStreams) : null;
        var response = await _sender.Send(new UpdateMultipleImageFileCommand
        {
            FormFiles = formFiles,
            DeleteUrls = deleteUrls,
        });

        response.ThrowIfFailure();
        var result = new ListFileDTO();
        if(response != null && response.Value!.Count != 0)
        {
            foreach(var file in response.Value)
            {
                result.Files.Add(_mapper.Map<FileDTO>(file));
            }
        }
        await context.RespondAsync(result);
    }

}