using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MediaInAction.EmbyService.EmbyRequestNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.EmbyService.RequestNs
{
    public class Request : AuditedAggregateRoot<Guid>
    {
        [NotNull]
      //  public EmbyOperation Operation { get; set; }
        public EmbyRequestState State { get; set; }
        
        [CanBeNull] public string FailReason { get; protected set; }

        public List<EmbyRequestItem> Embys { get; set; }
      

        private Request()
        {
        }

        /*
        public EmbyRequest(Guid id,
            [NotNull] EmbyOperation operation) : base(id)
        {
            Operation = operation;
        }
        */
        
        public virtual void SetAsCompleted()
        {
            if (State == EmbyRequestState.Completed)
            {
                return;
            }

            State = EmbyRequestState.Completed;
            FailReason = null;

            AddDistributedEvent(new EmbyRequestCompletedEto
            {
                EmbyRequestId = Id,
                State = State,
                Embys = Embys.Select(MapEmbyToEto).ToList(),
            });
        }

        public virtual void SetAsFailed(string failReason)
        {
            if (State != EmbyRequestState.Failed)
            {
                return;
            }

            State = EmbyRequestState.Failed;
            FailReason = failReason;

            AddDistributedEvent(new EmbyRequestFailedEto
            {
                EmbyRequestId = Id,
                FailReason = failReason,
            });
        }

        private static EmbyRequestItemEto MapEmbyToEto(EmbyRequestItem file)
        {
            return new EmbyRequestItemEto
            {
                Server = file.Server,
                EmbyName = file.EmbyName,
                Directory = file.Directory,
                ReferenceId = file.ReferenceId
            };
        }
   }
}
