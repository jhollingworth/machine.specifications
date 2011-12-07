using System.IO;

namespace Machine.Specifications.Example.UsingExternalFile
{
  [Subject("External resources usage")]
  public class when_using_file_copied_to_assembly_output_directory
  {
    Then _shouldBeAbleToLocateThenByRelativePath = () => 
      File.Exists("TestData.txt").ShouldBeTrue();
  }
}