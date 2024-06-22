using System;
using System.Threading.Tasks;
using MediaInAction.FileService.Orders;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.FileService.FileRequestsNs;

public class FileRequestEventHandler : IDistributedEventHandler<FileRequestCompletedEto>,
    ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    //private readonly SeriesManager _seriesManager;

    public FileRequestEventHandler(IDistributedEventBus eventBus)
    {
        _eventBus = eventBus;
        //_seriesManager = seriesManager;
    }

    public async Task HandleEventAsync(FileRequestCompletedEto eventData)
    {
        if (!Guid.TryParse(eventData.FileRequestId, out var requestId))
        {
            throw new BusinessException(FileServiceErrorCodes.OrderIdIdNotGuid);
        }
/*
        var acceptedOrder = await _seriesManager.AcceptOrderAsync(
            requestId, eventData.FileRequestId, eventData.State.ToString()
        );

        await _eventBus.PublishAsync(new OrderAcceptedEto
        {
            Items = eventData.Products.Select(MapProductToOrderItem).ToList(),
            TraktStatus = acceptedOrder.TraktStatus,
            Buyer = new BuyerEto
            {
                BuyerId = acceptedOrder.Buyer.Id,
                BuyerEmail = acceptedOrder.Buyer.Email,
                BuyerName = acceptedOrder.Buyer.Name
            },
            OrderId = acceptedOrder.Id
        });
        */
    }

    private static OrderItemEto MapProductToOrderItem(FileRequestFileEto arg)
    {
        return new OrderItemEto
        {
           // Units = arg.Quantity,
            ProductId = Guid.Parse(arg.ReferenceId)
        };
    }
}