namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class GetItemById
  {
    public virtual void Process([NotNull]GetItemByIdArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.GetItem(args.ItemId, args.Language, args.Version, args.Database, args.SecurityCheck);
    }
  }
}