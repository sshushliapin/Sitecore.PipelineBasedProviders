namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Pipelines;
  using Sitecore.SecurityModel;

  public class CreateItemArgs : PipelineArgs
  {
    private readonly string itemName;

    private readonly ID templateId;

    private readonly Item destination;

    private readonly ID newId;

    private readonly SecurityCheck securityCheck;

    public CreateItemArgs(string itemName, Item destination, ID templateId, ID newId, SecurityCheck securityCheck)
    {
      Assert.ArgumentNotNullOrEmpty(itemName, "itemName");
      Assert.ArgumentNotNull(templateId, "templateId");
      Assert.ArgumentNotNull(destination, "destination");
      Assert.ArgumentNotNull(newId, "newId");

      this.itemName = itemName;
      this.templateId = templateId;
      this.destination = destination;
      this.newId = newId;
      this.securityCheck = securityCheck;
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

    public SecurityCheck SecurityCheck
    {
      get { return this.securityCheck; }
    }

    public Item Result { get; set; }
  }
}