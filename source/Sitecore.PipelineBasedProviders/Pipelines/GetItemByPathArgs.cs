namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.SecurityModel;

  public class GetItemByPathArgs : GetItemArgs
  {
    private readonly string itemPath;

    public GetItemByPathArgs([NotNull]ItemProvider defaultProvider, [NotNull] string itemPath, [NotNull] Language language, [NotNull]Version version, [NotNull]Database database, SecurityCheck securityCheck)
      : base(defaultProvider, language, version, database, securityCheck)
    {
      Assert.ArgumentNotNull(itemPath, "itemPath");

      this.itemPath = itemPath;
    }

    [NotNull]
    public string ItemPath
    {
      get { return this.itemPath; }
    }
  }
}