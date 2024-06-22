using MediaInAction.TraktService.TraktRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Services;
using MediaInAction.FileService.Orders;
using MediaInAction.PublicWeb.ServiceProviders;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Users;

namespace MediaInAction.PublicWeb.Pages;

[Authorize]
public class TraktModel : AbpPageModel
{
    private readonly ITraktRequestAppService _paymentRequestAppService;
    private readonly IOrderAppService _orderAppService;
    private readonly UserEmbyProvider _userEmbyProvider;
    private readonly UserAddressProvider _userAddressProvider;
    private readonly MediaInActionPublicWebTraktOptions _publicWebTraktOptions;

    public TraktModel(
        ITraktRequestAppService paymentRequestAppService,
        IOrderAppService orderAppService,
        UserEmbyProvider userEmbyProvider,
        UserAddressProvider userAddressProvider,
        IOptions<MediaInActionPublicWebTraktOptions> publicWebTraktOptions)
    {
        _paymentRequestAppService = paymentRequestAppService;
        _userEmbyProvider = userEmbyProvider;
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

        var emby = await _userEmbyProvider.GetEmbyAsync();
        var productItems = ObjectMapper.Map<List<EmbyItemDto>, List<OrderItemCreateDto>>(emby.Items);

        if (model.TotalDiscountPercentage != 0)
        {
            ApplyDiscountPercentageToEmbyItems(productItems, model.TotalDiscountPercentage);
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
            Products = ObjectMapper.Map<List<EmbyItemDto>, List<TraktRequestProductCreationDto>>(emby.Items)
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

    private void ApplyDiscountPercentageToEmbyItems(List<OrderItemCreateDto> productItems, decimal discount)
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