using System;
using Machine.Specifications.Example;
using Machine.Specifications.FailingExample;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Impl;

namespace Machine.Specifications.Specs.Runner
{
  [Subject("Specification Runner")]
  public class when_running_a_context_with_no_specifications
    : RunnerSpecs
  {
    Given context = () =>
    {
      TestCleanupAfterEveryContext.AfterContextCleanupRun = false;
      context_with_no_specs.ContextEstablished = false;
      context_with_no_specs.CleanupOccurred = false;
    };

    When of = () =>
      Run<context_with_no_specs>();

    Then should_not_establish_the_context = () =>
      context_with_no_specs.ContextEstablished.ShouldBeFalse();

    Then should_not_cleanup = () =>
      context_with_no_specs.CleanupOccurred.ShouldBeFalse();

    Then should_not_perform_assembly_wide_cleanup = () =>
      TestCleanupAfterEveryContext.AfterContextCleanupRun.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_an_ignored_specifications
    : RunnerSpecs
  {
    Given context = () =>
    {
      context_with_no_specs.ContextEstablished = false;
      context_with_no_specs.CleanupOccurred = false;
      context_with_ignore_on_one_spec.IgnoredSpecRan = false;
      TestCleanupAfterEveryContext.AfterContextCleanupRun = false;
    };

    When of = () =>
      Run<context_with_ignore_on_one_spec>();

    Then should_not_run_the_spec = () =>
      context_with_ignore_on_one_spec.IgnoredSpecRan.ShouldBeFalse();

    Then should_not_establish_the_context = () =>
      context_with_ignore_on_one_spec.ContextEstablished.ShouldBeFalse();

    Then should_not_cleanup = () =>
      context_with_ignore_on_one_spec.CleanupOccurred.ShouldBeFalse();

    Then should_not_perform_assembly_wide_cleanup = () =>
      TestCleanupAfterEveryContext.AfterContextCleanupRun.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_an_ignored_context
    : RunnerSpecs
  {
    Given context = () =>
    {
      context_with_ignore.IgnoredSpecRan = false;
      TestCleanupAfterEveryContext.AfterContextCleanupRun = false;
    };

    When of = () =>
      Run<context_with_ignore>();

    Then should_not_run_the_spec = () =>
      context_with_ignore.IgnoredSpecRan.ShouldBeFalse();

    Then should_not_establish_the_context = () =>
      context_with_ignore.ContextEstablished.ShouldBeFalse();

    Then should_not_cleanup = () =>
      context_with_ignore.CleanupOccurred.ShouldBeFalse();

    Then should_not_perform_assembly_wide_cleanup = () =>
      TestCleanupAfterEveryContext.AfterContextCleanupRun.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_multiple_specifications
    : RunnerSpecs
  {
    Given context = () =>
    {
      context_with_multiple_specifications.EstablishRunCount = 0;
      context_with_multiple_specifications.BecauseClauseRunCount = 0;
      TestCleanupAfterEveryContext.Reset();
    };

    When of = () =>
      Run<context_with_multiple_specifications>();

    Then should_establish_the_context_once = () =>
      context_with_multiple_specifications.EstablishRunCount.ShouldEqual(1);

    Then should_invoke_the_because_clause_once = () =>
      context_with_multiple_specifications.BecauseClauseRunCount.ShouldEqual(1);

    Then should_invoke_the_assembly_wide_cleanup_once = () =>
      TestCleanupAfterEveryContext.AfterContextCleanupRunCount.ShouldEqual(1);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_multiple_specifications_and_setup_once_per_attribute
    : RunnerSpecs
  {
    Given context = () =>
    {
      context_with_multiple_specifications_and_setup_for_each.EstablishRunCount = 0;
      context_with_multiple_specifications_and_setup_for_each.BecauseClauseRunCount = 0;
      TestCleanupAfterEveryContext.Reset();
    };

    When of = () =>
      Run<context_with_multiple_specifications_and_setup_for_each>();

    Then should_establish_the_context_for_each_specification = () =>
      context_with_multiple_specifications_and_setup_for_each.EstablishRunCount.ShouldEqual(2);

    Then should_invoke_the_because_clause_for_each_specification = () =>
      context_with_multiple_specifications_and_setup_for_each.BecauseClauseRunCount.ShouldEqual(2);

    Then should_invoke_the_assembly_wide_cleanup_once_per_spec = () =>
      TestCleanupAfterEveryContext.AfterContextCleanupRunCount.ShouldEqual(2);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_multiple_establish_clauses
    : RunnerSpecs
  {
    static Exception exception;

    When of = () =>
      exception = Catch.Exception(Run<context_with_multiple_establish_clauses>);

    Then should_fail = () =>
      exception.ShouldBeOfType<SpecificationUsageException>();
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_failing_establish_clauses
    : RunnerSpecs
  {
    When of = Run<context_with_failing_establish>;

    Then should_fail = () =>
    testListener.LastResult.Passed.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_failing_because_clauses
    : RunnerSpecs
  {
    When of = Run<context_with_failing_because>;

    Then should_fail = () =>
    testListener.LastResult.Passed.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_failing_specs
  : RunnerSpecs
  {
    When of = Run<context_with_failing_specs>;

    Then should_fail = () =>
      testListener.LastResult.Passed.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_an_assembly_with_no_included_contexts
  {
    static DefaultRunner runner;

    Given context = () =>
    {
      TestAssemblyContext.OnAssemblyStartRun = false;
      TestAssemblyContext.OnAssemblyCompleteRun = false;
      runner = new DefaultRunner(new TestListener(), new RunOptions(new[] { "asdfasdf" }, new string[0], new string[0]));
    };

    When of = () =>
      runner.RunAssembly(typeof(TestAssemblyContext).Assembly);

    Then should_not_run_assembly_start = () =>
      TestAssemblyContext.OnAssemblyStartRun.ShouldBeFalse();

    Then should_not_run_assembly_complete = () =>
      TestAssemblyContext.OnAssemblyCompleteRun.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  [Ignore]
  public class when_running_a_specification_with_console_output
    : RunnerSpecs
  {
    When of = () =>
      Run<context_with_console_output>();

    Then should_capture_the_console_out_stream = () =>
      testListener.LastResult.ConsoleOut.ShouldEqual("Console.Out message in Given\r\n" +
        "Console.Out message in When\r\n" +
        "Console.Out message in spec\r\n");

    Then should_capture_the_console_error_stream = () =>
      testListener.LastResult.ConsoleError.ShouldEqual("Console.Error message in Given\r\n" +
        "Console.Error message in When\r\n" +
        "Console.Error message in spec\r\n");
  }

  [Subject("Specification Runner")]
  [Ignore]
  public class when_running_a_specification_with_console_output_and_foreach
    : RunnerSpecs
  {
    When of = () =>
      Run<context_with_console_output_and_for_each>();

    Then should_capture_the_console_out_stream = () =>
      testListener.LastResult.ConsoleOut.ShouldEqual("Console.Out message in Given\r\n" +
        "Console.Out message in When\r\n" +
        "Console.Out message in spec\r\n" +
        "Console.Out message in cleanup\r\n");

    Then should_capture_the_console_error_stream = () =>
      testListener.LastResult.ConsoleError.ShouldEqual("Console.Error message in Given\r\n" +
        "Console.Error message in When\r\n" +
        "Console.Error message in spec\r\n" +
        "Console.Error message in cleanup\r\n");
  }

  [Subject("Specification Runner")]
  public class when_running_a_specification_that_throws_an_exception_with_an_inner_exception
    : RunnerSpecs
  {
    When of =()=>
      Run<context_with_inner_exception>();

    Then should_include_the_inner_exception_in_the_result =()=>
      testListener.LastResult.Exception.ToString().ShouldContain("INNER123");
  }

  [Subject("Specification Runner")]
  public class when_running_a_behavior
    : RunnerSpecs
  {
    Given context = () =>
    {
      Behaviors.BehaviorSpecRan = false;
    };

    When of = () =>
      Run<Behaviors>();

    Then should_not_run_the_behavior_specs = () =>
      Behaviors.BehaviorSpecRan.ShouldBeFalse();
  }

  [Subject("Specification Runner")]
  public class when_running_with_tag
  {
    Given context = () =>
    {
      TaggedCleanup.Reset();
      UntaggedCleanup.Reset();
      TaggedAssemblyContext.Reset();
      UntaggedAssemblyContext.Reset();
      TaggedResultSupplementer.Reset();
      UntaggedResultSupplementer.Reset();

      testListener = new TestListener();
      var options = new RunOptions(new string[] { "foobar" }, new string[] { }, new string[0]);

      runner = new DefaultRunner(testListener, options);
    };

    When of = () =>
      runner.RunMember(typeof(context_with_multiple_specifications).Assembly,
                       typeof(context_with_multiple_specifications));

    Then should_run_untagged_assembly_context = () =>
      UntaggedAssemblyContext.OnAssemblyStartRun.ShouldBeTrue();

    Then should_run_tagged_assembly_context = () =>
      TaggedAssemblyContext.OnAssemblyStartRun.ShouldBeTrue();

    Then should_run_untagged_assembly_context_complete = () =>
      UntaggedAssemblyContext.OnAssemblyCompleteRun.ShouldBeTrue();

    Then should_run_tagged_assembly_context_complete = () =>
      TaggedAssemblyContext.OnAssemblyCompleteRun.ShouldBeTrue();

    Then should_run_untagged_global_cleanup = () =>
      UntaggedCleanup.AfterContextCleanupRunCount.ShouldBeGreaterThan(0);

    Then should_run_tagged_global_cleanup = () =>
      TaggedCleanup.AfterContextCleanupRunCount.ShouldBeGreaterThan(0);

    Then should_run_tagged_result_supplementer = () =>
      TaggedResultSupplementer.SupplementResultRun.ShouldBeTrue();

    Then should_run_untagged_result_supplementer = () =>
      UntaggedResultSupplementer.SupplementResultRun.ShouldBeTrue();

    static DefaultRunner runner;
    static TestListener testListener;
  }
  
  [Subject("Specification Runner")]
  public class when_running_with_empty_filters
  {
    Given context = () =>
    {
      testListener = new TestListener();
      var options = new RunOptions(new string[] { }, new string[] { }, new string[0]);

      runner = new DefaultRunner(testListener, options);
    };

    When of = () =>
      {
        runner.RunMember(typeof(context_with_multiple_specifications).Assembly,
                         typeof(context_with_multiple_specifications));
        runner.RunMember(typeof(context_with_duplicate_tags).Assembly,
                         typeof(context_with_duplicate_tags));
      };

    Then should_run_everything = () =>
      testListener.SpecCount.ShouldEqual(3);

    static DefaultRunner runner;
    static TestListener testListener;
  }
  
  [Subject("Specification Runner")]
  public class when_running_with_context_filters
  {
    Given context = () =>
    {
      testListener = new TestListener();
      var options = new RunOptions(new string[] {}, new string[] {}, new []{ "Machine.Specifications.Specs.context_with_multiple_specifications" });

      runner = new DefaultRunner(testListener, options);
    };

    When of = () =>
      {
        runner.RunMember(typeof(context_with_multiple_specifications).Assembly,
                         typeof(context_with_multiple_specifications));
        runner.RunMember(typeof(context_with_duplicate_tags).Assembly,
                         typeof(context_with_duplicate_tags));
      };

    Then should_run_included_contexts_only = () =>
      testListener.SpecCount.ShouldEqual(2);

    static DefaultRunner runner;
    static TestListener testListener;
  }
  
  [Subject("Specification Runner")]
  public class when_running_with_specification_filters
  {
    Given context = () =>
    {
      testListener = new TestListener();
      var options = new RunOptions(new string[] { },
                                   new string[] { },
                                   new[]
                                   {
                                     "Machine.Specifications.Specs.context_with_multiple_specifications",
                                     "Machine.Specifications.Specs.context_with_duplicate_tags"
                                   });

      runner = new DefaultRunner(testListener, options);
    };

    When of = () =>
      {
        runner.RunMember(typeof(context_with_multiple_specifications).Assembly,
                         typeof(context_with_multiple_specifications));
        runner.RunMember(typeof(context_with_duplicate_tags).Assembly,
                         typeof(context_with_duplicate_tags));
      };

    Then should_run_included_specifications_only = () =>
      testListener.SpecCount.ShouldEqual(3);

    static DefaultRunner runner;
    static TestListener testListener;
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_public_Its
    : RunnerSpecs
  {
    When of = Run<context_with_public_It_field>;

    Then should_succeed =
      () => testListener.SpecCount.ShouldEqual(1);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_protected_Its
    : RunnerSpecs
  {
    When of = Run<context_with_protected_It_field>;

    Then should_succeed =
      () => testListener.SpecCount.ShouldEqual(1);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_internal_Its
    : RunnerSpecs
  {
    When of = Run<context_with_internal_It_field>;

    Then should_succeed =
      () => testListener.SpecCount.ShouldEqual(1);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_public_Behaves_like
    : RunnerSpecs
  {
    When of = Run<context_with_public_Behaves_like_field>;

    Then should_succeed =
      () => testListener.SpecCount.ShouldEqual(1);
  }

  [Subject("Specification Runner")]
  public class when_running_a_context_with_nonprivate_framework_fields
    : RunnerSpecs
  {
    When of = Run<context_with_nonprivate_framework_fields>;

    Then should_succeed =
      () => testListener.LastResult.Passed.ShouldBeTrue();
  }

  public class RunnerSpecs
  {
    static DefaultRunner runner;
    protected static TestListener testListener;

    Given context = () =>
    {
      testListener = new TestListener();
      runner = new DefaultRunner(testListener, RunOptions.Default);
    };

    public static void Run<T>()
    {
      runner.RunMember(typeof(T).Assembly, typeof(T));
    }
  }
}
