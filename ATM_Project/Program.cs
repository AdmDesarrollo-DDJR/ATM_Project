using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Project.Models;

namespace ATM_Project
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ATM Atm = new ATM();
            Console.WriteLine("Welcome\nplease insert your ATM card");
            string pan = Console.ReadLine();
            if (Atm.ValidarPAN(pan))
            {
                Console.Clear();
                Console.WriteLine("Please enter your PIN");
                string pin = Console.ReadLine();
                if (Atm.ValidarPIN(pan, pin))
                {
                    Console.WriteLine("Select transaction:");
                    int selectedOption = ConsoleHelper.MultipleChoice(true,"Balance>","Deposit>","Withdrawal>");
                    switch (selectedOption)
                    {
                        case 0: Balance(); break;
                        case 1: Deposit(); break;
                        case 2: Withdrawal(); break;
                        case -1:break;
                    }
                }
                else
                {

                }
            }
            else
            {
                Console.WriteLine("Invalid ATM card. It will be retained");
            }

        }

        private static void Balance()
        {

        }
        private static void Deposit()
        {

        }
        private static void Withdrawal()
        {

        }
    }
}
