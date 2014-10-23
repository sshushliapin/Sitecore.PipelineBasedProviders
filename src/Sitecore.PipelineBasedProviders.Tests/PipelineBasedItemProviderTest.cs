namespace Sitecore.PipelineBasedProviders.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.Pipelines;
  using Sitecore.Globalization;
  using Sitecore.PipelineBasedProviders.Pipelines;
  using Sitecore.SecurityModel;
  using Xunit;
  using Xunit.Extensions;

  public class PipelineBasedItemProviderTest
  {
    private const string ItemName = "home";

    private const SecurityCheck SecurityCheck = SecurityModel.SecurityCheck.Enable;

    private readonly ItemProvider defaultProvider;

    private readonly PipelineBasedItemProvider provider;

    private readonly ID templateId = ID.NewID;

    private readonly ID itemId = ID.NewID;

    public PipelineBasedItemProviderTest()
    {
      this.defaultProvider = Substitute.For<ItemProvider>();
      this.provider = new PipelineBasedItemProvider(this.defaultProvider);
    }

    [Fact]
    public void ShouldBeItemProvider()
    {
      // act & assert
      this.provider.Should().BeAssignableTo<ItemProvider>();
    }

    [Fact]
    public void ShouldInjectDefaultProvider()
    {
      // arrange
      var pipelineBasedItemProvider = new PipelineBasedItemProvider(this.defaultProvider);

      // act & assert
      pipelineBasedItemProvider.DefaultProvider.Should().BeSameAs(this.defaultProvider);
    }

    [Fact]
    public void ShouldResolveDefaultProviderIfNothingIngected()
    {
      // arrange
      var pipelineBasedItemProvider = new PipelineBasedItemProvider();

      // act & assert
      pipelineBasedItemProvider.DefaultProvider.Should().BeSameAs(ItemManager.Providers["default"]);
    }

    [Fact]
    public void ShouldCallAddFromTemplatePipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("new") })
      {
        var destination = db.GetItem("/sitecore/content");
        var newItem = db.GetItem("/sitecore/content/new");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<AddFromTemplateArgs>(a =>
            a.Provider == this.defaultProvider &&
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
    public void ShouldCallAddVersionPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("home"), new DbItem("result") })
      {
        var item = db.GetItem("/sitecore/content/home");
        var expected = db.GetItem("/sitecore/content/result");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<AddVersionArgs>(a =>
            a.Provider == this.defaultProvider &&
            a.Item == item &&
            a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<AddVersionArgs>().Result = expected);

        db.PipelineWatcher.Register("addVersion", processor);

        // act
        var result = this.provider.AddVersion(item, SecurityCheck);

        // assert
        result.Should().BeSameAs(expected);
      }
    }

    [Fact]
    public void ShouldCallCreateItemPipeline()
    {
      // arrange
      using (var db = new Db { new DbItem("new") })
      {
        var destination = db.GetItem("/sitecore/content");
        var newItem = db.GetItem("/sitecore/content/new");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<CreateItemArgs>(a =>
            a.Provider == this.defaultProvider &&
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldCallDeleteItemPipeline(bool isDeleted)
    {
      // arrange
      using (var db = new Db { new DbItem("home") })
      {
        var item = db.GetItem("/sitecore/content/home");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<DeleteItemArgs>(a =>
            a.Provider == this.defaultProvider &&
            a.Item == item &&
            a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<DeleteItemArgs>().Result = isDeleted);

        db.PipelineWatcher.Register("deleteItem", processor);

        // act
        var result = this.provider.DeleteItem(item, SecurityCheck);

        // assert
        result.Should().Be(isDeleted);
      }
    }

    [Fact]
    public void ShouldCallGetItemByIdPipeline()
    {
      // arrange
      var language = Language.Parse("en");
      var version = Version.Parse(2);

      using (var db = new Db { new DbItem("home", this.itemId) })
      {
        var database = db.Database;
        var item = db.GetItem("/sitecore/content/home");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<GetItemByIdArgs>(a =>
            a.Provider == this.defaultProvider &&
            a.ItemId == this.itemId &&
            a.Language == language &&
            a.Version == version &&
            a.Database == database &&
            a.SecurityCheck == SecurityCheck)))
          .Do(ci => ci.Arg<GetItemArgs>().Result = item);

        db.PipelineWatcher.Register("getItemById", processor);

        // act
        var result = this.provider.GetItem(this.itemId, language, version, database, SecurityCheck);

        // assert
        result.Should().BeSameAs(item);
      }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void ShouldCallSaveItemPipeline(bool isDeleted)
    {
      // arrange
      using (var db = new Db { new DbItem("home") })
      {
        var item = db.GetItem("/sitecore/content/home");

        var processor = Substitute.For<IPipelineProcessor>();
        processor
          .When(p => p.Process(Arg.Is<SaveItemArgs>(a =>
            a.Provider == this.defaultProvider &&
            a.Item == item)))
          .Do(ci => ci.Arg<SaveItemArgs>().Result = isDeleted);

        db.PipelineWatcher.Register("saveItem", processor);

        // act
        var result = this.provider.SaveItem(item);

        // assert
        result.Should().Be(isDeleted);
      }
    }
  }
}