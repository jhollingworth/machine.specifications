using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Machine.Specifications.GallioAdapter.TestResources
{ 
  [Subject("Scott Bellware")]
  public class at_any_given_moment
  {
    Then will_fail = () =>
    {
      throw new Exception("hi scott, love you, miss you.");
    };
  }

  [Tags("example")]
  public class failing_specification_assertions
  {
    Then failing_boolean_assertion = () => false.ShouldBeTrue();
    Then failing_equality_assertion = () => 1.ShouldEqual(2);
    Then failing_contains_assertion = () => new int[] { 1, 2, 3, 5, }.ShouldContain(4);    
  }
}
