using Xunit;

namespace MediaInAction.CatalogService.MongoDB;

[CollectionDefinition(CatalogServiceTestConsts.CollectionDefinitionName)]
public class CatalogServiceMongoCollection : CatalogServiceMongoDbCollectionFixtureBase
{

}