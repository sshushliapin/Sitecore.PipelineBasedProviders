namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.Pipelines;

  public class PipelineBasedProviderArgs : PipelineArgs
  {
    private readonly ItemProvider provider;

    public PipelineBasedProviderArgs([NotNull]ItemProvider provider)
    {
      Assert.ArgumentNotNull(provider, "provider");

      this.provider = provider;
    }

    [NotNull]
    public ItemProvider Provider
    {
      get { return this.provider; }
    }
  }
}