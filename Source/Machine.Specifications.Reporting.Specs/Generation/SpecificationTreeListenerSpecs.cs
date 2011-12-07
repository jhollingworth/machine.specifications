using System;

using Machine.Specifications.Example;
using Machine.Specifications.Reporting.Generation;
using Machine.Specifications.Runner;
using Machine.Specifications.Runner.Impl;

namespace Machine.Specifications.Reporting.Specs.Generation
{
  [Subject(typeof(SpecificationTreeListener))]
  public class when_getting_a_tree_from_a_spec_run
  {
    static DefaultRunner runner;
    static SpecificationTreeListener listener;

    Given context = () =>
      {
        listener = new SpecificationTreeListener();
        runner = new DefaultRunner(listener, RunOptions.Default);
      };

    When of =
      () => runner.RunAssembly(typeof(when_a_customer_first_views_the_account_summary_page).Assembly);

    Then should_set_the_total_specifications =
      () => listener.Run.TotalSpecifications.ShouldEqual(6);

    Then should_set_the_report_generation_date =
      () => DateTime.Now.AddSeconds(-5).ShouldBeLessThan(listener.Run.Meta.GeneratedAt);
    
    Then should_default_to_no_timestamp =
      () => listener.Run.Meta.ShouldGenerateTimeInfo.ShouldBeFalse();
    
    Then should_default_to_no_link_to_the_summary =
      () => listener.Run.Meta.ShouldGenerateIndexLink.ShouldBeFalse();
  }
}


