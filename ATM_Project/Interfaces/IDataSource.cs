using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project.Intefaces
{
    interface IDataSource
    {
        decimal GetBalance(string PAN);
        decimal SumarBalance(string PAN,decimal monto);
        decimal RestarBalance(string PAN,decimal monto);
        string GetPAN(string PAN);
        string GetPIN(string PAN);

    }
}
