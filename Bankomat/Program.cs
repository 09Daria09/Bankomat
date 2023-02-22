using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatHW
{
    class Program
    {
        public class BankAccount
        {
            Money money = new Money();
            public string FullName { get; set; }
            public string AccountNumber { get; set; }
            public BankAccount(string accountNumber, string fullName, string currency)
            {
                AccountNumber = accountNumber;
                money.Balance = 0.0;
                money.Currency = currency;
                FullName = fullName;
            }
            public void Refill(double amount)
            {
                money.Balance += amount;
            }
            public void Pay(double amount)
            {
                if (money.Balance >= amount)
                {
                    money.Balance -= amount;
                }
                else
                {
                    throw new ArgumentException("Недостаточно денег");
                }
            }
            public double GetBalance()
            {
                return money.Balance;
            }
            public string GetCorrency()
            {
                return money.Currency;
            }

        }
        public class Money
        {
            public double Balance { get; set; }
            public string Currency { get; set; }
            public Money()
            {
                Currency = null;
                Balance = 0;
            }
            public Money(string currency, double balance)
            {
                Balance = balance;
                Currency = currency;
            }
        }
        public interface IPin
        {
            int PinCode(int pin);
        }
        abstract public class ICheck
        {
            abstract public void Print();
            virtual public void PrintCheck()
            {

            }
        }
        class Bankomat:ICheck, IPin 
        {
            public int Pin { get; set; }
            private BankAccount account;
            public double Spent { get; set; }
            public Bankomat(BankAccount account)
            {
                this.account = account;
                Spent = 0.0;
            }
            public void Refill(double amount)
            {
                account.Refill(amount);
            }
            public void Pay(double amount)
            {
                account.Pay(amount);                
                Spent = amount;
            }
            public double GetBalance()
            {
                return account.GetBalance();
            }
            public string GetCurrency()
            {
                return account.GetCorrency();
            }
            public int PinCode(int pin)
            {
                return Pin = pin;
            }           
            public void PrintCheck()
            {
                Console.WriteLine($"Вы потратили: {Spent}\nБаланс: {GetBalance()} {GetCurrency()}");
            }
            public override void Print()
            {
                Console.WriteLine($"ФИО: {account.FullName}\nНомер карты: {account.AccountNumber}\nБаланс: {GetBalance()} {GetCurrency()}");
            }
        }

        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("12345674222324555556", "Daria Kukuruza", "UAN");
            Bankomat bankomat = new Bankomat(account);
            bankomat.Refill(1200);
            bankomat.Print();
            bankomat.Pay(34.56);
            bankomat.PrintCheck();

        }
    }
}
