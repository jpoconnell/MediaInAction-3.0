namespace MediaInAction.DelugeService.MongoDb
{
    /* This class can be used as a base class for MongoDB integration tests,
     * while SampleRepository_Tests uses a different approach.
     */
    public abstract class DelugeServiceMongoDbTestBase : DelugeServiceTestBase<DelugeServiceMongoDbTestModule>
    {

    }
}