namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Globalization;
  using Sitecore.PipelineBasedProviders.Pipelines;
  using Sitecore.Pipelines;
  using Sitecore.SecurityModel;

  public class PipelineBasedItemProvider : ItemProvider
  {
    private ItemProvider defaultProvider;

    public PipelineBasedItemProvider()
    {
    }

    public PipelineBasedItemProvider(ItemProvider defaultProvider)
    {
      this.defaultProvider = defaultProvider;
    }

    public ItemProvider DefaultProvider
    {
      get { return this.defaultProvider ?? (this.defaultProvider = ItemManager.Providers["default"]); }
    }

    public override Item AddFromTemplate(string itemName, ID templateId, Item destination, ID newId)
    {
      var args = new AddFromTemplateArgs(this.DefaultProvider, itemName, templateId, destination, newId);
      CorePipeline.Run("addFromTemplate", args);

      return args.Result;
    }

    public override Item AddVersion(Item item, SecurityCheck securityCheck)
    {
      var args = new AddVersionArgs(this.DefaultProvider, item, securityCheck);
      CorePipeline.Run("addVersion", args);

      return args.Result;
    }

    public override Item CreateItem(string itemName, Item destination, ID templateId, ID newId, SecurityCheck securityCheck)
    {
      var args = new CreateItemArgs(this.DefaultProvider, itemName, destination, templateId, newId, securityCheck);
      CorePipeline.Run("createItem", args);

      return args.Result;
    }

    public override bool DeleteItem(Item item, SecurityCheck securityCheck)
    {
      var args = new DeleteItemArgs(this.DefaultProvider, item, securityCheck);
      CorePipeline.Run("deleteItem", args);

      return args.Result;
    }

    public override Item GetItem(ID itemId, Language language, Version version, Database database, SecurityCheck securityCheck)
    {
      var args = new GetItemByIdArgs(this.DefaultProvider, itemId, language, version, database, securityCheck);
      CorePipeline.Run("getItemById", args);

      return args.Result;
    }

    public override Item GetItem(string itemPath, Language language, Version version, Database database, SecurityCheck securityCheck)
    {
      var args = new GetItemByPathArgs(this.DefaultProvider, itemPath, language, version, database, securityCheck);
      CorePipeline.Run("getItemByPath", args);

      return args.Result;
    }

    public override bool SaveItem(Item item)
    {
      var args = new SaveItemArgs(this.DefaultProvider, item);
      CorePipeline.Run("saveItem", args);

      return args.Result;
    }
  }
}