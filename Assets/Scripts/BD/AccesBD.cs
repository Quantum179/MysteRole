using System;
using System.Data.SQLClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDUtils
{
    class AccesBD
    {
        static readonly SqlConnection myConn = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\Github\\MysteRole\\Assets\\BD_Mysterole.mdf; Integrated Security = True; Connect Timeout = 30");
        static Dictionary<string,string>[] SELECT(string select)
        {
            Dictionary<string, string>[] retrun = { };

            try
            {
                myConn.Open();

                SqlCommand myComm = new SqlCommand(select, myConn);

                SqlDataReader myRead = myComm.ExecuteReader();

                while (myRead.Read())
                {
                    retrun[retrun.Count()] = new Dictionary<string, string>();
                    for (int i = 0; i < myRead.FieldCount; i++)
                    {
                        retrun[retrun.Count() - 1].Add(myRead.GetName(i), myRead[i].ToString());
                    }
                }
            }
            finally
            {
                myConn.Close();
            }

            return retrun;
        }
    }
}
