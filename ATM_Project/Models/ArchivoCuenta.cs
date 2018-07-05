using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_Project.Intefaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

namespace ATM_Project.Models
{
    public class ArchivoCuenta : IDataSource
    {
        JObject JCuentas;
        private string Ruta;
        public ArchivoCuenta(string ruta)
        {
            using (StreamReader file = File.OpenText(ruta))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                this.JCuentas = (JObject)JToken.ReadFrom(reader);
            }
            this.Ruta = ruta;
        }

        public decimal GetBalance(string PAN)
        {
            JArray cuentas = (JArray)JCuentas["Cuentas"];
            JToken result = null;
            foreach (var cuenta in cuentas)
            {
                if (result != null)
                {
                    return result["Informacion"]["Balance"].Value<decimal>();
                }
                else
                {
                    result = cuenta["NumCuenta"].Value<string>() == PAN ? cuenta : null;
                }
            }
            return -1;
        }

        public string GetPAN(string PAN)
        {
            JArray cuentas = (JArray)JCuentas["Cuentas"];
            JToken result = null;
            foreach (var cuenta in cuentas)
            {
                if (result!=null)
                {
                    return result["NumCuenta"].Value<string>();
                }
                else
                {
                    result = cuenta["NumCuenta"].Value<string>() == PAN ? cuenta : null;
                }
            }
            return "";
        }

        public string GetPIN(string PAN)
        {
            JArray cuentas = (JArray)JCuentas["Cuentas"];
            JToken result = null;
            foreach (var cuenta in cuentas)
            {
                if (result != null)
                {
                    return result["Informacion"]["PIN"].Value<string>();
                }
                else
                {
                    result = cuenta["NumCuenta"].Value<string>() == PAN ? cuenta : null;
                }
            }
            return "";
        }

        public decimal RestarBalance(string PAN,decimal monto)
        {
            var index = -1;
            decimal Balance = -1;
            JArray cuentas = (JArray)JCuentas["Cuentas"];
            JToken result = null;
            foreach (var cuenta in cuentas)
            {
                if (result != null)
                {
                    Balance = result["Informacion"]["Balance"].Value<decimal>() - monto;
                    result["Informacion"]["Balance"] = Balance;
                    break;
                }
                else
                {
                    result = cuenta["NumCuenta"].Value<string>() == PAN ? cuenta : null;
                    index++;
                }

            }
            cuentas[index] = result;
            using (StreamWriter file = File.CreateText(this.Ruta))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                JCuentas.WriteTo(writer);
            }
            return Balance;
        }

        public decimal SumarBalance(string PAN,decimal monto)
        {
            var index = -1;
            decimal Balance=-1;
            JArray cuentas = (JArray)JCuentas["Cuentas"];
            JToken result = null;
            foreach (var cuenta in cuentas)
            {
                if (result != null)
                {
                    Balance = result["Informacion"]["Balance"].Value<decimal>() + monto;
                    result["Informacion"]["Balance"] = Balance;
                    break;
                }
                else
                {
                    result = cuenta["NumCuenta"].Value<string>() == PAN ? cuenta : null;
                    index++;
                }
               
            }
            cuentas[index]=result;
            JCuentas["Cuentas"] = cuentas;
            using (StreamWriter file = File.CreateText(this.Ruta))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                JCuentas.WriteTo(writer);
            }
            return Balance;
        }
    }
}
