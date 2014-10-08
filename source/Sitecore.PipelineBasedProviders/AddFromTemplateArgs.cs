namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Pipelines;

  public class AddFromTemplateArgs : PipelineArgs
  {
    private readonly string itemName;

    private readonly ID templateId;

    private readonly Item destination;

    private readonly ID newId;

    public AddFromTemplateArgs(string itemName, ID templateId, Item destination, ID newId)
    {
      Assert.ArgumentNotNullOrEmpty(itemName, "itemName");
      Assert.ArgumentNotNull(templateId, "templateId");
      Assert.ArgumentNotNull(destination, "destination");
      Assert.ArgumentNotNull(newId, "newId");

      this.itemName = itemName;
      this.templateId = templateId;
      this.destination = destination;
      this.newId = newId;
    }

    public string ItemName
    {
      get { return this.itemName; }
    }

    public ID TemplateId
    {
      get { return this.templateId; }
    }

    public Item Destination
    {
      get { return this.destination; }
    }

    public ID NewId
    {
      get { return this.newId; }
    }

    public Item Result { get; set; }
  }
}