using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService.MediaFoldersNs;
public class MediaFolder: Entity<Guid>
{
    public string Name { get; set; }
    public string Server { get; set; }
    
    //public string CollectionType { get; set; }
}

