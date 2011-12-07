using System.Reflection;

using Machine.Specifications.Model;
using Machine.Specifications.Utility;
using Machine.Specifications.Utility.Internal;

namespace Machine.Specifications.Factories
{
  public class SpecificationFactory
  {
    public Specification CreateSpecification(Context context, FieldInfo specificationField)
    {
      bool isIgnored = context.IsIgnored || specificationField.HasAttribute<IgnoreAttribute>();
      Then then = (Then) specificationField.GetValue(context.Instance);
      string name = specificationField.Name.ToFormat();

      return new Specification(name, then, isIgnored, specificationField);
    }

    public Specification CreateSpecificationFromBehavior(Behavior behavior, FieldInfo specificationField)
    {
      bool isIgnored = behavior.IsIgnored || specificationField.HasAttribute<IgnoreAttribute>();
      Then then = (Then) specificationField.GetValue(behavior.Instance);
      string name = specificationField.Name.ToFormat();

      return new BehaviorSpecification(name, then, isIgnored, specificationField, behavior.Context, behavior);
    }
  }
}
