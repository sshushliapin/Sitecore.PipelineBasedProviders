Sitecore.PipelineBasedProviders
===============================

```xml
<sitecore>
  <!-- ITEM MANAGER -->
  <itemManager defaultProvider="pipeline">
    <providers>
      <clear />
      <add name="default" type="Sitecore.Data.Managers.ItemProvider, Sitecore.Kernel" />
      <add name="pipeline" type="Sitecore.PipelineBasedProviders.PipelineBasedItemProvider, Sitecore.PipelineBasedProviders" />
    </providers>
  </itemManager>
  <pipelines>
    <!-- Pipeline-based providers -->
    <addFromTemplate>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.AddFromTemplate, Sitecore.PipelineBasedProviders" />
    </addFromTemplate>
    <addVersion>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.AddVersion, Sitecore.PipelineBasedProviders" />
    </addVersion>
    <createItem>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.CreateItem, Sitecore.PipelineBasedProviders" />
    </createItem>
    <deleteItem>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.DeleteItem, Sitecore.PipelineBasedProviders" />
    </deleteItem>
    <getItemById>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.GetItemById, Sitecore.PipelineBasedProviders" />
    </getItemById>
    <getItemByPath>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.GetItemByPath, Sitecore.PipelineBasedProviders" />
    </getItemByPath>
    <saveItem>
      <processor type="Sitecore.PipelineBasedProviders.Pipelines.SaveItem, Sitecore.PipelineBasedProviders" />
    </saveItem>
  </pipelines>
<sitecore>
```