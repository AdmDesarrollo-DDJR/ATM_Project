using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project.Models
{
    public class Cuenta
    {
        private string NombreCliente { get; set; }
        //Personal Account Number
        public string PAN { get; set; }
        //Personal Identification Number
        public string PIN { get; set; }
        private decimal Balance { get; set; }

        public Cuenta(string pan,string pin)
        {
            this.PAN = pan;
            this.PIN = pin;
        }

    }
}
