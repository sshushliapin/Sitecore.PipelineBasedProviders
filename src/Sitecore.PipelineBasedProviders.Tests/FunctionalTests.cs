namespace Sitecore.PipelineBasedProviders.Tests
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.FakeDb;
  using Sitecore.SecurityModel;
  using Xunit;

  public class FunctionalTests
  {
    private const string ItemName = "home";

    private const SecurityCheck SecurityCheck = SecurityModel.SecurityCheck.Enable;

    private readonly ID templateId = ID.NewID;

    private readonly ID itemId = ID.NewID;

    [Fact]
    public void ShouldAddFromTemplate()
    {
      // arrange
      using (var db = new Db { new DbTemplate(this.templateId) })
      {
        var destination = db.GetItem("/sitecore/content");

        // act
        var result = ItemManager.AddFromTemplate(ItemName, this.templateId, destination, this.itemId);

        // assert
        db.GetItem("/sitecore/content/home").Should().NotBeNull();

        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.TemplateID.Should().Be(this.templateId);
        result.Parent.Should().Be(destination);
      }
    }

    [Fact]
    public void ShouldAddVersion()
    {
      // arrange
      using (var db = new Db { new DbItem("home") })
      {
        var item = db.GetItem("/sitecore/content/home");

        // act
        var result = ItemManager.AddVersion(item, SecurityCheck);

        // assert
        result.Versions.Count.Should().Be(2);
      }
    }

    [Fact]
    public void ShouldCreateItem()
    {
      // arrange
      using (var db = new Db())
      {
        var destination = db.GetItem("/sitecore/content");

        // act
        var result = ItemManager.CreateItem(ItemName, destination, this.templateId, this.itemId, SecurityCheck);

        // assert
        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.TemplateID.Should().Be(this.templateId);
        result.Parent.Should().Be(destination);
      }
    }

    [Fact]
    public void ShouldDeleteItem()
    {
      // arrange
      using (var db = new Db { new DbItem("home", this.itemId) })
      {
        var item = db.GetItem("/sitecore/content/home");

        // act
        ItemManager.DeleteItem(item, SecurityCheck).Should().BeTrue();

        // assert
        db.GetItem("/sitecore/content/home").Should().BeNull();
      }
    }

    [Fact]
    public void ShouldGetItem()
    {
      // arrange
      using (var db = new Db { new DbItem("home", this.itemId, this.templateId) })
      {
        // act
        var result = db.GetItem("/sitecore/content/home");

        // assert
        result.Name.Should().Be(ItemName);
        result.ID.Should().Be(this.itemId);
        result.TemplateID.Should().Be(this.templateId);
      }
    }

    [Fact]
    public void ShouldSaveItem()
    {
      // arrange
      using (var db = new Db { new DbItem("home", this.itemId) })
      {
        var item = db.GetItem("/sitecore/content/home");

        // act
        using (new EditContext(item))
        {
          item.Name = "new home";
        }

        // assert
        db.GetItem(this.itemId).Name.Should().Be("new home");
      }
    }
  }
}