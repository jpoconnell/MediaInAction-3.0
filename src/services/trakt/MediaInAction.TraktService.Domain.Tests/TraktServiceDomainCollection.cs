using MediaInAction.TraktService.MongoDb;
using Xunit;

namespace MediaInAction.TraktService;

[CollectionDefinition(TraktServiceTestConsts.CollectionDefinitionName)]
public class TraktServiceDomainCollection : TraktServiceMongoDbCollectionFixtureBase
{

}