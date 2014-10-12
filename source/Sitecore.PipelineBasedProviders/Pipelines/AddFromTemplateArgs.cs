namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;

  public class AddFromTemplateArgs : PipelineBasedProviderArgs
  {
    private readonly string itemName;

    private readonly ID templateId;

    private readonly Item destination;

    private readonly ID newId;

    public AddFromTemplateArgs([NotNull] ItemProvider defaultProvider, [NotNull]string itemName, [NotNull]ID templateId, [NotNull]Item destination, [NotNull]ID newId)
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

    public Item Result { get; set; }
  }
}