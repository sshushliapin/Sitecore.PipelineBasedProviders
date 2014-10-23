namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class AddVersion
  {
    public virtual void Process([NotNull]AddVersionArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.AddVersion(args.Item, args.SecurityCheck);
    }
  }
}