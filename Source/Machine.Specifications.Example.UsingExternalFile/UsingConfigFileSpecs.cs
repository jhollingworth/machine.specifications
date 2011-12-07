using System.Configuration;

namespace Machine.Specifications.Example.UsingExternalFile
{
  [Subject("External resources usage")]
  public class when_using_test_assembly_configuration_file
  {
    Then should_be_able_to_read_application_settings =
      () => ConfigurationManager.AppSettings["key"].ShouldEqual("value");
  }
}