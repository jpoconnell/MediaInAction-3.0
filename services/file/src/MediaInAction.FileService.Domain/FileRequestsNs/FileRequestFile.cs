using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.FileService.FileRequestsNs
{
    public class FileRequestFile : Entity<Guid>
    {
        public Guid FileRequestId { get; private set; }
        public string ReferenceId { get; private set; }
        public string Server { get; private set; }
        public string FileName { get; private set; }
        public string Directory { get; set; }
        public int Sequence { get; set; }

        public FileRequestFile(
            Guid id,
            Guid fileRequestId,
            [NotNull] string server,
            [NotNull] string filename,
            string directory,
            int quantity,
            decimal totalPrice,
            [CanBeNull] string referenceId = null) : base(id)
        {
            FileRequestId = fileRequestId;
            Server = Check.NotNullOrEmpty(server, nameof(server), maxLength: FileRequestConsts.MaxCodeLength);
            FileName = Check.NotNullOrEmpty(filename, nameof(filename), maxLength: FileRequestConsts.MaxNameLength);
            ReferenceId = referenceId;
        }
    }
}