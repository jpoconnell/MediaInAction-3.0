using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.FileService.FileRequestNs;

[Serializable]
public class FileRequestFile : Entity<Guid>
{
    public string ReferenceId { get; set; }

    public Guid FileRequestId { get; private set; }
    
    [Required]
    public string Server { get; set; }

    [Required]
    public string FileName { get; set; }
    
    [Required]
    public string Directory { get; set; }
    public int Sequence { get; set; }
    
    public FileRequestFile(
        Guid id,
        Guid fileRequestId,
        [NotNull] string server,
        [NotNull] string filename,
        [NotNull] string directory,
        [CanBeNull] string referenceId = null,
        int sequence = 1) : base(id)
    {
        FileRequestId = fileRequestId;
        Server = Check.NotNullOrEmpty(server, nameof(server), maxLength: 20);
        FileName = Check.NotNullOrEmpty(filename, nameof(filename), maxLength: 250);

        ReferenceId = referenceId;
        Directory = directory;
        Sequence = sequence;
    }
}
