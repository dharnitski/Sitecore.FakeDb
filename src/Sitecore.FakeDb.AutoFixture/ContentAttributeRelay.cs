namespace Sitecore.FakeDb.AutoFixture
{
  using System.Linq;
  using System.Reflection;
  using Ploeh.AutoFixture.Kernel;

  public class ContentAttributeRelay : ISpecimenBuilder
  {
    public object Create(object request, ISpecimenContext context)
    {
      var customAttributeProvider = request as ICustomAttributeProvider;
      if (customAttributeProvider == null)
      {
        return new NoSpecimen(request);
      }

      var attribute =
        customAttributeProvider.GetCustomAttributes(typeof(ContentAttribute), true)
          .OfType<ContentAttribute>()
          .FirstOrDefault();
      if (attribute == null)
      {
        return new NoSpecimen(request);
      }

      var parameterInfo = request as ParameterInfo;
      if (parameterInfo == null)
      {
        return new NoSpecimen(request);
      }

      return context.Resolve(parameterInfo.ParameterType);
    }
  }
}