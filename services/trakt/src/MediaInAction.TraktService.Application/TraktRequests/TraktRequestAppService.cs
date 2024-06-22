using MediaInAction.TraktService.TraktMethods;
using System.Linq;
using System.Threading.Tasks;

namespace MediaInAction.TraktService.TraktRequests
{
    public class TraktRequestAppService : TraktServiceAppService, ITraktRequestAppService
    {
        private readonly TraktMethodResolver _paymentMethodResolver;
        protected ITraktRequestRepository TraktRequestRepository { get; }

        public TraktRequestAppService(
            ITraktRequestRepository paymentRequestRepository,
            TraktMethodResolver paymentMethodResolver)
        {
            TraktRequestRepository = paymentRequestRepository;
            _paymentMethodResolver = paymentMethodResolver;
        }

        public virtual async Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input)
        {
            var paymentRequest = new TraktRequest(id: GuidGenerator.Create(), orderId: input.OrderId,
                orderNo: input.OrderNo, currency: input.Currency, buyerId: input.BuyerId);

            foreach (var paymentRequestProduct in input.Products
                         .Select(s => new TraktRequestProduct(
                             GuidGenerator.Create(),
                             paymentRequestId: paymentRequest.Id,
                             code: s.Code,
                             name: s.Name,
                             unitPrice: s.UnitPrice,
                             quantity: s.Quantity,
                             totalPrice: s.TotalPrice,
                             referenceId: s.ReferenceId)))
            {
                paymentRequest.Products.Add(paymentRequestProduct);
            }

            await TraktRequestRepository.InsertAsync(paymentRequest);

            return ObjectMapper.Map<TraktRequest, TraktRequestDto>(paymentRequest);
        }

        public virtual async Task<TraktRequestStartResultDto> StartAsync(string paymentType, TraktRequestStartDto input)
        {
            TraktRequest paymentRequest =
                await TraktRequestRepository.GetAsync(input.TraktRequestId, includeDetails: true);

            var paymentService = _paymentMethodResolver.Resolve(paymentType);
            return await paymentService.StartAsync(paymentRequest, input);
        }

        public virtual async Task<TraktRequestDto> CompleteAsync(string paymentType, TraktRequestCompleteInputDto input)
        {
            var paymentService = _paymentMethodResolver.Resolve(paymentType);

            var paymentRequest = await paymentService.CompleteAsync(TraktRequestRepository, input.Token);
            return ObjectMapper.Map<TraktRequest, TraktRequestDto>(paymentRequest);
        }

        public virtual async Task<bool> HandleWebhookAsync(string paymentType, string payload)
        {
            var paymentService = _paymentMethodResolver.Resolve(paymentType);

            await paymentService.HandleWebhookAsync(payload);

            // PayPal doesn't accept Http 204 (NoContent) result and tries to execute webhook again.
            // So with following value, API returns Http 200 (OK) result.
            return true;
        }
    }
}