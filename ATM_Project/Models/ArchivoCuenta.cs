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
        public ArchivoCuenta(string ruta)
        {
            this.JCuentas=JObject.Load(new JsonTextReader(File.OpenText(ruta)));
        }

        public decimal GetBalance(string PAN)
        {
            throw new NotImplementedException();
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

        public decimal RestarBalance(string PAN)
        {
            throw new NotImplementedException();
        }

        public decimal SumarBalance(string PAN)
        {
            throw new NotImplementedException();
        }
    }
}
