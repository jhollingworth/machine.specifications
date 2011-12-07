using Machine.Specifications.Reporting.Integration;

namespace Machine.Specifications.Reporting.Specs.Integration
{
	[Subject(typeof(TeamCityServiceMessageWriter))]
	public class when_errors_are_reported
	{
		Given context = () =>
		{
			Writer = new TeamCityServiceMessageWriter(s => Written = s);
		};

		When of = () => Writer.WriteError("test failed", "details");

		Then should_report_an_error_string =
			() => Written.ShouldEndWith("status=\'ERROR\']");

		Then should_report_the_error_message =
			() => { Written.ShouldContain("test=\'test failed\'"); };
		
		Then should_report_error_details =
			() => { Written.ShouldContain("errorDetails=\'details\'"); };

		static string Written;
		static TeamCityServiceMessageWriter Writer;
	}
}