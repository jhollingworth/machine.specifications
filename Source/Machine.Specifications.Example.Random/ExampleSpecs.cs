using System;
using System.Reflection;

namespace Machine.Specifications.Specs
{
  public static class tag
  {
    public const string example = "example";
    public const string some_other_tag = "some other tag";
    public const string one_more_tag = "one more tag";
  }

  [SetupForEachSpecification, Tags(tag.example)]
  public class context_with_multiple_specifications_and_setup_for_each
  {
    public static int EstablishRunCount;
    public static int BecauseClauseRunCount;

    Given context = () => EstablishRunCount++;

    When of = () => BecauseClauseRunCount++;

    Then spec1 = () => { };
    Then spec2 = () => { };
  }

  [Tags(tag.example, "foobar")]
  public class context_with_multiple_specifications
  {
    public static int EstablishRunCount;
    public static int BecauseClauseRunCount;

    Given context = () => EstablishRunCount++;

    When of = () => BecauseClauseRunCount++;

    Then spec1 = () => { };
    Then spec2 = () => { };
  }

  [Tags(tag.example, tag.example)]
  [Tags(tag.example)]
  public class context_with_duplicate_tags
  {
    Then bla_bla = () => { };
  }

  [Tags(tag.example, tag.some_other_tag, tag.one_more_tag)]
  public class context_with_tags
  {
    Then bla_bla = () => { };
  }

  public class context_with_unimplemented_specs
  {
    Then should_be_unimplemented;
  }
  
  [Ignore]
  public class context_with_ignore : context_with_no_specs
  {
    public static bool IgnoredSpecRan;

    Then should_be_ignored = () =>
      IgnoredSpecRan = true;
  }

  public class context_with_ignore_on_one_spec : context_with_no_specs
  {
    public static bool IgnoredSpecRan;

    [Ignore]
    Then should_be_ignored = () =>
      IgnoredSpecRan = true;
  }
  
  [Ignore("just When")]
  public class context_with_ignore_and_reason : context_with_no_specs
  {
    public static bool IgnoredSpecRan;

    Then should_be_ignored = () =>
      IgnoredSpecRan = true;
  }

  public class context_with_ignore_and_reason_on_one_spec : context_with_no_specs
  {
    public static bool IgnoredSpecRan;

    [Ignore("just When")]
    Then should_be_ignored = () =>
      IgnoredSpecRan = true;
  }

  [Tags(tag.example)]
  public class context_with_no_specs
  {
    public static bool ContextEstablished;
    public static bool CleanupOccurred;

    Given context = () =>
    {
      ContextEstablished = true;
    };

    Cleanup after_each = () =>
    {
      CleanupOccurred = true;
    };
  }

  [Subject(typeof(int), "Some description")]
  [Tags(tag.example)]
  public class context_with_subject
  {
  }

  public class context_with_parent_with_subject : context_with_subject
  {
  }

  [Subject(typeof(int), "Parent description")]
  public class parent_context
  {
    Then should_be_able_to_assert_something = () =>
      true.ShouldBeTrue();

    public class nested_context
    {
      Then should_be_able_to_assert_something_else = () =>
        false.ShouldBeFalse();
    }

    public class nested_context_inheriting_another_concern : context_with_subject
    {
      Then should_be_able_to_assert_something_else = () =>
        false.ShouldBeFalse();
    }

    [Subject(typeof(int), "Nested description")]
    public class nested_context_inheriting_and_owning_a_concern : context_with_subject
    {
      Then should_be_able_to_assert_something_else = () =>
        false.ShouldBeFalse();
    }
  }

  public class parent_context_without_concern
  {
    Then should_be_able_to_assert_something = () =>
      true.ShouldBeTrue();

    public class nested_context
    {
      Then should_be_able_to_assert_something_else = () =>
        false.ShouldBeFalse();
    }
  }

  public class parent_context_that_has_its_own_because_block
  {
    When of = () =>
    {
    };

    public class nested_context_that_has_a_because_block_which
    {
      When of = () =>
      {
      };
    }
  }

  [Tags(tag.example)]
  public class context_with_failing_specs
  {
    Then should = () => { throw new InvalidOperationException("something went wrong"); };
  }

  [Tags(tag.example)]
  public class context_with_failing_establish
  {
    Given context = () => { throw new InvalidOperationException("something went wrong"); };
    Then should = () => { };
  }

  [Tags(tag.example)]
  public class context_with_failing_because
  {
    When of = () => { throw new InvalidOperationException("something went wrong"); };
    Then should = () => { };
  }

  [Tags(tag.example)]
  public class context_with_console_output
  {
    Given context = () =>
    {
      Console.Out.WriteLine("Console.Out message in Given");
      Console.Error.WriteLine("Console.Error message in Given");
    };

    When of = () =>
    {
      Console.Out.WriteLine("Console.Out message in When");
      Console.Error.WriteLine("Console.Error message in When");
    };

    Cleanup after = () =>
    {
      Console.Out.WriteLine("Console.Out message in cleanup");
      Console.Error.WriteLine("Console.Error message in cleanup");
    };

    Then should_log_messages = () =>
    {
      Console.Out.WriteLine("Console.Out message in spec");
      Console.Error.WriteLine("Console.Error message in spec");
    };

    Then should_log_messages_also_for_the_nth_run = () =>
    {
      Console.Out.WriteLine("Console.Out message in spec");
      Console.Error.WriteLine("Console.Error message in spec");
    };
  }

  [Tags(tag.example)]
  public class context_with_inner_exception
  {
    Then should_throw = () =>
    {
      try
      {
        throw new Exception("INNER123");

      }
      catch (Exception err)
      {
        throw new TargetInvocationException(err);
      }
    };
  }

  [SetupForEachSpecification, Tags(tag.example)]
  public class context_with_console_output_and_for_each
  {
    Given context = () =>
    {
      Console.Out.WriteLine("Console.Out message in Given");
      Console.Error.WriteLine("Console.Error message in Given");
    };

    When of = () =>
    {
      Console.Out.WriteLine("Console.Out message in When");
      Console.Error.WriteLine("Console.Error message in When");
    };

    Cleanup after_each = () =>
    {
      Console.Out.WriteLine("Console.Out message in cleanup");
      Console.Error.WriteLine("Console.Error message in cleanup");
    };

    Then should_log_messages = () =>
    {
      Console.Out.WriteLine("Console.Out message in spec");
      Console.Error.WriteLine("Console.Error message in spec");
    };

    Then should_log_messages_also_for_the_nth_run = () =>
    {
      Console.Out.WriteLine("Console.Out message in spec");
      Console.Error.WriteLine("Console.Error message in spec");
    };
  }

  public class Container
  {
    [Tags(tag.example)]
    public class nested_context
    {
      Then should_be_run = () => { };
    }
  }

  [Tags(tag.example)]
  public class context_with_public_It_field
  {
    public Then should;
  }

  [Tags(tag.example)]
  public class context_with_protected_It_field
  {
    protected Then should;
  }

  [Tags(tag.example)]
  public class context_with_internal_It_field
  {
    internal Then should;
  }

  [Tags(tag.example)]
  public class context_with_public_Behaves_like_field
  {
    public Behaves_like<Behaviors> behavior;
  }

  [Tags(tag.example)]
  public class context_with_nonprivate_framework_fields
  {
    public Given Given;
    public When When;
    public Cleanup cleanup;

    internal Behaves_like<Behaviors> behavior;
    protected Then specification;
    Then private_specification;
  }
}
