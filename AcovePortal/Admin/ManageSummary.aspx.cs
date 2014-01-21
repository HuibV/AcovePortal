using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcovePortal.Admin
{
    public partial class ManageSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid(gvConditions, "condition_table");
                BindGrid(gvCategory,"category");
            }
        }

        protected void BindGrid(GridView gv, string table)
        {
            string query = "SELECT * FROM " + table;
            DataTable dt = SqlHandler.GetData(query);
            gv.DataSource = dt;
            gv.DataBind();
        }

        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "InsertEmpty")
            {
                string label = ((TextBox)gvCategory.Controls[0].Controls[0].FindControl("tbNewCategory")).Text;
                string query = "INSERT INTO category(label) VALUES('" + label + "')";
                SqlHandler.ExecuteQuery(query);

                BindGrid(gvCategory,"category");
            }
            else if (e.CommandName == "New")
            {
                string label = ((TextBox)gvCategory.FooterRow.FindControl("tbInsertCategory")).Text;
                string query = "INSERT INTO category(label) VALUES('" + label + "')";
                SqlHandler.ExecuteQuery(query);

                BindGrid(gvCategory, "category");
            }
        }

        protected void gvCategory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategory.EditIndex = e.NewEditIndex;
            BindGrid(gvCategory, "category");
        }

        protected void gvCategory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategory.EditIndex = -1;
            BindGrid(gvCategory, "category");
        }

        protected void gvCategory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string category_id = ((HiddenField)gvCategory.Rows[e.RowIndex].FindControl("hfCategoryID")).Value;
            string label = ((TextBox)gvCategory.Rows[e.RowIndex].FindControl("tbEditCategory")).Text;
            string query = "UPDATE category SET label='" + label + "' WHERE id='" + category_id + "'";
            SqlHandler.ExecuteQuery(query);

            gvCategory.EditIndex = -1;

            BindGrid(gvCategory, "category");
        }

        protected void btnNewCondition_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ManageConditions.aspx");
        }

        protected void gvConditions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="EditCondition")
            {                
                Response.Redirect("~/Admin/ManageConditions.aspx?condition=" + e.CommandArgument.ToString());
            }
        }

    }
}