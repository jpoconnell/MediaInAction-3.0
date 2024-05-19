using MediaInAction.TraktService.TraktRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.BasketService.Services;
using MediaInAction.VideoService.Orders;
using MediaInAction.PublicWeb.ServiceProviders;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Users;

namespace MediaInAction.PublicWeb.Pages;

[Authorize]
public class TraktModel : AbpPageModel
{
    private readonly ITraktRequestAppService _paymentRequestAppService;
    private readonly IOrderAppService _orderAppService;
    private readonly UserBasketProvider _userBasketProvider;
    private readonly UserAddressProvider _userAddressProvider;
    private readonly MediaInActionPublicWebTraktOptions _publicWebTraktOptions;

    public TraktModel(
        ITraktRequestAppService paymentRequestAppService,
        IOrderAppService orderAppService,
        UserBasketProvider userBasketProvider,
        UserAddressProvider userAddressProvider,
        IOptions<MediaInActionPublicWebTraktOptions> publicWebTraktOptions)
    {
        _paymentRequestAppService = paymentRequestAppService;
        _userBasketProvider = userBasketProvider;
        _userAddressProvider = userAddressProvider;
        _orderAppService = orderAppService;
        _publicWebTraktOptions = publicWebTraktOptions.Value;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(TraktPageViewModel model)
    {
        Logger.LogInformation("Trakt Proceeded...");
        Logger.LogInformation($"AddressId: {model.SelectedAddressId}");
        Logger.LogInformation($"TraktMethod: {model.SelectedTraktMethod}");
        Logger.LogInformation($"Total Discount: {model.TotalDiscountPercentage}");

        var basket = await _userBasketProvider.GetBasketAsync();
        var productItems = ObjectMapper.Map<List<BasketItemDto>, List<OrderItemCreateDto>>(basket.Items);

        if (model.TotalDiscountPercentage != 0)
        {
            ApplyDiscountPercentageToBasketItems(productItems, model.TotalDiscountPercentage);
        }

        var placedOrder = await _orderAppService.CreateAsync(new OrderCreateDto()
        {
            TraktMethod = model.SelectedTraktMethod,
            Address = GetUserAddress(model.SelectedAddressId),
            Products = productItems
        });

        var paymentRequest = await _paymentRequestAppService.CreateAsync(new TraktRequestCreationDto
        {
            OrderId = placedOrder.Id.ToString(),
            OrderNo = placedOrder.OrderNo,
            BuyerId = CurrentUser.GetId().ToString(),
            Currency = MediaInActionTraktConsts.Currency,
            Products = ObjectMapper.Map<List<BasketItemDto>, List<TraktRequestProductCreationDto>>(basket.Items)
        });

        var response = await _paymentRequestAppService.StartAsync(
            model.SelectedTraktMethod,
            new TraktRequestStartDto
            {
                TraktRequestId = paymentRequest.Id,
                ReturnUrl = _publicWebTraktOptions.TraktSuccessfulCallbackUrl,
                CancelUrl = _publicWebTraktOptions.TraktFailureCallbackUrl
            });

        return Redirect(response.CheckoutLink);
    }

    public class TraktPageViewModel
    {
        public int SelectedAddressId { get; set; }
        public string SelectedTraktMethod { get; set; }
        public decimal TotalDiscountPercentage { get; set; }
    }

    private void ApplyDiscountPercentageToBasketItems(List<OrderItemCreateDto> productItems, decimal discount)
    {
        for (int i = 0; i < productItems.Count; i++)
        {
            productItems[i].Discount = discount;
        }
    }

    private OrderAddressDto GetUserAddress(int selectedAddressId)
    {
        var address = _userAddressProvider.GetDemoAddresses().First(q => q.Id == selectedAddressId);
        return new OrderAddressDto
        {
            Description = address.Type,
            City = address.City,
            Country = address.Country,
            Street = address.Street,
            ZipCode = address.ZipCode
        };
    }
}