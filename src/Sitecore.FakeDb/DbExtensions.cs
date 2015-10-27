namespace Sitecore.FakeDb
{
  using Sitecore.Data.Items;

  public static class DbExtensions
  {
    /// <summary>
    /// Adds a new <see cref="DbItem"/> to the database and returns the <see cref="Item"/> instance.
    /// </summary>
    /// <param name="db">The db context.</param>
    /// <param name="item">The item to create.</param>
    /// <returns>The <see cref="Item"/> instance.</returns>
    public static Item Create(this Db db, DbItem item)
    {
      db.Add(item);
      return db.GetItem(item.ID);
    }

    /// <summary>
    /// Adds a new <see cref="DbTemplate"/> to the database and returns the <see cref="TemplateItem"/> instance.
    /// </summary>
    /// <param name="db">The db context.</param>
    /// <param name="template">The template to create.</param>
    /// <returns>The <see cref="TemplateItem"/> instance.</returns>
    public static TemplateItem Create(this Db db, DbTemplate template)
    {
      db.Add(template);
      return db.GetItem(template.ID);
    }
  }
}