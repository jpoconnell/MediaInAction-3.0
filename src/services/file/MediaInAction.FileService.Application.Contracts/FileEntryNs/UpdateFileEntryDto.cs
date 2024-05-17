using System.ComponentModel.DataAnnotations;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.FileService.FileEntryNs
{
    public class UpdateFileEntryDto
    {
        [Required]
        public string Server { get; set; }

        public string Filename { get; set; }

        public string Directory { get; set; }

        public ListType ListName { get; set; }
    }
}