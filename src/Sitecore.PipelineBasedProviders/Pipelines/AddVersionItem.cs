namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.SecurityModel;

  public class AddVersionArgs : PipelineBasedProviderArgs
  {
    private readonly Item item;

    private readonly SecurityCheck securityCheck;

    public AddVersionArgs(ItemProvider defaultProvider, Item item, SecurityCheck securityCheck)
      : base(defaultProvider)
    {
      Assert.ArgumentNotNull(item, "item");

      this.item = item;
      this.securityCheck = securityCheck;
    }

    public Item Item
    {
      get { return this.item; }
    }

    public SecurityCheck SecurityCheck
    {
      get { return this.securityCheck; }
    }

    public Item Result { get; set; }
  }
}