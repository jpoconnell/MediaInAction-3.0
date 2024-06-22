using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;

namespace MediaInAction.FileService.FileRequestsNs;

[Serializable]
public class FileRequestFileCreateDto{
    
    [Required]
    public string Server { get; set; }

    [Required]
    public string FileName { get; set; }
    
    [Required]
    public string Directory { get; set; }
    public int Sequence { get; set; }
    
    public FileRequestFileCreateDto(
        Guid id,
        Guid fileRequestId,
        [NotNull] string server,
        [NotNull] string filename,
        [NotNull] string directory,
        [CanBeNull] string referenceId = null,
        int sequence = 1) 
    {
        Server = Check.NotNullOrEmpty(server, nameof(server), maxLength: 20);
        FileName = Check.NotNullOrEmpty(filename, nameof(filename), maxLength: 250);
        Directory = directory;
        Sequence = sequence;
    }
}
