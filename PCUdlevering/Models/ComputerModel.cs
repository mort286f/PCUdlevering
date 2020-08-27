using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace PCUdlevering.Models
{
//#nullable enable
    public class ComputerModel
    {
        private static string connectionString = "Data Source=ZBC-E-MORT286F;Initial Catalog=PCUdlevering;Integrated Security=True";


        public static List<DAOClasses.DAOComputer> GetComputers()
        {
            List<DAOClasses.DAOComputer> computers = new List<DAOClasses.DAOComputer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT PCID, LendDate, IsLent, IsHandedIn, HandInDate FROM PCTable", connection);
                SqlDataReader rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    string id = (string)rdr["PCID"];
                    DateTime? lendDate = (rdr["LendDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)rdr["LendDate"]); //shorthand IF
                    bool isLent = (bool)rdr["IsLent"];
                    bool isHandedIn = (bool)rdr["IsHandedIn"];
                    DateTime? handInDate = (rdr["HandInDate"] == DBNull.Value) ? (DateTime?)null : ((DateTime)rdr["HandInDate"]); //shorthand IF

                    DAOClasses.DAOComputer comp = new DAOClasses.DAOComputer(id, lendDate, isLent, isHandedIn, handInDate);
                    computers.Add(comp);
                }
            }
            return computers;
        }

        public static void SetLendDate(string pcID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var ld = DateTime.Now;
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE PCTable SET LendDate = @ld WHERE PCID = @pcID", connection);
                cmd.Parameters.Add(new SqlParameter("@pcID", pcID));
                cmd.Parameters.Add(new SqlParameter("@ld", ld));
                cmd.ExecuteNonQuery();
            }
        }
    }
//#nullable restore
}