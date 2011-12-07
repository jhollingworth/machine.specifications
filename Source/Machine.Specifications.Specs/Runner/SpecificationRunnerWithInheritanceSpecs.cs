namespace Machine.Specifications.Specs.Runner
{
  [Subject("Specification Runner")]
  public class when_running_a_context_with_inherited_specifications
    : RunnerSpecs
  {
    Given context = () =>
      {
        context_that_inherits.BaseEstablishRunCount = 0;
        context_with_inherited_specifications.BecauseClauseRunCount = 0;
        context_with_inherited_specifications.EstablishRunCount = 0;
      };

    When of = Run<context_with_inherited_specifications>;

    Then should_establish_the_context_once = () =>
                                           context_with_inherited_specifications.BecauseClauseRunCount.
                                             ShouldEqual(1);

    Then should_invoke_the_because_clause_once = () =>
                                               context_with_inherited_specifications.EstablishRunCount.
                                                 ShouldEqual(1);

    Then should_invoke_the_because_clause_from_the_base_class_once = () =>
                                                                   context_that_inherits.BaseEstablishRunCount.
                                                                     ShouldEqual(1);

    Then should_detect_two_specs = () =>
                                 testListener.SpecCount.ShouldEqual(2);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_inherited_specifications_and_setup_once_per_attribute
    : RunnerSpecs
  {
    Given context = () =>
      {
        context_that_inherits.BaseEstablishRunCount = 0;
        context_with_inherited_specifications_and_setup_for_each.BecauseClauseRunCount = 0;
        context_with_inherited_specifications_and_setup_for_each.EstablishRunCount = 0;
      };

    When of = Run<context_with_inherited_specifications_and_setup_for_each>;

    Then should_establish_the_context_twice = () =>
                                            context_with_inherited_specifications_and_setup_for_each.
                                              BecauseClauseRunCount.ShouldEqual(2);

    Then should_invoke_the_because_clause_twice = () =>
                                                context_with_inherited_specifications_and_setup_for_each.
                                                  EstablishRunCount.ShouldEqual(2);

    Then should_invoke_the_because_clause_from_the_base_class_twice = () =>
                                                                    context_that_inherits.BaseEstablishRunCount.
                                                                      ShouldEqual(2);

    Then should_detect_two_specs = () =>
                                 testListener.SpecCount.ShouldEqual(2);
  }
}