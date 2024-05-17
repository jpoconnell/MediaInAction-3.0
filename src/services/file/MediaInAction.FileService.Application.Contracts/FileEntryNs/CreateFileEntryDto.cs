using System.ComponentModel.DataAnnotations;
using MediaInAction.FileService.FileEntriesNs;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileEntryNs
{
    public class CreateFileEntryDto
    {
        [Required]
        [StringLength(FileEntryConsts.MaxFileNameLength)]
        public string Server { get; set; }

        [Required]
        [StringLength(FileEntryConsts.MaxFileNameLength)]
        public string Filename { get; set; }
        
        public string Directory { get; set; }
        public string Extn { get; set; }
        public ListType ListName { get; set; }
        
        public long Size { get; set; }
        public int Sequence  { get; set; }
        
    }
}