using AutoMapper;
using Contract.Constants;
using Contract.DTOs.UploadFileDTO;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.API.Utilities;
using UploadFileService.Application.CloudinaryFiles.Commands;

namespace UploadFileService.API.EventHandlers;
[QueueName(RabbitMQConstant.QUEUE.NAME.UPDATE_MULTIPLE_IMAGE_FILE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.UPDATE_MULTIPLE_IMAGE_FILE)]
public sealed class UpdateMultipleImageFileConsumer : IConsumer<UpdateMultipleImageFileEvent>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UpdateMultipleImageFileConsumer(ISender sender, IApplicationDbContext context, IFileUtility fileUtility, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<UpdateMultipleImageFileEvent> context)
    {
        var fileStreams = context.Message.FileStreams;
        var deleteUrls = context.Message.DeleteUrls;

        var formFiles = fileStreams != null ? await FileUtility.ConvertFileStreamDTOToIFormFileAsync(fileStreams) : null;
        var response = await _sender.Send(new UpdateMultipleImageFileCommand
        {
            FormFiles = formFiles,
            Urls = deleteUrls,
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