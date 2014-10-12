namespace Sitecore.PipelineBasedProviders.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Managers;
  using Xunit;

  public class ConfigurationTests
  {
    [Fact]
    public void ShouldRegisterPipelineBasedProviderAsDefault()
    {
      // assert
      ItemManager.Provider.Should().BeOfType<PipelineBasedItemProvider>();
    }
  }
}