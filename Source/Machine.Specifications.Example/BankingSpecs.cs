using System;

namespace Machine.Specifications.Example
{
  [Subject(typeof(Account), "Funds transfer")]
  public class when_transferring_between_two_accounts
    : AccountSpecs
  {
    When of = () =>
      fromAccount.Transfer(1m, toAccount);

    Then should_debit_the_from_account_by_the_amount_transferred = () =>
      fromAccount.Balance.ShouldEqual(0m);

    Then should_credit_the_to_account_by_the_amount_transferred = () =>
      toAccount.Balance.ShouldEqual(2m);
  }

  [Subject(typeof(Account), "Funds transfer"), Tags("failure")]
  public class when_transferring_an_amount_larger_than_the_balance_of_the_from_account
    : AccountSpecs
  {
    static Exception exception;
    When of =()=>
      exception = Catch.Exception(()=>fromAccount.Transfer(2m, toAccount));

    Then should_not_allow_the_transfer =()=>
      exception.ShouldBeOfType<Exception>();
  }

  public class failure {}

  public abstract class AccountSpecs
  {
    protected static Account fromAccount;
    protected static Account toAccount;

    Given context =()=>
    {
      fromAccount = new Account {Balance = 1m};
      toAccount = new Account {Balance = 1m};
    };
  }
}
