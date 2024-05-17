using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.FileEntryNs.Dtos;

    public class FileEntryCreatedDto 
    { 
        public string FileId { get; set; }
        public string Server { get; set; }
        public string Directory { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
        public string Resolution { get; set; }
        public string Extn { get; set; }
        public ListType ListName { get; set; }
        public int Sequence { get; set; }
        public MediaType MediaType { get; set; }
        public string ErrorMessage { get; set; }
        
        public FileStatus FileStatus { get; set; }
        
        public FileEntryStatus FileEntryStatus { get; set; }
        public int Updates { get; set; }
    }