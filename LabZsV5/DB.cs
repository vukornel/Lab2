using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LabZsV
{
    class DB
    {
        public void DBquery()
        {
            string connetionString = null;
            connetionString = @"Data Source=S4\VB;Initial Catalog=Labirintus;Trusted_Connection=True;";
            SqlConnection cnn = new SqlConnection(connetionString);
            SqlCommand command;

            try
            {
                cnn.Open();
                string sql = "SELECT TOP 3 Score, Login, TipTimeSum FROM dbo.Users ORDER BY Score DESC;";
                command = new SqlCommand(sql, cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine("Nev: " + reader.GetValue(1) + " Pontok: " + reader.GetValue(0) + " Ido: " + reader.GetValue(2));
                    reader.Read();
                    Console.WriteLine("Nev: " + reader.GetValue(1) + " Pontok: " + reader.GetValue(0) + " Ido: " + reader.GetValue(2));
                    reader.Read();
                    Console.WriteLine("Nev: " + reader.GetValue(1) + " Pontok: " + reader.GetValue(0) + " Ido: " + reader.GetValue(2));
                }
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Console.ReadLine();
            }


            try
            {
                cnn.Open();
                string sql = "SELECT Login, Score, TipTimeSum FROM dbo.Users WHERE Login = '" + Environment.UserName + "';";
                command = new SqlCommand(sql, cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    Console.WriteLine("Nev: " + reader.GetValue(0) + " Pontok: " + reader.GetValue(1) + " Ido: " + reader.GetValue(2));
                }
                command.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Console.ReadLine();
            }
        }

        public void DBconnect(int pont, int ido, string nev)
        {
            SqlConnection cnn;
            string connetionString = @"Data Source=S4\VB;Initial Catalog=Labirintus;Trusted_Connection=True;";
            cnn = new SqlConnection(connetionString);
            SqlCommand command;

            try
            {
                cnn.Open();
                if (pont != 0)
                {
                    string sql = "UPDATE dbo.Users SET Login = '" + Environment.UserName + "', Score = " + pont + ", TipTimeSum = " + ido + ", DisplayName = '" + nev + "' WHERE Login = '" + Environment.UserName + "'" + ";";
                    command = new SqlCommand(sql, cnn);
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                Console.ReadLine();
            }
        }
    }
}
