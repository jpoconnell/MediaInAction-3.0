using Volo.Abp.Application.Dtos;

namespace MediaInAction.FileService.Orders;

public class TraktDto : EntityDto
{
    public decimal RateOfTraktMethod { get; set; }
    public string TraktMethod { get; set; }
}