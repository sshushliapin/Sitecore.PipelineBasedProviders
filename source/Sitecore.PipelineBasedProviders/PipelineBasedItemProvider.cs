namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Globalization;
  using Sitecore.Pipelines;
  using Sitecore.SecurityModel;

  public class PipelineBasedItemProvider : ItemProvider
  {
    public override Item AddFromTemplate(string itemName, ID templateId, Item destination, ID newId)
    {
      var args = new AddFromTemplateArgs(itemName, templateId, destination, newId);
      CorePipeline.Run("addFromTemplate", args);

      return args.Result ?? base.AddFromTemplate(itemName, templateId, destination, newId);
    }

    public override Item AddVersion(Item item, SecurityCheck securityCheck)
    {
      var args = new AddVersionArgs(item, securityCheck);
      CorePipeline.Run("addVersion", args);

      return args.Result ?? base.AddVersion(item, securityCheck);
    }

    public override Item CreateItem(string itemName, Item destination, ID templateId, ID newId, SecurityCheck securityCheck)
    {
      var args = new CreateItemArgs(itemName, destination, templateId, newId, securityCheck);
      CorePipeline.Run("createItem", args);

      return args.Result ?? base.CreateItem(itemName, destination, templateId, newId, securityCheck);
    }

    public override Item GetItem(ID itemId, Language language, Version version, Database database, SecurityCheck securityCheck)
    {
      var args = new GetItemArgs(itemId, language, version, database, securityCheck);
      CorePipeline.Run("getItem", args);

      return args.Result ?? base.GetItem(itemId, language, version, database, securityCheck);
    }
  }
}