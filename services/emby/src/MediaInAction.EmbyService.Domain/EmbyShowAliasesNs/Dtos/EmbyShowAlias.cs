using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.EmbyService.EmbyShowAliasesNs.Dtos;

public class EmbyShowAlias : Entity<Guid>
    {
        public Guid ShowId { get; set; }
        public string IdType { get; set; }
        public string IdValue { get; set; }
        
        public EmbyShowAlias() { }
        
    }
