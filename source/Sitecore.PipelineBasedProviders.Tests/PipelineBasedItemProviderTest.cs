namespace Sitecore.PipelineBasedProviders.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Globalization;
  using Sitecore.SecurityModel;
  using Xunit;

  public class PipelineBasedItemProviderTest
  {
    private const string ItemName = "home";

    private const SecurityCheck SecurityCheck = SecurityModel.SecurityCheck.Enable;

    private readonly PipelineBasedItemProvider provider;

    private readonly ID templateId = ID.NewID;

    private readonly ID itemId = ID.NewID;

    public PipelineBasedItemProviderTest()
    {
      this.provider = new PipelineBasedItemProvider();
    }

    [Fact]
    public void ShouldBeItemProvider()
    {
      // act & assert
      this.provider.Should().BeAssignableTo<ItemProvider>();
    }

    [Fact]
    public void ShouldAddFromTemplateUsingPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("new") })
      {
        var destination = db.GetItem("/sitecore/content");
        var newItem = db.GetItem("/sitecore/content/new");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<AddFromTemplateArgs>(a =>
            a.ItemName == ItemName &&
            a.TemplateId == this.templateId &&
            a.Destination == destination &&
            a.NewId == this.itemId)))
          .Do(ci => ci.Arg<AddFromTemplateArgs>().Result = newItem);

        db.PipelineWatcher.Register("addFromTemplate", processor);

        // act
        var result = this.provider.AddFromTemplate(ItemName, this.templateId, destination, this.itemId);

        // assert
        result.Should().BeSameAs(newItem);
      }
    }

    [Fact]
    public void ShouldAddFromTemplateUsingDefaultProviderIfNoItemCreatedByPipeline()
    {
      // arrange
      using (var db = new Db())
      {
        var destination = db.GetItem("/sitecore/content");

        var processor = Substitute.For<IPipelineProcessor>();
        db.PipelineWatcher.Register("addFromTemplate", processor);

        // act
        var result = this.provider.AddFromTemplate(ItemName, this.templateId, destination, this.itemId);

        // assert
        db.GetItem("/sitecore/content/home").Should().NotBeNull();

        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.TemplateID.Should().Be(this.templateId);
        result.Parent.Should().Be(destination);
      }
    }

    [Fact]
    public void ShouldAddVersionUsingPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("home"), new DbItem("result") })
      {
        var item = db.GetItem("/sitecore/content/home");
        var expected = db.GetItem("/sitecore/content/result");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<AddVersionArgs>(a => a.Item == item && a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<AddVersionArgs>().Result = expected);

        db.PipelineWatcher.Register("addVersion", processor);

        // act
        var result = this.provider.AddVersion(item, SecurityCheck);

        // assert
        result.Should().BeSameAs(expected);
      }
    }

    [Fact]
    public void ShouldAddVersionUsingDefaultProviderIfNoVersionAddedByPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("home") })
      {
        var item = db.GetItem("/sitecore/content/home");

        var processor = Substitute.For<IPipelineProcessor>();
        db.PipelineWatcher.Register("addVersion", processor);

        // act
        var result = this.provider.AddVersion(item, SecurityCheck);

        // assert
        result.Versions.Count.Should().Be(2);
      }
    }

    [Fact]
    public void ShouldCreateItemUsingPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("new") })
      {
        var destination = db.GetItem("/sitecore/content");
        var newItem = db.GetItem("/sitecore/content/new");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<CreateItemArgs>(a =>
            a.ItemName == ItemName &&
            a.TemplateId == this.templateId &&
            a.Destination == destination &&
            a.NewId == this.itemId &&
            a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<CreateItemArgs>().Result = newItem);

        db.PipelineWatcher.Register("createItem", processor);

        // act
        var result = this.provider.CreateItem(ItemName, destination, this.templateId, this.itemId, SecurityCheck);

        // assert
        result.Should().BeSameAs(newItem);
      }
    }

    [Fact]
    public void ShouldCreateItemUsingDefaultProviderIfNoItemCreatedByPipeline()
    {
      // arrange
      using (var db = new Db())
      {
        var destination = db.GetItem("/sitecore/content");

        var processor = Substitute.For<IPipelineProcessor>();
        db.PipelineWatcher.Register("createItem", processor);

        // act
        var result = this.provider.CreateItem(ItemName, destination, this.templateId, this.itemId, SecurityCheck);

        // assert
        db.GetItem("/sitecore/content/home").Should().NotBeNull();

        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.TemplateID.Should().Be(this.templateId);
        result.Parent.Should().Be(destination);
      }
    }

    [Fact]
    public void ShouldGetItemUsingPipeline()
    {
      // arrange
      var language = Language.Parse("en");
      var version = Version.Parse(2);

      using (var db = new Db { new DbItem("home") })
      {
        var database = db.Database;
        var item = db.GetItem("/sitecore/content/home");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<GetItemArgs>(a =>
            a.ItemId == this.itemId &&
            a.Language == language &&
            a.Version == version &&
            a.Database == database &&
            a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<GetItemArgs>().Result = item);

        db.PipelineWatcher.Register("getItem", processor);

        // act
        var result = this.provider.GetItem(this.itemId, language, version, database, SecurityCheck);

        // assert
        result.Should().BeSameAs(item);
      }
    }

    [Fact]
    public void ShouldGetItemUsingDefaultProviderIfNoItemCreatedByPipeline()
    {
      // arrange
      var language = Language.Parse("en");
      var version = Version.Parse(2);

      using (var db = new Db { new DbItem("home", this.itemId) })
      {
        var database = db.Database;

        var processor = Substitute.For<IPipelineProcessor>();
        db.PipelineWatcher.Register("getItem", processor);

        // act
        var result = this.provider.GetItem(this.itemId, language, version, database, SecurityCheck);

        // assert
        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.Language.Should().Be(language);
        result.Version.Should().Be(version);
      }
    }
  }
}