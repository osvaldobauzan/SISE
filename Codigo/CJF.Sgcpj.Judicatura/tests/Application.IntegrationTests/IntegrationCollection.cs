namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests;

[CollectionDefinition(Name)]
public class IntegrationTestsCollection : ICollectionFixture<TestsInitializer>
{
    public const string Name = nameof(IntegrationTestsCollection);
}
