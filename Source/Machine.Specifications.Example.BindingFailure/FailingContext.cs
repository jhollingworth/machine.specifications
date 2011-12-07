using Machine.Specifications.Example.BindingFailure.Ref;

namespace Machine.Specifications.Example.BindingFailure
{
  [Subject("Assembly binding failure")]
  public class if_a_referenced_assembly_cannot_be_bound
  {
    Referenced _referenced;

    Then will_fail = ()=> { };
  }
}
