using System;
using System.Data.SqlClient;
//using MySql.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;

namespace BDUtils
{
    class AccesBD
    {


       static SqlConnection myConn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\GitHub\\MysteRole\\Assets\\BD_Mysterole.mdf;Integrated Security=True;Connect Timeout=30");
       
        

        public static Dictionary<string,string>[] SELECT()
        {
            Dictionary<string, string>[] retrun = { };

            myConn.ConnectionString = "Data Source=.\\SQLExpress;" +
                  "User Instance=true;" +
                  "Integrated Security=true;" +
                  "AttachDbFilename=|DataDirectory|BD_Mysterole.mdf;";
            myConn.Open();


            /*Debug.Log(myConn.DataSource);
            Debug.Log(myConn.ConnectionString.ToString()); 

            try
            {
                myConn.Open();
                Debug.Log(myConn.State);

                SqlCommand myComm = new SqlCommand("SELECT * FROM Stats;", myConn);

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
            */
            return retrun;
        }
        public static void OpenConnection()
        {
            //myConn.
        }
    }
}
