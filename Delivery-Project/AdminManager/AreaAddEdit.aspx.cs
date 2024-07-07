using BLL;
using DATA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Delivery_Project.AdminManager
{
    public partial class AreaAddEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCities();
            }
        }
        private void LoadCities()
        {
            List<Cities> cities = Cities.GetAll();
            TxtCities.DataSource = cities;
            TxtCities.DataTextField = "CityName";
            TxtCities.DataValueField = "CityId";
            TxtCities.DataBind();

            // הוסף אופציה ברירת מחדל לבחירת עיר
            TxtCities.Items.Insert(0, new ListItem("בחר עיר", "0"));
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strCitiesList = "";
            Area area = new Area();
            if (hidCid.Value == "-1")
            {
                area.AreaId = -1; // עיר חדשה
            }
            else
            {
                area.AreaId = int.Parse(hidCid.Value); // עיר קיימת
            }
            for (int i = 0; i < TxtCities.Items.Count; i++)
            {
                if (TxtCities.Items[i].Selected)
                {
                    strCitiesList += TxtCities.Items[i].Value + ",";
                }
            }
            string str= strCitiesList.Substring(0,strCitiesList.Length-1);
                string sql = "INSERT INTO AreaList (NameArea, ListCities) VALUES ";
                sql += $"(N'{TxtCname.Text}', N'{str}')";

                DbContext Db = new DbContext();
                Db.ExecuteNonQuery(sql);
            }

        }
    }

