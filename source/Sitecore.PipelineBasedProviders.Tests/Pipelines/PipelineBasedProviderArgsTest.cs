namespace Sitecore.PipelineBasedProviders.Tests.Pipelines
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data.Managers;
  using Sitecore.PipelineBasedProviders.Pipelines;
  using Xunit;

  public class PipelineBasedProviderArgsTest
  {
    [Fact]
    public void ShouldSetInnerProvider()
    {
      // arrange
      var provider = Substitute.For<ItemProvider>();

      // act
      var args = new PipelineBasedProviderArgs(provider);

      // assert
      args.Provider.Should().BeSameAs(provider);
    }
  }
}