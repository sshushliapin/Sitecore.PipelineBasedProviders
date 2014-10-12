namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class CreateItem
  {
    public virtual void Process([NotNull]CreateItemArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.CreateItem(args.ItemName, args.Destination, args.TemplateId, args.NewId, args.SecurityCheck);
    }
  }
}