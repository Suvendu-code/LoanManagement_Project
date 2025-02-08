using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanManagement
{
    public partial class GenerateEMI : System.Web.UI.Page
    {
        private string selectedPlanName;
        private int tenure;
        private double roi;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPlanNames();
            }
        }
       
        private void LoadPlanNames()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LoanDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPlanMaster", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlPlanName.DataSource = reader;
                ddlPlanName.DataTextField = "PlanName";
                ddlPlanName.DataValueField = "PlanID";
                ddlPlanName.DataBind();
                ddlPlanName.Items.Insert(0, new ListItem("--Select Scheme--", ""));
            }
        }

        protected void btnCalculateEMI_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (ddlPlanName.SelectedValue == "")
                {
                    Response.Write("<script>alert('Please select a plan');</script>");
                    return;
                }

              
                if (!decimal.TryParse(TextBox1.Text.Trim(), out decimal loanAmount))
                {
                    Response.Write("<script>alert('Please enter a valid loan amount');</script>");
                    return;
                }

                
                if (string.IsNullOrWhiteSpace(TextBox2.Text) || !DateTime.TryParse(TextBox2.Text.Trim(), out DateTime loanDate))
                {
                    Response.Write("<script>alert('Please enter a valid loan date');</script>");
                    return;
                }

                
                int planID = int.Parse(ddlPlanName.SelectedValue);
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LoanDB"].ConnectionString;
                decimal emiAmount = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT ROI, Tenure FROM PlanMaster WHERE PlanID = @PlanID", conn);
                    cmd.Parameters.AddWithValue("@PlanID", planID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal roi = reader.GetDecimal(0);
                        int tenure = reader.GetInt32(1);

                      
                        emiAmount = (loanAmount + (loanAmount * roi / 100)) / tenure;

                       
                        txtTenure.Text = tenure.ToString();
                        txtROI.Text = roi.ToString("F2");
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid plan selected');</script>");
                    }
                }

                txtEMIAmount.Text = emiAmount.ToString("F2");
            }
            catch (FormatException)
            {
                Response.Write("<script>alert('Input format is incorrect');</script>");
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }
        protected void ddlPlanName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlanName.SelectedValue == "")
                {
                    txtTenure.Text = string.Empty;
                    txtROI.Text = string.Empty;
                    return;
                }
                int planID = int.Parse(ddlPlanName.SelectedValue);
                string connectionString = ConfigurationManager.ConnectionStrings["LoanDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT ROI, Tenure FROM PlanMaster WHERE PlanID = @PlanID", conn);
                    cmd.Parameters.AddWithValue("@PlanID", planID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        decimal roi = reader.GetDecimal(0);  
                        int tenure = reader.GetInt32(1);    
                        txtROI.Text = roi.ToString("F2");
                        txtTenure.Text = tenure.ToString();
                    }
                    else
                    {
                        Response.Write("<script>alert('Invalid plan selected');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error: {ex.Message}');</script>");
            }
        }



        protected void btnGenerateSchedule_Click(object sender, EventArgs e)
        {
            int planID = int.Parse(ddlPlanName.SelectedValue);
            decimal loanAmount = decimal.Parse(TextBox1.Text.Trim());
            DateTime loanDate = DateTime.Parse(TextBox2.Text.Trim());
            decimal emiAmount = decimal.Parse(txtEMIAmount.Text.Trim());
            int tenure = int.Parse(txtTenure.Text.Trim());

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LoanDB"].ConnectionString;
            DataTable dtSchedule = new DataTable();
            dtSchedule.Columns.Add("EMINo", typeof(int));
            dtSchedule.Columns.Add("DueDate", typeof(DateTime));
            dtSchedule.Columns.Add("EMIAmount", typeof(decimal));

            DateTime dueDate = loanDate.AddMonths(1);  

            for (int i = 1; i <= tenure; i++)
            {
                dtSchedule.Rows.Add(i, dueDate, emiAmount);
                dueDate = dueDate.AddMonths(1);
            }

            gvEMISchedule.DataSource = dtSchedule;
            gvEMISchedule.DataBind();
        }

    }
}
    