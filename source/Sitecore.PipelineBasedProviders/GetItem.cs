namespace Sitecore.PipelineBasedProviders
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.Pipelines;
  using Sitecore.SecurityModel;

  public class GetItemArgs : PipelineArgs
  {
    private readonly ID itemId;

    private readonly Language language;

    private readonly Version version;

    private readonly Database database;

    private readonly SecurityCheck securityCheck;

    public GetItemArgs(ID itemId, Language language, Version version, Database database, SecurityCheck securityCheck)
    {
      Assert.ArgumentNotNull(itemId, "itemId");
      Assert.ArgumentNotNull(language, "language");
      Assert.ArgumentNotNull(version, "version");
      Assert.ArgumentNotNull(database, "database");

      this.itemId = itemId;
      this.language = language;
      this.version = version;
      this.database = database;
      this.securityCheck = securityCheck;
    }

    public ID ItemId
    {
      get { return this.itemId; }
    }

    public Language Language
    {
      get { return this.language; }
    }

    public Version Version
    {
      get { return this.version; }
    }

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