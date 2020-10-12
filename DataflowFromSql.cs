using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Certificate_generation
{
    class DataflowFromSql
    {
        public static SqlDataReader retrieve()
        {
            
            SqlConnection con = new SqlConnection(@"Data Source=BT1707113\SQLEXPRESS02;Initial Catalog=ranchisummit;Integrated Security=True;Pooling=False");
            try
            {
                con.Open();
                String query = "select * from Certificate";
                SqlCommand sda = new SqlCommand(query, con);
                SqlDataReader dr = sda.ExecuteReader();
                return dr;
                
            }
            catch(SqlException sql_ex)
            {
                MessageBox.Show("SQL EXCEPTION");
                SqlDataReader dr=null;
                return dr;
            }
            catch(Exception ex)
            {
                MessageBox.Show("GENERAL EXCEPTION");
                SqlDataReader dr = null ;
                return dr;
            }
            
        }
        public static void Update(String trans_ID,string email)
        {
            SqlConnection con = new SqlConnection(@"Data Source=BT1707113\SQLEXPRESS02;Initial Catalog=ranchisummit;Integrated Security=True;Pooling=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("update Certificate set tracking_id=@transactionid where Email='" + email + "'",con);
            cmd.Parameters.Add("@transactionid",trans_ID);           
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
