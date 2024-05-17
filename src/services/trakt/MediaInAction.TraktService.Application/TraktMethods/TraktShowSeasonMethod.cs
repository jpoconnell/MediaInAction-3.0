using System.Threading.Tasks;
using MediaInAction.TraktService.TraktRequests;
using Newtonsoft.Json.Linq;
using TraktNet;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.TraktMethods;

[ExposeServices(typeof(ITraktMethod), typeof(TraktShowSeasonMethod))]
public class TraktShowSeasonMethod : ITraktMethod
{
    private readonly TraktClient _traktClient;
    private readonly TraktRequestDomainService _traktRequestDomainService;
    public string Name => TraktMethodNames.GetShowSeason;

    public TraktShowSeasonMethod(TraktClient traktClient, 
        TraktRequestDomainService traktRequestDomainService)
    {
        _traktClient = traktClient;
        _traktRequestDomainService = traktRequestDomainService;
    }

    public async Task<TraktRequestStartResultDto> StartAsync(TraktRequest traktRequest,
        TraktRequestStartDto input)
    {
    /*
        var totalCheckoutPrice = traktRequest.Products.Sum(s => s.TotalPrice);

        var order = new OrderRequest
        {
            CheckoutTraktIntent = "CAPTURE",
            ApplicationContext = new ApplicationContext
            {
                ReturnUrl = input.ReturnUrl,
                CancelUrl = input.CancelUrl,
            },
            PurchaseUnits = new List<PurchaseUnitRequest>
            {
                new PurchaseUnitRequest
                {
                    AmountWithBreakdown = new AmountWithBreakdown
                    {
                        AmountBreakdown = new AmountBreakdown
                        {
                            ItemTotal = new Money
                            {
                                CurrencyCode = paymentRequest.Currency,
                                Value = totalCheckoutPrice.ToString(
                                    $"{CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator}00")
                            }
                        },
                        CurrencyCode = paymentRequest.Currency,
                        Value = totalCheckoutPrice.ToString(
                            $"{CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator}00"),
                    },
                    Items = paymentRequest.Products.Select(p => new Item
                    {
                        Quantity = p.Quantity.ToString(),
                        Name = p.Name,
                        UnitAmount = new Money
                        {
                            CurrencyCode = paymentRequest.Currency,
                            Value = p.UnitPrice.ToString(
                                $"{CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator}00")
                        }
                    }).ToList(),
                    ReferenceId = paymentRequest.Id.ToString()
                }
            }
        };

        var request = new OrdersCreateRequest();
        request.Prefer("return=representation");
        request.RequestBody(order);

        var result = (await _payPalHttpClient.Execute(request)).Result<Order>();

        return new TraktRequestStartResultDto
        {
            CheckoutLink = result.Links.First(x => x.Rel == "approve").Href
        };
        */
        return null;
    }

    public async Task<TraktRequestDto> CompleteAsync(ITraktRequestRepository paymentRequestRepository, string token)
    {
        /*
        var request = new OrdersCaptureRequest(token);
        request.RequestBody(new OrderActionRequest());

        var order = (await _payPalHttpClient.Execute(request)).Result<Order>();

        var paymentRequestId = Guid.Parse(order.PurchaseUnits.First().ReferenceId);
        return await _paymentRequestDomainService.UpdateTraktRequestStateAsync(paymentRequestId, order.Status,
            order.Id);
            */
        return null;
    }

    public async Task HandleWebhookAsync(string payload)
    {
        // TODO: Find better way to parse.
      
        var jObject = JObject.Parse(payload);

        /*
        var order = jObject["resource"].ToObject<Order>();

        var request = new OrdersGetRequest(order.Id);

        // Ensure order object comes from PayPal
        var response = await _payPalHttpClient.Execute(request);
        order = response.Result<Order>();

        var paymentRequestId = Guid.Parse(order.PurchaseUnits.First().ReferenceId);
        await _paymentRequestDomainService.UpdateTraktRequestStateAsync(paymentRequestId, order.Status, order.Id);
        */
    }
}