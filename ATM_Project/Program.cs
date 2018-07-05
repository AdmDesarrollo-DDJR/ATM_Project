using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Project.Models;
using System.Threading;

namespace ATM_Project
{
    class Program
    {
        static ATM Atm;
        
        static void Main(string[] args)
        {
            Atm = new ATM();
            while (true)
            {
                Atm.ReinicializarATM();
                    if (IsAuthenticated())
                    {
                         SeleccionarOpcion();
                    }
                    else
                    {
                        Console.WriteLine("Invalid ATM card. It will be retained");
                        Thread.Sleep(3000);
                }
            }
        }

        private static bool IsAuthenticated()
        {
            Console.Clear();
            Console.WriteLine("Welcome\nplease insert your ATM card");
            string pan = Console.ReadLine();

            if (Atm.ValidarPAN(pan))
            {
                while (!Atm.CuentaRetenida())
                {
                    Console.Clear();
                    Console.WriteLine("Please enter your PIN");
                    string pin = Console.ReadLine();
                    if (Atm.ValidarPIN(pan, pin))
                    {
                        Atm.UsarCuenta(pan, pin);
                        return true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Your PIN is incorrect.\nPlease try again.");
                        Thread.Sleep(3000);
                        Atm.AumentarIntentosFallido();
                        if (Atm.CuentaRetenida())
                            break;
                    }
                }
            }
            return false;
        }

        private static void SeleccionarOpcion()
        {
            Console.WriteLine("Select transaction:");
            int selectedOption = ConsoleHelper.MultipleChoice(true, "Balance>", "Deposit>", "Withdrawal>");
            switch (selectedOption)
            {
                case 0: Balance(); break;
                case 1: Deposit(); break;
                case 2: Withdrawal(); break;
                case -1: break;
            }
        }

        private static void Balance()
        {
            Console.Clear();
            Console.WriteLine("Balance is \n $"+Atm.GetBalance());
            Console.WriteLine("Your new balance is being printed.Another transaction?");
            var response = Console.ReadLine();
            if (response == "Y")
                SeleccionarOpcion();
            else
                Finish();
        }
        private static void Deposit()
        {
            Console.Clear();
            try
            {
                Console.Clear();
                Console.WriteLine("Enter amount.\n Withdrawls must be multiples of $10");
                decimal Monto = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Please insert deposit into deposit slot");
                Thread.Sleep(5000);
                Console.WriteLine("Your new balance is being printed.Another transaction?");
                var response = Console.ReadLine();
                if (response == "Y")
                    SeleccionarOpcion();
                else
                    Finish();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Temporarly unable to process deposits\n Another Transaction?");
                var response = Console.ReadLine();
                if (response == "Y")
                    SeleccionarOpcion();
                else
                    Finish();
            }
            Thread.Sleep(3000);
        }
        private static void Withdrawal()
        {
            Console.Clear();
            try
            {
                Console.WriteLine("Enter amount.\n Withdrawls must be multiples of $10");
                decimal Monto = decimal.Parse(Console.ReadLine());
                if (Atm.GetBalance() < Monto)
                {
                    Console.WriteLine("Insufficient funds!\nPlease enter a new amount");
                }
                else
                {
                    if (Monto % 10 != 0)
                    {
                        Console.WriteLine("Machine can only dispense $10 notes");
                    }
                    else
                    {
                        Atm.Retirar(Monto);
                        Console.WriteLine("Your balance is being updated. Please take cash from dispenser");
                        var response = Console.ReadLine();
                        if (response == "Y")
                            SeleccionarOpcion();
                        else
                            Finish();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Temporarly unable to process withdrawals\n Another Transaction?");
                var response = Console.ReadLine();
                if (response == "Y")
                    SeleccionarOpcion();
                else
                    Finish();
            }
            Thread.Sleep(3000);

        }

        private static void Finish()
        {
            Console.WriteLine("Please take your receipt and ATM card\n Thank you.");
            Thread.Sleep(3000);
        }
    }
}
