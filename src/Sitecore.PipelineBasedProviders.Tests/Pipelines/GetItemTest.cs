namespace Sitecore.PipelineBasedProviders.Tests.Pipelines
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data.Managers;
  using Sitecore.FakeDb;
  using Sitecore.PipelineBasedProviders.Pipelines;
  using Sitecore.SecurityModel;
  using Xunit;

  public class GetItemTest
  {
    [Fact]
    public void ShouldGetItemUsingDefaultProvider()
    {
      // arrange
      var defaultProvider = Substitute.For<ItemProvider>();
      var processor = new GetItemById();

      using (var db = new Db { new DbItem("home") })
      {
        var item = db.GetItem("/sitecore/content/home");
        defaultProvider.GetItem(item.ID, item.Language, item.Version, item.Database, SecurityCheck.Enable).Returns(item);

        var args = new GetItemByIdArgs(defaultProvider, item.ID, item.Language, item.Version, item.Database, SecurityCheck.Enable);

        // act
        processor.Process(args);

        // assert
        args.Result.Should().Be(item);
      }
    }
  }
}