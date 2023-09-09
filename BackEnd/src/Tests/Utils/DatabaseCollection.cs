namespace Tests.Utils
{
    using Xunit;
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<InMemoryDbContextFixture>
    {
    }
}
