using Contract.Constants;
using Contract.Event.UploadEvent;
using MassTransit;
using UploadFileService.Application.CloudinaryFiles.Commands;


namespace UploadFileService.API.EventHandlers;
[QueueName(RabbitMQConstant.QUEUE.NAME.DELETE_MULTIPLE_IMAGE_FILE,
exchangeName: RabbitMQConstant.EXCHANGE.NAME.DELETE_MULTIPLE_IMAGE_FILE)]
public sealed class DeleteMultipleFileConsumer : IConsumer<DeleteMultipleFileEvent>
{
    private readonly ISender _sender;

    public DeleteMultipleFileConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<DeleteMultipleFileEvent> context)
    {
        var response = await _sender.Send(new DeleteMultipleImageFileCommand
        {
            DeleteUrls = context.Message.DeleteUrl 
        });
        response.ThrowIfFailure();
    }

}