using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanManagement
{
    public partial class PlanMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string planName = txtPlanName.Text.Trim();
            int tenure = int.Parse(txtTenure.Text.Trim());
            decimal roi = decimal.Parse(txtROI.Text.Trim());

         
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LoanDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertPlanMaster", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PlanName", planName);
                cmd.Parameters.AddWithValue("@Tenure", tenure);
                cmd.Parameters.AddWithValue("@ROI", roi);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

           
            txtPlanName.Text = string.Empty;
            txtTenure.Text = string.Empty;
            txtROI.Text = string.Empty;
            Response.Write("<script>alert('Plan saved successfully!');</script>");
        }
    }
}