using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMethods;

namespace MediaInAction.TraktService.TraktRequests
{
    public class TraktRequestAppService : TraktServiceAppService, ITraktRequestAppService
    {
        private readonly TraktMethodResolver _traktMethodResolver;
        protected ITraktRequestRepository TraktRequestRepository { get; }

        public TraktRequestAppService(
            ITraktRequestRepository traktRequestRepository,
            TraktMethodResolver traktMethodResolver)
        {
            TraktRequestRepository = traktRequestRepository;
            _traktMethodResolver = traktMethodResolver;
        }

        public virtual async Task<TraktRequestDto> CreateAsync(TraktRequestCreationDto input)
        {
            var traktRequest = new TraktRequest(id: GuidGenerator.Create(), orderId: input.OrderId,
                orderNo: input.OrderNo, currency: input.Currency, buyerId: input.BuyerId);

            /*
            foreach (var traktRequestProduct in input.Products
                         .Select(s => new TraktRequestProduct(
                             GuidGenerator.Create(),
                             traktRequestId: traktRequest.Id,
                             code: s.Code,
                             name: s.Name,
                             unitPrice: s.UnitPrice,
                             quantity: s.Quantity,
                             totalPrice: s.TotalPrice,
                             referenceId: s.ReferenceId)))
            {
                traktRequest.Products.Add(traktRequestProduct);
            }

            await TraktRequestRepository.InsertAsync(traktRequest);

            return ObjectMapper.Map<TraktRequest, TraktRequestDto>(traktRequest);
            */
            return null;
        }

        public virtual async Task<TraktRequestStartResultDto> StartAsync(string traktType, TraktRequestStartDto input)
        {
            TraktRequest traktRequest =
                await TraktRequestRepository.GetAsync(input.TraktRequestId, includeDetails: true);

            var traktService = _traktMethodResolver.Resolve(traktType);
            return await traktService.StartAsync(traktRequest, input);
        }

        public virtual async Task<TraktRequestDto> CompleteAsync(string traktType, TraktRequestCompleteInputDto input)
        {
            var traktService = _traktMethodResolver.Resolve(traktType);

            var traktRequestDto = await traktService.CompleteAsync(TraktRequestRepository, input.Token);
            return traktRequestDto;
        }

        public virtual async Task<bool> HandleWebhookAsync(string traktType, string payload)
        {
            var traktService = _traktMethodResolver.Resolve(traktType);

            await traktService.HandleWebhookAsync(payload);

            // PayPal doesn't accept Http 204 (NoContent) result and tries to execute webhook again.
            // So with following value, API returns Http 200 (OK) result.
            return true;
        }
    }
}