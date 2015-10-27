namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Items;
  using Xunit;

  public class DbExtensionsTest
  {
    [Fact]
    public void ShouldCreateAndGetItem()
    {
      using (var db = new Db())
      {
        db.Create(new DbItem("item")).Should().BeAssignableTo<Item>();
      }
    }

    [Fact]
    public void ShouldCreateAndGetTemplate()
    {
      using (var db = new Db())
      {
        db.Create(new DbTemplate("template")).Should().BeAssignableTo<TemplateItem>();
      }
    }
  }
}