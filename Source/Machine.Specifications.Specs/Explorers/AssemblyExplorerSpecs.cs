using System.Collections.Generic;
using System.Linq;

using Machine.Specifications.Explorers;
using Machine.Specifications.Model;

namespace Machine.Specifications.Specs.Explorers
{
  [Subject(typeof(AssemblyExplorer))]
  public class When_inspecting_internal_types_for_contexts
  {
    static AssemblyExplorer Explorer;
    static IEnumerable<Context> Contexts;
    Given context = () => { Explorer = new AssemblyExplorer(); };

    When of =
      () => { Contexts = Explorer.FindContextsIn(typeof(tag).Assembly, "Machine.Specifications.Specs.Internal"); };

    Then should_find_two_contexts =
      () => Contexts.Count().ShouldEqual(2);
  }
}