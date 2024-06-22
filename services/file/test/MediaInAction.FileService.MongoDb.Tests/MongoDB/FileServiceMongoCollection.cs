using Xunit;

namespace MediaInAction.FileService.MongoDB;

[CollectionDefinition(FileServiceTestConsts.CollectionDefinitionName)]
public class FileServiceMongoCollection : FileServiceMongoDbCollectionFixtureBase
{

}