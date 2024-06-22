using Volo.Abp;

namespace MediaInAction.FileService.FileEntriesNs
{
    public class FileEntryAlreadyExistsException : BusinessException
    {
        public FileEntryAlreadyExistsException(string filename)
            : base("FileService:000001", $"A fileentry with filename {filename} has already exists!")
        {
            WithData("fileEntryCode", filename);
        }
    }
}