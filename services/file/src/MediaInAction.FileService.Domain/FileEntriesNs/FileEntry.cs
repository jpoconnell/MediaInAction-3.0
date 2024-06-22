using System;
using JetBrains.Annotations;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.FileService.FileEntriesNs
{
    public class FileEntry : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// A unique value for this fileentry.
        /// FileEntryManager ensures the uniqueness of it.
        /// It can not be changed after creation of the fileentry.
        /// </summary>
        [NotNull]
        public string Server { get; set; }
        [NotNull]
        public string Filename { get; set; }
        public string Directory { get; set; }
        public string Extn { get; set; }
        public ListType ListName { get;  set; }
        public long Size { get;  set; }
        public FileStatus FileStatus { get;  set; }
        public int Sequence  { get;  set; }

        private FileEntry()
        {
            //Default constructor is needed for ORMs.
        }

        internal FileEntry(
            Guid id,
            [NotNull] string server,
            [NotNull] string filename,
            [NotNull] string directory,
            [NotNull] string extn,
            long size,
            int sequence,
            ListType listName = ListType.Compressed,
            FileStatus status = FileStatus.New)
        {
            Check.NotNullOrWhiteSpace(filename, nameof(filename));
            
            Id = id;
            SetName(Check.NotNullOrWhiteSpace(filename, nameof(filename)));
            Server = server;
            Directory = directory;
            Extn = extn;
            Size = size;
            ListName = listName;
            FileStatus = status;
            Sequence = sequence;
        }

        public FileEntry SetName([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            if (name.Length >= FileEntryConsts.MaxFileNameLength)
            {
                throw new ArgumentException($"FileEntry name can not be longer than {FileEntryConsts.MaxFileNameLength}");
            }

            Filename = name;
            return this;
        }
   }
}
