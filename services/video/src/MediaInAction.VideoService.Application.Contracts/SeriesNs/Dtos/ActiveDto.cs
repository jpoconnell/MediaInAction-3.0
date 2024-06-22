using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

    public class ActiveDto : EntityDto
    {
        public string SeriesName { get; set; }
        public string PictureUrl { get; set; }
        public int Episodes { get; set; }
    }

