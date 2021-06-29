using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseValidationAPI.Model
{
    public class DBManager
    {
        private static string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LicenseAPI_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static SqlConnection con= new SqlConnection(constr);
        
        public static string getLicenseStatus(string SoftwareSerialNumber)
        {
            string result = "Expired";
            DateTime startDate;
            DateTime dateTime = DateTime.Today;
            SqlCommand com = new SqlCommand("GetLicense", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@LicenseNumber",SoftwareSerialNumber);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["StartDate"].ToString() == "")
                {
                    startDate = DateTime.Today;
                    SqlCommand comm = new SqlCommand("InitializeLicense", con);
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@LicenseNumber",SoftwareSerialNumber);
                    con.Open();
                    int i = comm.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    startDate = (DateTime)dt.Rows[0]["StartDate"];
                }
                int diff = (int)(dateTime - startDate).TotalDays;
                if (diff <= 30) { result = "Valid"; }
                else if(diff>30 && dt.Rows[0]["LicenseType"].ToString().Equals("Registered")) { result = "Valid"; }
                else { result = "Expired"; }
            }
            else { result = "Invalid Product Serial Number"; }
            return result;
        }
    }
}
