namespace Sitecore.PipelineBasedProviders.Pipelines
{
  using Sitecore.Diagnostics;

  public class AddFromTemplate
  {
    public virtual void Process([NotNull]AddFromTemplateArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      args.Result = args.Provider.AddFromTemplate(args.ItemName, args.TemplateId, args.Destination, args.NewId);
    }
  }
}