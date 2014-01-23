using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcovePortal.Admin
{
    public partial class ManageConditions : System.Web.UI.Page
    {
        int suggestionCounter;
        ArrayList tbList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCategories();
                tbList = new ArrayList();
                Session["tbList"] = tbList;
                suggestionCounter = 1;
                if(Request.QueryString["condition"] != null)
                {
                    GetCondition(Request.QueryString["condition"]);
                }
            }
            else
            {
                AddDynamicControls();
            }
        }

        protected void GetCondition(string condition_id)
        {
            string query = "SELECT cd.id, cd.originalText, cd.conditionDescription, cd.categoryID FROM condition_table cd ";
            query += "WHERE cd.id='" + condition_id + "'";
            DataTable dt = SqlHandler.GetData(query);

            foreach(DataRow dr in dt.Rows)
            {
                tbConditionID.Text = dr["id"].ToString();
                tbOriginalCondition.Text = dr["originalText"].ToString();
                tbDescription.Text = dr["conditionDescription"].ToString();
                ListItem categoryItem = ddl_Category.Items.FindByValue(dr["categoryID"].ToString());
                if (categoryItem != null)
                    categoryItem.Selected = true;
            }
        }

        protected void AddDynamicControls()
        {
            if (Session["tbList"] != null)
            {
                tbList = (ArrayList)Session["tbList"];
                for (int i = 0; i < tbList.Count; i++)
                {
                    TextBox tb = (TextBox)tbList[i];
                    i++;
                    tb.Text = tbList[i].ToString();
                    tb.TextMode = TextBoxMode.MultiLine;
                    Label lbl = new Label();
                    lbl.Text = "Description";
                    lbl.CssClass = "Label";
                    divSuggestions.Controls.Add(lbl);
                    divSuggestions.Controls.Add(tb);
                    divSuggestions.Controls.Add(new LiteralControl("<br />"));
                    tb = (TextBox)tbList[++i];
                    tb.Text = tbList[++i].ToString();
                    tb.TextMode = TextBoxMode.MultiLine;
                    lbl = new Label();
                    lbl.Text = "Reason";
                    lbl.CssClass = "Label";
                    divSuggestions.Controls.Add(lbl);
                    divSuggestions.Controls.Add(tb);
                    divSuggestions.Controls.Add(new LiteralControl("<br />"));
                }
            }
        }

        protected void LoadCategories()
        {
            string query = "SELECT * FROM category";
            DataTable dt = SqlHandler.GetData(query);
            foreach (DataRow dr in dt.Rows)
            {
                ListItem it = new ListItem();
                it.Text = dr["label"].ToString();
                it.Value = dr["id"].ToString();
                ddl_Category.Items.Add(it);
            }
        }

        protected void btnNewSuggestion_Click(object sender, EventArgs e)
        {
            if (ViewState["counter"] != null)
                suggestionCounter = (int)ViewState["counter"];
            suggestionCounter++;
            TextBox tb = new TextBox();
            tb.ID = "tbSuggestionDescription" + suggestionCounter.ToString();
            tb.TextMode = TextBoxMode.MultiLine;
            Label lbl = new Label();
            lbl.Text = "Description";
            lbl.CssClass = "Label";
            divSuggestions.Controls.Add(lbl);
            divSuggestions.Controls.Add(tb);
            LiteralControl lc = new LiteralControl("<br />");
            divSuggestions.Controls.Add(lc);
            tb = new TextBox();
            tb.ID = "tbSuggestionReason" + suggestionCounter.ToString();
            tb.TextMode = TextBoxMode.MultiLine;
            lbl = new Label();
            lbl.Text = "Reason";
            lbl.CssClass = "Label";
            divSuggestions.Controls.Add(lbl);
            divSuggestions.Controls.Add(tb);
            lc = new LiteralControl("<br />");
            divSuggestions.Controls.Add(lc);
            tbList = new ArrayList();
            foreach (Control c in divSuggestions.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    tbList.Add(c);
                    tbList.Add(((TextBox)c).Text);
                }
            }
            Session["tbList"] = tbList;
            ViewState["counter"] = suggestionCounter;
        }

        protected void btnSaveCondition_Click(object sender, EventArgs e)
        {
            using(MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AcoveDb"].ConnectionString))
            {
                conn.Open();
                using(MySqlCommand cmd = new MySqlCommand("sp_condition_Insert",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", MySqlDbType.VarChar,45).Value = tbConditionID.Text;
                    cmd.Parameters.Add("p_originalText", MySqlDbType.LongText).Value = tbOriginalCondition.Text;
                    cmd.Parameters.Add("p_conditionDescription", MySqlDbType.LongText).Value = tbDescription.Text;
                    cmd.Parameters.Add("p_categoryID", MySqlDbType.Int32).Value = ddl_Category.SelectedValue;
                    cmd.Parameters.Add("p_conditionException", MySqlDbType.VarChar, 255).Value = tbException.Text;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void btnSaveSuggestions_Click(object sender, EventArgs e)
        {
            InsertSuggestion(tbConditionID.Text, tbSuggestionDescription.Text, tbSuggestionReason.Text);
            string description = "";
            string reason = "";
            ArrayList al_Suggestion = new ArrayList();
            foreach (Control c in divSuggestions.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    al_Suggestion.Add(((TextBox)c).Text);
                }
            }
            for (int i = 0; i < al_Suggestion.Count; i++)
            {
                description = al_Suggestion[i].ToString();
                reason = al_Suggestion[++i].ToString();
                InsertSuggestion(tbConditionID.Text, description, reason);
            }
        }

        protected void InsertSuggestion(string id, string description, string reason)
        {
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["AcoveDb"].ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("sp_suggestion_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", MySqlDbType.VarChar, 45).Value = id;
                    cmd.Parameters.Add("p_suggestionDescription", MySqlDbType.LongText).Value = description;
                    if (reason.Trim().Length > 0)
                        cmd.Parameters.Add("p_suggestionReason", MySqlDbType.LongText).Value = reason;
                    else
                        cmd.Parameters.Add("p_suggestionReason", MySqlDbType.LongText).Value = DBNull.Value;
                    cmd.ExecuteNonQuery();
                }               
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ManageSummary.aspx");
        }
    }
}