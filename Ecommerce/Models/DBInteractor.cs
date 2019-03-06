using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ecommerce.Models;
using Ecommerce.DAL;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace Ecommerce.Models
{
    public class DBInteractor
    {
        private EcommerceDBContect _HdbContext = new EcommerceDBContect();
        public string GetConnectionString()

        {

            return System.Configuration.ConfigurationManager.ConnectionStrings["ServerdbConnection"].ConnectionString;

        }
        //Application Login
        public string AppLogin(LoginViewModel _data)
        {
            // UserDetails _UD = new UserDetails();
            string _UD = "";
            string flag = "";
            SqlConnection con = new SqlConnection(GetConnectionString());
            try
            {
                //var _UD2 = _HdbContext.Database.SqlQuery<UserDetails>("exec sp_UserLoginLogin '" + _data.UserEmailID + "','" + _data.Password + "'").FirstOrDefault();


                con.Open();
                string query = "sp_UserLogin";   //stored procedure Name
                SqlCommand com = new SqlCommand(query, con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userID", _data.Email);   //for username 
                com.Parameters.AddWithValue("@password", _data.Password);  //for word
                string ipaddress = GetLocalIPAddress();
                com.Parameters.AddWithValue("@deviceid", ipaddress ?? "");  //for word
                _UD = (string)com.ExecuteScalar();// for taking single value

                if (_UD.Length > 0)
                {
                    flag = _UD;
                }
            }
            catch (Exception _Ex)
            {
                return flag;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}