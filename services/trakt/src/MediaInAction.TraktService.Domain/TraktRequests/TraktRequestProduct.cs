using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktRequests
{
    public class TraktRequestProduct : Entity<Guid>
    {
        public Guid TraktRequestId { get; private set; }
        public string ReferenceId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalPrice { get; private set; }

        public TraktRequestProduct(
            Guid id,
            Guid paymentRequestId,
            [NotNull] string code,
            [NotNull] string name,
            decimal unitPrice,
            int quantity,
            decimal totalPrice,
            [CanBeNull] string referenceId = null) : base(id)
        {
            TraktRequestId = paymentRequestId;
            Code = Check.NotNullOrEmpty(code, nameof(code), maxLength: TraktRequestConsts.MaxCodeLength);
            Name = Check.NotNullOrEmpty(name, nameof(name), maxLength: TraktRequestConsts.MaxNameLength);
            UnitPrice = unitPrice;
            Quantity = quantity;
            TotalPrice = totalPrice;
            ReferenceId = referenceId;
        }
    }
}