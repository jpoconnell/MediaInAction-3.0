using MediaInAction.FileService.MongoDB;
using Xunit;

namespace MediaInAction.FileService;

[CollectionDefinition(FileServiceTestConsts.CollectionDefinitionName)]
public class FileServiceDomainCollection : FileServiceMongoDbCollectionFixtureBase
{

}