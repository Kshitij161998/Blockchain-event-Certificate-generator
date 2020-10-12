using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Certificate_generation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int success_count = 0;
            int failed_count = 0;
            SqlDataReader dr = DataflowFromSql.retrieve();
            while (dr.Read())
            {
                Candidate candidate = new Candidate();
                candidate.name = dr["Name"].ToString();
                candidate.email = dr["Email"].ToString();
                candidate.date = dr["Timestamp"].ToString();
                candidate.number = dr["Phone"].ToString();

                Blockchain blockchain = new Blockchain();
                Response res = blockchain.MakeTransaction(candidate);
                if (res.resp == true)
                {
                    DataflowFromSql.Update(res.transID, candidate.email);
                    Certificate certificate = new Certificate();
                    Response res1 = certificate.GenerateReport(candidate);
                    if (res1.resp == true)
                    {
                        success_count++;

                    }
                    else
                    {
                        failed_count++;
                    }
                }
                else
                {
                    failed_count++;
                }
            }
            Response.Text = "Certificate Generated:    SUCESS:-"+success_count.ToString()+"   Failed:-"+failed_count.ToString();
            Response.Visible = true;
            Response.ForeColor = Color.Green;
            
        }
    }
}
