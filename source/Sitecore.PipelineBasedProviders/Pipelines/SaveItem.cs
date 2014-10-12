namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class SaveItem
  {
    public virtual void Process([NotNull]SaveItemArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.SaveItem(args.Item);
    }
  }
}