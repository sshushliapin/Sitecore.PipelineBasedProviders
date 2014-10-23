namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class DeleteItem
  {
    public virtual void Process([NotNull]DeleteItemArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.DeleteItem(args.Item, args.SecurityCheck);
    }
  }
}