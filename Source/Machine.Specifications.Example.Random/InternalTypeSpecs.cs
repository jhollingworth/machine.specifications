namespace Machine.Specifications.Specs.Internal
{
  [Subject("Internal types")]
  [Tags(tag.example)]
  class when_a_context_is_internal
  {
    Then should_work =
      () => true.ShouldBeTrue();
  }

  [Subject("Internal types")]
  [Tags(tag.example)]
  class when_a_context_is_internal_and_uses_internal_behaviors
  {
    protected static bool MSpecRocks = true;

    When of = () => { MSpecRocks = true; };

    Behaves_like<WorkingSpecs> a_working_spec;
  }

  [Behaviors]
  class WorkingSpecs
  {
    protected static bool MSpecRocks;

    Then should_work =
      () => MSpecRocks.ShouldBeTrue();
  }
}