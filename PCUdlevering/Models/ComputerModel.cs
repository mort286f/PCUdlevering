﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

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
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE PCTable SET LendDate = @ld, HandInDate = @hd, IsLent = @il WHERE PCID = @pcID", connection);
                cmd.Parameters.Add(new SqlParameter("@il", true));
                cmd.Parameters.Add(new SqlParameter("@pcID", pcID));
                cmd.Parameters.Add(new SqlParameter("@ld", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@hd", DBNull.Value));
                cmd.ExecuteNonQuery();
            }
        }

        public static void SetHandInDate(string pcID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE PCTable SET HandInDate = @hd, LendDate = @ld, IsLent = @il WHERE PCID = @pcID", connection);
                cmd.Parameters.Add(new SqlParameter("@il", false));
                cmd.Parameters.Add(new SqlParameter("@pcID", pcID));
                cmd.Parameters.Add(new SqlParameter("@hd", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@ld", DBNull.Value));
                cmd.ExecuteNonQuery();
            }
        }
    }
//#nullable restore
}