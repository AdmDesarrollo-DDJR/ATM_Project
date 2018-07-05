using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Project.Intefaces;

namespace ATM_Project.Models
{
   public class ATM
    {

        private IDataSource Source;
        private int IntentosFallidos { get; set; }

        public ATM()
        {
            Source= new ArchivoCuenta("../../Data/cuentas.json");
            ReinicializarATM();
        }

        public void ReinicializarATM()
        {
            IntentosFallidos = 0;
        }

        public bool ValidarPAN(string pan)
        {
            return Source.GetPAN(pan) == pan;
        }

        public bool ValidarPIN(string pan,string pin)
        {
            return Source.GetPIN(pan) == pin;
        }


    }
}
