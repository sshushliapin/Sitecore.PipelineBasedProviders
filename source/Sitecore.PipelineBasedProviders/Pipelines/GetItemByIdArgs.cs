namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.SecurityModel;

  public class GetItemByIdArgs : GetItemArgs
  {
    private readonly ID itemId;

    public GetItemByIdArgs([NotNull]ItemProvider defaultProvider, [NotNull] ID itemId, [NotNull] Language language, [NotNull]Version version, [NotNull]Database database, SecurityCheck securityCheck)
      : base(defaultProvider, language, version, database, securityCheck)
    {
      Assert.ArgumentNotNull(itemId, "itemId");

      this.itemId = itemId;
    }

    [NotNull]
    public ID ItemId
    {
      get { return this.itemId; }
    }
  }
}