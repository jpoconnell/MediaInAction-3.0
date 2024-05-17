using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService.EmbyRequestNs;

[Serializable]
public class EmbyRequestItem : Entity<Guid>
{
    public string ReferenceId { get; set; }

    public Guid EmbyRequestId { get; private set; }
    
    [Required]
    public string Server { get; set; }

    [Required]
    public string EmbyName { get; set; }
    
    [Required]
    public string Directory { get; set; }
    
    public EmbyRequestItem(
        Guid id,
        Guid fileRequestId,
        [NotNull] string server,
        [NotNull] string filename,
        [NotNull] string directory,
        [CanBeNull] string referenceId = null) : base(id)
    {
        EmbyRequestId = fileRequestId;
        Server = Check.NotNullOrEmpty(server, nameof(server), maxLength: 20);
        EmbyName = Check.NotNullOrEmpty(filename, nameof(filename), maxLength: 250);

        ReferenceId = referenceId;
    }
}
