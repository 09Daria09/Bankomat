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
            public string FullName { get; set; }
            public string AccountNumber { get; set; }
            public string PinCode { get; set; }
            public BankAccount(string accountNumber, string fullName, string pinCode)
            {
                AccountNumber = accountNumber;
                FullName = fullName;
                PinCode = pinCode;
            }
            public BankAccount()
            {
                AccountNumber = null;
                FullName = null;
                PinCode = null;
            }
        }
        ///
        public interface IMoney
        {
            void Refill();
            void Pay();
            double ShowPrice();
            string ShowCorrency();
            double ShowBalance();
            void Convert(IMoney money);
        }
        public class USD: IMoney
        {
            public double Balance { get; set; }
            public string Currency { get; set; }
            public double Price { get; set; }
            public USD(double balance, double price)
            {
                Balance = balance;
                Currency = "USD";
                Price = price;
            }
            public USD()
            {
                Balance = 0;
                Currency = "USD";
                Price = 0;
            }
            public void Refill()
            {
                Balance += Price;
            }
            public void Pay()
            {
                if (Balance >= Price)
                {
                    Balance -= Price;
                }
                else
                {
                    throw new ArgumentException("Недостаточно денег");
                }
            }

            public double ShowPrice()
            {
                return Price;
            }
            public string ShowCorrency()
            {
                return Currency;
            }

            public void Convert(IMoney money)
            {
                EUR eur = new EUR();
                if (money.ShowCorrency() == eur.ShowCorrency())
                {
                    Price *= 0.94;
                    Currency = eur.ShowCorrency();
                }
                else
                {
                    Price *= 36.74;
                    Currency = "UAH";
                }
            }

            public double ShowBalance()
            {
                return Balance;
            }
        }
        public class EUR : IMoney
        {
            public double Balance { get; set; }
            public string Currency { get; set; }
            public double Price { get; set; }
            public EUR(double balance, double price)
            {
                Balance = balance;
                Currency = "EUR";
                Price = price;
            }
            public EUR()
            {
                Balance = 0;
                Currency = "EUR";
                Price = 0;
            }
            public void Refill()
            {
                Balance += Price;
            }
            public void Pay()
            {
                if (Balance >= Price)
                {
                    Balance -= Price;
                }
                else
                {
                    throw new ArgumentException("Недостаточно денег");
                }
            }
            public double ShowPrice()
            {
                return Price;
            }
            public string ShowCorrency()
            {
                return Currency;
            }
            public double ShowBalance()
            {
                return Balance;
            }
            public void Convert(IMoney money)
            {
                USD usd = new USD();
                if(money.ShowCorrency() == usd.ShowCorrency())
                {
                    Price *= 1.06;
                    Currency = usd.ShowCorrency() ;
                }
                else
                {
                    Price *= 0.027;
                    Currency = "UAH";
                }
            }

          
        }
        public class UAH : IMoney
        {
            public double Balance { get; set; }
            public string Currency { get; set; }
            public double Price { get; set; }

            public UAH(double balance, double price)
            {
                Balance = balance;
                Currency = "UAH";
                Price = price;
            }
            public UAH()
            {
                Balance = 0;
                Currency = "UAH";
                Price = 0;
            }
            public void Refill()
            {
                Balance += Price;
            }
            public void Pay()
            {
                if (Balance >= Price)
                {
                    Balance -= Price;
                }
                else
                {
                    throw new ArgumentException("Недостаточно денег");
                }
            }
            public double ShowPrice()
            {
                return Price;
            }
            public string ShowCorrency()
            {
                return Currency;
            }
            public double ShowBalance()
            {
                return Balance;
            }

            public void Convert(IMoney money)
            {
                USD usd = new USD();
                if (money.ShowCorrency() == usd.ShowCorrency())
                {
                    Price *= 0.027;
                    Currency = usd.ShowCorrency();
                }
                else
                {                    
                    Price *= 0.026;
                    Currency = "EUR";
                }
            }
        }
        ///
        public interface IPaymentInformation
        {
            void Print(BankAccount account, IMoney money);
        }
        class SMS : IPaymentInformation
        {
            public string YourPhone { get; set; }
            public SMS(string yourPhone)
            {
                YourPhone = yourPhone;
            }
            public void Print(BankAccount account, IMoney money)
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Почта: {YourPhone}\nИмя покупателя {account.FullName}\nTранзакция в размере: {money.ShowPrice()} {money.ShowCorrency()}" +
                    $"\nВремя транзакции: {currentTime.ToString("HH:mm:ss")}");
            }
        }
        class MailCheck : IPaymentInformation
        {
            public string YourEmail { get; set; }

            public MailCheck(string yourEmail)
            {
                YourEmail = yourEmail;
            }
            public void Print(BankAccount account, IMoney money)
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Номер телефона: {YourEmail}\nИмя покупателя {account.FullName}\nTранзакция в размере: {money.ShowPrice()} {money.ShowCorrency()}" +
                    $"\nВремя транзакции: {currentTime.ToString("HH:mm:ss")}");
            }
        }
        class PrintCheck : IPaymentInformation
        {
            public void Print(BankAccount account, IMoney money)
            {
                DateTime currentTime = DateTime.Now;
                Console.WriteLine($"Имя покупателя {account.FullName}\nTранзакция в размере: {money.ShowPrice()} {money.ShowCorrency()}" +
                    $"\nВремя транзакции: {currentTime.ToString("HH:mm:ss")}");
            }
        }
        ///
        class Bankomat
        {   
            public void Print(BankAccount account, IMoney money,IPaymentInformation payment)
            {
                payment.Print(account, money);
            }
            public void PrintBalancy(IMoney money)
            {
                money.ShowBalance();
            }
            public void Convert(IMoney money, IMoney money2)
            {
                money.Convert(money2);
            }
        }

        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("123 456 742 223 245 555", "Daria Kukuruza", "14-53");
            UAH money = new UAH(2000, 50);

            PrintCheck check = new PrintCheck();
            Bankomat bankomat = new Bankomat();
            bankomat.Print(account, money, check);

            EUR eur = new EUR();
            bankomat.Convert(money,eur);

            bankomat.Print(account, money, check);
        }
    }
}
