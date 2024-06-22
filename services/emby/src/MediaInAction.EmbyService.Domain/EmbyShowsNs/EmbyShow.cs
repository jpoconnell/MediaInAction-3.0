using System;
using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyShowAliasesNs.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.EmbyService.EmbyShowsNs;

public class EmbyShow: AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string Slug { get; set; }
    public List<EmbyShowAlias> ShowAliases { get; set; }
}
