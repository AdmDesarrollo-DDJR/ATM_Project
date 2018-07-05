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
        private const int limiteIntentos=3;
        private int IntentosFallidos { get; set; }
        private Cuenta Cuenta;

        public ATM()
        {
            Source= new ArchivoCuenta("../../Data/cuentas.json");
            ReinicializarATM();
        }

        public void AumentarIntentosFallido()
        {
            IntentosFallidos++;
        }

        public bool CuentaRetenida()
        {
            return IntentosFallidos == limiteIntentos;
        }

        public void ReinicializarATM()
        {
            IntentosFallidos = 0;
        }

        public bool ValidarPAN(string pan)
        {

            return !String.IsNullOrEmpty(pan)?Source.GetPAN(pan) == pan:false;
        }

        public bool ValidarPIN(string pan,string pin)
        {
            return !String.IsNullOrEmpty(pin) ? Source.GetPIN(pan) == pin : false;
        }

        public void UsarCuenta(string pan, string pin)
        {
            Cuenta = new Cuenta(pan, pin);
        }

        public decimal GetBalance()
        {
           return Source.GetBalance(Cuenta.PAN);
        }

        public decimal Depositar(decimal Monto)
        {
            return Source.SumarBalance(Cuenta.PAN, Monto);
        }

        public decimal Retirar(decimal Monto)
        {
            return Source.RestarBalance(Cuenta.PAN, Monto);
        }


    }
}
