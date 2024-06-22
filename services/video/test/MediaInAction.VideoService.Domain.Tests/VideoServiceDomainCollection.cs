using MediaInAction.CatalogService.MongoDB;
using Xunit;

namespace MediaInAction.CatalogService;

[CollectionDefinition(CatalogServiceTestConsts.CollectionDefinitionName)]
public class CatalogServiceDomainCollection : CatalogServiceMongoDbCollectionFixtureBase
{

}