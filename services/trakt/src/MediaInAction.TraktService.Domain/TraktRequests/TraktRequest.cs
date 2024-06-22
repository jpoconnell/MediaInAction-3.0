using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktRequests
{
    public class TraktRequest : CreationAuditedAggregateRoot<Guid>, ISoftDelete
    {
        [NotNull] public string Command { get; protected set; }
        [NotNull] public string OrderId { get; protected set; }
        public int OrderNo { get; protected set; }
        [CanBeNull] public string BuyerId { get; protected set; }
        public TraktRequestState State { get; protected set; }
        [CanBeNull] public string FailReason { get; protected set; }
        public bool IsDeleted { get; set; }
        public ICollection<TraktRequestProduct> Products { get; } = new List<TraktRequestProduct>();
        private TraktRequest()
        {
        }

        public TraktRequest(Guid id,
            [NotNull] string orderId,
            int orderNo,
            [NotNull] string currency,
            [CanBeNull] string buyerId = null) : base(id)
        {
            OrderId = Check.NotNullOrWhiteSpace(orderId, nameof(orderId), minLength: TraktRequestConsts.MinOrderIdLength, maxLength: TraktRequestConsts.MaxOrderIdLength);
            Command = Check.NotNullOrWhiteSpace(currency, nameof(currency), maxLength: TraktRequestConsts.MaxCurrencyLength);
            BuyerId = buyerId;
            OrderNo = orderNo;
        }

        public virtual void SetAsCompleted()
        {
            if (State == TraktRequestState.Completed)
            {
                return;
            }

            State = TraktRequestState.Completed;
            FailReason = null;

            AddDistributedEvent(new TraktRequestCompletedEto
            {
                TraktRequestId = Id,
                OrderId = OrderId,
                OrderNo = OrderNo,
                BuyerId = BuyerId,
                Currency = Command,
                State = State,
                Products = Products.Select(MapProductToEto).ToList(),
                ExtraProperties = ExtraProperties
            });
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != TraktRequestState.Failed)
            {
                return;
            }

            State = TraktRequestState.Failed;
            FailReason = failReason;

            AddDistributedEvent(new TraktRequestFailedEto
            {
                TraktRequestId = Id,
                OrderId = OrderId,
                OrderNo = OrderNo,
                FailReason = failReason,
                ExtraProperties = ExtraProperties
            });
        }

        private static TraktRequestProductEto MapProductToEto(TraktRequestProduct product)
        {
            return new TraktRequestProductEto
            {
                Code = product.Code,
                Name = product.Name,
                Quantity = product.Quantity,
                ReferenceId = product.ReferenceId,
                TotalPrice = product.TotalPrice,
                UnitPrice = product.UnitPrice,
            };
        }
    }
}