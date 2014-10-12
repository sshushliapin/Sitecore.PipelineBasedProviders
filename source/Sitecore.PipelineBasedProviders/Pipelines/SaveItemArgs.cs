namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;

  public class SaveItemArgs : PipelineBasedProviderArgs
  {
    private readonly Item item;

    public SaveItemArgs([NotNull]ItemProvider defaultProvider, [NotNull]Item item)
      : base(defaultProvider)
    {
      Assert.ArgumentNotNull(item, "item");

      this.item = item;
    }

    [NotNull]
    public Item Item
    {
      get { return this.item; }
    }

    public bool Result { get; set; }
  }
}