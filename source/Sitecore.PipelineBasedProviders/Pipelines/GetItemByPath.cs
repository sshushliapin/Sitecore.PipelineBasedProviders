namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class GetItemByPath
  {
    public virtual void Process([NotNull]GetItemByPathArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.GetItem(args.ItemPath, args.Language, args.Version, args.Database, args.SecurityCheck);
    }
  }
}