namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.SecurityModel;

  public class CreateItemArgs : PipelineBasedProviderArgs
  {
    private readonly string itemName;

    private readonly ID templateId;

    private readonly Item destination;

    private readonly ID newId;

    private readonly SecurityCheck securityCheck;

    public CreateItemArgs([NotNull]ItemProvider defaultProvider, [NotNull]string itemName, [NotNull]Item destination, [NotNull]ID templateId, [NotNull] ID newId, SecurityCheck securityCheck)
      : base(defaultProvider)
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

    [NotNull]
    public string ItemName
    {
      get { return this.itemName; }
    }

    [NotNull]
    public ID TemplateId
    {
      get { return this.templateId; }
    }

    [NotNull]
    public Item Destination
    {
      get { return this.destination; }
    }

    [NotNull]
    public ID NewId
    {
      get { return this.newId; }
    }

    [NotNull]
    public SecurityCheck SecurityCheck
    {
      get { return this.securityCheck; }
    }

    public Item Result { get; set; }
  }
}