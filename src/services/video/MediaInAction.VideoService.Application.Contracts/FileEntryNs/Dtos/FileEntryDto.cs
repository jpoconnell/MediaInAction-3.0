using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

    public class FileEntryDto : EntityDto<Guid>
    { 
        public string Server { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public string CleanFileName { get; set; }
        public string Resolution { get; set; }
        public string Extn { get; set; }
        public long Size  { get; set; }
        public Guid Link { get; set; }
        public Guid SeriesLink { get; set; }
        public ListType ListName { get; set; }
        public int Sequence { get; set; }
        public MediaType MediaType { get; set; }
        public FileStatus FileStatus { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsMapped { get; set; }
        public int Updates { get; set; }
    }