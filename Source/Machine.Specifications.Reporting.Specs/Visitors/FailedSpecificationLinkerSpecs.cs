using System;

using Machine.Specifications.Reporting.Model;
using Machine.Specifications.Reporting.Visitors;

namespace Machine.Specifications.Reporting.Specs.Visitors
{
  [Subject(typeof(FailedSpecificationLinker))]
  public class when_failed_specifications_are_linked : ReportSpecs
  {
    static FailedSpecificationLinker Linker;
    static Run Report;
    static Specification Specification;
    static Specification First;
    static Specification Second;
    static Specification Last;

    Given context = () =>
      {
        Linker = new FailedSpecificationLinker();

        First = Spec("a 1 c 1 c 1 specification 1", Result.Failure(new Exception()));
        Second = Spec("a 2 c 1 c 1 specification 1", Result.Failure(new Exception()));
        Last = Spec("a 2 c 1 c 2 specification 1", Result.Failure(new Exception()));

        Report = Run(Assembly("assembly 1",
                              Concern("a 1 concern 1",
                                      Context("a 1 c 1 context 1",
                                              First,
                                              Spec("a 1 c 1 c 1 specification 2", Result.Pass())
                                        )
                                )
                       ),
                     Assembly("assembly 2",
                              Concern("a 2 concern 1",
                                      Context("a 2 c 1 context 1",
                                              Spec("a 2 c 1 c 1 specification 2", Result.Pass()),
                                              Second),
                                      Context("a 2 c 1 context 2",
                                              Last,
                                              Spec("a 2 c 1 c 2 specification 2", Result.Pass()))))
          );
      };

    When of = () => Linker.Visit(Report);

    Then should_not_assign_a__previous__link_to_the_report =
      () => Report.PreviousFailed.ShouldBeNull();

    Then should_assign_a__next__link_to_the_report =
      () => Report.NextFailed.ShouldEqual(First);

    Then should_assign_a__next__link_to_the_first_failed_spec =
      () => First.NextFailed.ShouldEqual(Second);

    Then should_not_assign_a__previous__link_to_the_first_failed_spec =
      () => First.PreviousFailed.ShouldBeNull();

    Then should_assign_a__next__link_to_the_second_failed_spec =
      () => Second.NextFailed.ShouldEqual(Last);

    Then should_assign_a__previous__link_to_the_second_failed_spec =
      () => Second.PreviousFailed.ShouldEqual(First);

    Then should_not_assign_a__next__link_to_the_last_failed_spec =
      () => Last.NextFailed.ShouldBeNull();

    Then should_assign_a__previous__link_to_the_last_failed_spec =
      () => Last.PreviousFailed.ShouldEqual(Second);
  }
}