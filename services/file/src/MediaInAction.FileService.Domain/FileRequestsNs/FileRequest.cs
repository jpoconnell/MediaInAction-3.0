using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.FileService.FileRequestsNs
{
    public class FileRequest : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public FileOperation Operation { get; set; }
        public FileRequestState State { get; set; }
        
        [CanBeNull] public string FailReason { get; protected set; }

        public List<FileRequestFile> Files { get; set; }
      

        public FileRequest()
        {
        }
        
        
        public virtual void SetAsCompleted()
        {
            if (State == FileRequestState.Completed)
            {
                return;
            }

            State = FileRequestState.Completed;
            FailReason = null;

            AddDistributedEvent(new FileRequestCompletedEto
            {
                //FileRequestId = Id,
                State = State,
                //Files = Files.Select(MapFileToEto).ToList(),
                ExtraProperties = ExtraProperties
            });
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != FileRequestState.Failed)
            {
                return;
            }

            State = FileRequestState.Failed;
            FailReason = failReason;

            AddDistributedEvent(new FileRequestFailedEto
            {
                FileRequestId = Id,
                FailReason = failReason,
                ExtraProperties = ExtraProperties
            });
        }

        private static FileRequestFileEto MapFileToEto(FileRequestFile file)
        {
            return new FileRequestFileEto
            {
                Server = file.Server,
                FileName = file.FileName,
                Directory = file.Directory,
                ReferenceId = file.ReferenceId,
                Sequence = file.Sequence
            };
        }
   }
}
