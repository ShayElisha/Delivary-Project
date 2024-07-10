using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using BLL;
using Google.Apis.Admin.Directory.directory_v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Delivery_Project.AdminManager
{
    public partial class salariesManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Salaries> salaries = Salaries.GetAll();
            RptProd.DataSource = salaries;
            RptProd.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button btnSave = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnSave.NamingContainer;

            TextBox txtBonuse = (TextBox)item.FindControl("TxtBonuse");
            TextBox txtReport = (TextBox)item.FindControl("txtReport");

            if (string.IsNullOrWhiteSpace(txtBonuse.Text) || string.IsNullOrWhiteSpace(txtReport.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('שדות חובה יש למלא');", true);
                return;
            }

            string SalaryId = btnSave.CommandArgument;
            int SalaryID = int.Parse(SalaryId);

            Salaries salary = Salaries.GetBySalaryId(SalaryID);
            if (salary != null)
            {
                if (salary.DeliaryAmount > 20)
                {
                salary.Bonuse = Decimal.Parse(txtBonuse.Text);
                }
                else
                {
                    salary.Bonuse = 0;
                }
                salary.Report = int.Parse(txtReport.Text);
                salary.salary = 200 * salary.DeliaryAmount + salary.Bonuse - salary.faults * salary.Report;
                salary.Save();
                Application["Salaries"] = Salaries.GetAll();
                Response.Redirect("SalariesManager.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('שגיאה: לא נמצא אובייקט המשכורת');", true);
            }
        }

    }
}