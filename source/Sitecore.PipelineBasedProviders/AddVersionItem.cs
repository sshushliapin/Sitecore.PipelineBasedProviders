namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data.Items;
  using Sitecore.Pipelines;
  using Sitecore.SecurityModel;

  public class AddVersionArgs : PipelineArgs
  {
    private readonly Item item;

    private readonly SecurityCheck securityCheck;

    public AddVersionArgs(Item item, SecurityCheck securityCheck)
    {
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