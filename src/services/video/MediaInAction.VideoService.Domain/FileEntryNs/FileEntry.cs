using JetBrains.Annotations;
using System;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.VideoService.FileEntryNs;

public class FileEntry : AuditedAggregateRoot<Guid>
{
    public string ExternalId { get; set; }
    public string Server { get; set; }
    public string Directory { get; set; }
    public string FileName { get; set; }
    public string Extn { get; set; }
    public long Size { get; set; }
    public ListType ListName { get; set; }
    public FileStatus FileStatus { get;  set; }
    public int Sequence { get; set; }
    public Guid SeriesLink { get; set; }
    public Guid EpisodeLink { get; set; }
    public MediaType MediaType { get; set; }
    
    public string CleanFileName { get; set; }
    private FileEntry()
    {
    }

    public FileEntry(Guid id, 
        string externalId,
        [NotNull] string server,
        [NotNull] string fileName, 
        [NotNull] string directory, 
        string extn,
        long size,
        ListType listName, 
        int sequence,
        FileStatus status,
        bool isMapped
        )
        : base(id)
    {
        Server = Check.NotNullOrEmpty(server, nameof(server));
        FileName = Check.NotNullOrEmpty(fileName, nameof(fileName));
        Directory = Check.NotNullOrEmpty(directory, nameof(directory));
        //ExternalId = Check.NotNullOrEmpty(externalId, nameof(externalId));
        ListName = listName;
        Sequence = sequence;
        Extn = extn;
        Size = size;
        FileStatus = status;
    }

    private int GenerateFileEntryNo(Guid id)
    {
        // Simple fileEntry no generation. Should be improved for uniqueness.
        var code = Id.GetHashCode();
        if (code < 0) // Should be negative
        {
            code *= -1;
        }

        return code;
    }
    
}