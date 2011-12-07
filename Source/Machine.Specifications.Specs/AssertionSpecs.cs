using System;
using System.Collections.Generic;

namespace Machine.Specifications.Specs
{
  [Subject(typeof(ShouldExtensionMethods))]
  public class when_checking_if_a_collection_contains_elements_that_match_a_func
  {
    static Exception Exception;
    static int[] Ints;

    Given context = () => { Ints = new[] {1, 2, 3}; };

    When of = () => { Exception = Catch.Exception(() => Ints.ShouldContain(x => x > 3)); };

    Then should_print_the_func_description =
      () => Exception.Message.ShouldContain("Should contain elements conforming to: x => (x > 3)");
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_checking_if_a_collection_contains_elements_that_do_not_match_a_func
  {
    static Exception Exception;
    static int[] Ints;

    Given context = () => { Ints = new[] {1, 2, 3}; };

    When of = () => { Exception = Catch.Exception(() => Ints.ShouldNotContain(x => x < 3)); };

    Then should_print_the_func_description =
      () => Exception.Message.ShouldContain("No elements should conform to: x => (x < 3)");

    Then should_print_the_elements_that_did_not_match =
      () => Exception.Message.ShouldMatch(@"does contain: {\s+\[1\],\s+\[2\]\s+}");
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_checking_if_a_collection_contains_only_elements_that_match_a_func
  {
    static Exception Exception;
    static int[] Ints;

    Given context = () => { Ints = new[] {1, 2, 3}; };

    When of = () => { Exception = Catch.Exception(() => Ints.ShouldEachConformTo(x => x % 2 == 0)); };

    Then should_print_the_func_description =
      () => Exception.Message.ShouldContain("Should contain only elements conforming to: x => ((x % 2) = 0)");

    Then should_print_the_elements_that_did_not_match =
      () => Exception.Message.ShouldMatch(@"the following items did not meet the condition: {\s+\[1\],\s+\[3\]\s+}");
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_a_list_contains_an_element_of_another_list
  {
    static Exception SpecException;
    static List<string> AList;
    static List<string> AnotherList;
    static string Element;

    Given context = () =>
    {
      Element = "An Element";
      AList = new List<string> {Element};
      AnotherList = new List<string>(AList);
    };

    When of =
      () => SpecException = Catch.Exception(() => AList.ShouldContain(AnotherList));

    Then should_pass_the__ShouldContains__assertion =
      () => SpecException.ShouldBeNull();
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_a_list_not_contains_an_element_of_another_list
  {
    static Exception SpecException;
    static List<string> AList;
    static List<string> AnotherList;
    static string Element;
    static string AnotherElement;

    Given context = () =>
    {
      Element = "An Element";
      AList = new List<string> {Element};

      AnotherElement = "Another Element";
      AnotherList = new List<string> {AnotherElement};
    };

    When of =
      () => SpecException = Catch.Exception(() => AList.ShouldContain(AnotherList));

    Then should_fail_the__ShouldContains__assertion =
      () => SpecException.ShouldBeOfType<SpecificationException>();
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_a_null_string_is_asserted_on
  {
    static string AString;

    Given context =
      () => AString = null;

    Then should_fail_the__ShouldBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldBeEmpty()).ShouldBeOfType<SpecificationException>();

    Then should_fail_the__ShouldNotBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeEmpty()).ShouldBeOfType<SpecificationException>();

    Then should_pass_the__ShouldBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldBeNull()).ShouldBeNull();

    Then should_fail_the__ShouldNotBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeNull()).ShouldBeOfType<SpecificationException>();
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_an_empty_string_is_asserted_on
  {
    static string AString;

    Given context =
      () => AString = "";

    Then should_pass_the__ShouldBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldBeEmpty()).ShouldBeNull();

    Then should_fail_the__ShouldNotBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeEmpty()).ShouldBeOfType<SpecificationException>();

    Then should_fail_the__ShouldBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldBeNull()).ShouldBeOfType<SpecificationException>();

    Then should_pass_the__ShouldNotBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeNull()).ShouldBeNull();
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_an_non_empty_string_is_asserted_on
  {
    static string AString;

    Given context =
      () => AString = "hey";

    Then should_fail_the__ShouldBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldBeEmpty()).ShouldBeOfType<SpecificationException>();

    Then should_pass_the__ShouldNotBeEmpty__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeEmpty()).ShouldBeNull();

    Then should_fail_the__ShouldBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldBeNull()).ShouldBeOfType<SpecificationException>();

    Then should_pass_the__ShouldNotBeNull__assertion =
      () => Catch.Exception(() => AString.ShouldNotBeNull()).ShouldBeNull();
  }

  [Subject(typeof(ShouldExtensionMethods))]
  public class when_a_type_assertion_fails
  {
    static string AString;
    static SpecificationException Exception;

    Given context = () => { AString = null; };

    When of =
      () => Exception = (SpecificationException) Catch.Exception(() => AString.ShouldBeOfType<int>());

    Then should_report_the_requested_type =
      () => Exception.Message.ShouldStartWith("Should be of type System.Int32");
  }
}