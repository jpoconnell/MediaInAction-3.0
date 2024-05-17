namespace MediaInAction.FileService
{
    public static class FileServiceDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public static string FileEntryNotFound  = "File:00001";
        public static string FileEntryIdNotGuid  = "File:00002";
        public static string FileEntryIdNotInDatabase = "File:00003";
        public static string FileEntryWithNewStatus =  "File:00004";
        public static string FileRequestAlreadyHave = "File:00005";
    }
}
