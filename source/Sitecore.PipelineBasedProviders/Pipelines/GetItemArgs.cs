namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.SecurityModel;

  public abstract class GetItemArgs : PipelineBasedProviderArgs
  {
    private readonly Language language;

    private readonly Version version;

    private readonly Database database;

    private readonly SecurityCheck securityCheck;

    public GetItemArgs([NotNull]ItemProvider defaultProvider, [NotNull] Language language, [NotNull]Version version, [NotNull]Database database, SecurityCheck securityCheck)
      : base(defaultProvider)
    {
      Assert.ArgumentNotNull(language, "language");
      Assert.ArgumentNotNull(version, "version");
      Assert.ArgumentNotNull(database, "database");

      this.language = language;
      this.version = version;
      this.database = database;
      this.securityCheck = securityCheck;
    }

    [NotNull]
    public Language Language
    {
      get { return this.language; }
    }

    [NotNull]
    public Version Version
    {
      get { return this.version; }
    }

    [NotNull]
    public Database Database
    {
      get { return this.database; }
    }

    public SecurityCheck SecurityCheck
    {
      get { return this.securityCheck; }
    }

    public Item Result { get; set; }
  }
}