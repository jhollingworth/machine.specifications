namespace Machine.Specifications.Specs
{
  public abstract class context_that_inherits
  {
    public static int BaseEstablishRunCount;

    Given context = () => BaseEstablishRunCount++;

    protected Then should_be_inherited_but_not_executed = () => { };
    Then should_not_be_executed = () => { };
  }

  [Tags(tag.example)]
  public class context_with_inherited_specifications : context_that_inherits
  {
    public static int BecauseClauseRunCount;
    public static int EstablishRunCount;

    Given context = () => EstablishRunCount++;

    When of = () => BecauseClauseRunCount++;

    Then spec1 = () => { };
    Then spec2 = () => { };
  }

  [SetupForEachSpecification]
  [Tags(tag.example)]
  public class context_with_inherited_specifications_and_setup_for_each : context_that_inherits
  {
    public static int BecauseClauseRunCount;
    public static int EstablishRunCount;

    Given context = () => EstablishRunCount++;

    When of = () => BecauseClauseRunCount++;

    Then spec1 = () => { };
    Then spec2 = () => { };
  }
}