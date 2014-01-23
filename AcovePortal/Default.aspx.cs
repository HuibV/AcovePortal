using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AcovePortal
{
    public partial class _Default : Page
    {
        string alphaSearch = "alpha";
        string categorySearch = "category";
        public ArrayList searchMap;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["searchMap"] != null)
                    searchMap = (ArrayList)Session["searchMap"];
                else
                {
                    searchMap = new ArrayList();
                    Session["searchMap"] = searchMap;
                }
                //AlphabetList();
                if (Request.RawUrl.Contains(alphaSearch))
                    SearchCategories(Request.QueryString[alphaSearch], null);
                else if (Request.RawUrl.Contains(categorySearch))
                    SearchCategories(null, Request.QueryString[categorySearch]);
                FillCategories();

                for (int i = 0; i < searchMap.Count; i++)
                {
                    HyperLink link = new HyperLink();
                    link.Text = searchMap[i].ToString();
                    pnlMap.Controls.Add(link);
                    if ((i + 1) < searchMap.Count)
                    {
                        LiteralControl next = new LiteralControl(">");
                        pnlMap.Controls.Add(next);
                    }
                }
            }
        }

        ///// <summary>
        ///// Fill the divLetter control with hyperlinks
        ///// </summary>
        //protected void AlphabetList()
        //{
        //    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    for (int i = 0; i < alphabet.Length; i++)
        //    {
        //        HyperLink link = new HyperLink();
        //        link.ID = "link_" + alphabet.Substring(i, 1);
        //        link.Text = alphabet.Substring(i, 1);
        //        link.NavigateUrl = "~/default.aspx?alpha=" + link.Text;
        //        divLetter.Controls.Add(link);
        //    }
        //}

        /// <summary>
        /// Fill the divOnderwerp control with all available categories
        /// </summary>
        protected void FillCategories()
        {
            string query = "SELECT id,label ";
            query += "FROM category ";
            DataTable dt = SqlHandler.GetData(query);

            foreach (DataRow dr in dt.Rows)
            {
                ListItem it = new ListItem();
                //it.Attributes.Add("class", "listItem");
                it.Text = dr["label"].ToString();
                it.Value = dr["id"].ToString();
                cbCategoryList.Items.Add(it);
            }
        }

        /// <summary>
        /// Create a query based on the hyperlink which was clicked
        /// Create a searchmap if a category was clicked
        /// Fill gridview gvResults with all found rules
        /// </summary>
        /// <param name="startLetter"></param>
        /// <param name="id"></param>
        public void SearchCategories(string startLetter, string id)
        {
            //lblSearchResult.Text = "U heeft gezocht op de letter '" + startLetter + "'";
            string query = "SELECT c.id, c.originalText,c.conditionDescription, cg.label AS Categorie ";
            query += "FROM condition_table c INNER JOIN category cg ON c.categoryID=cg.id ";
            if (startLetter != null)
                query += "WHERE cg.label LIKE '" + startLetter + "%'";
            else if (id != null)
                query += "WHERE cg.id in ('" + id + "')";
            DataTable dt = SqlHandler.GetData(query);

            if (id != null && dt.Rows.Count > 0)
            {
                if (Session["searchMap"] != null)
                    searchMap = (ArrayList)Session["searchMap"];
                searchMap.Add(dt.Rows[0]["Categorie"].ToString());
            }
            else
                searchMap = new ArrayList();

            Session["searchMap"] = searchMap;
            gvResults.DataSource = dt;
            gvResults.DataBind();

        }

        public void SearchSuggestions(string ruleID)
        {
            string query = "SELECT * FROM suggestion ";
            query += "WHERE id in ('" + ruleID + "')";
            DataTable dt = SqlHandler.GetData(query);
            int suggestionCount = 1;
            Panel suggestionPanel = new Panel();
            suggestionPanel.CssClass = "kickblogger";
            suggestionPanel.Style.Add("line-height", "150%");
            foreach(DataRow dr in dt.Rows)
            {                
                LiteralControl header = new LiteralControl("<b>Suggestion " + suggestionCount + " </b><br />");
                LiteralControl body = new LiteralControl(dr["suggestionDescription"].ToString());

                suggestionPanel.Controls.Add(header);
                suggestionPanel.Controls.Add(body);

                if(dr["suggestionReason"] != DBNull.Value)
                {
                    LiteralControl reason = new LiteralControl("<br /><b>Uitleg</b><br />" + dr["suggestionReason"].ToString() + "<br /><br />");
                    suggestionPanel.Controls.Add(reason);
                }
                suggestionCount++;
            }
            
            tpConclusion.Controls.Add(suggestionPanel);
        }

        /// <summary>
        /// Set the hyperlink for each rule on RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string rule_id = ((HiddenField)e.Row.FindControl("hfRuleID")).Value;
                //HyperLink hl = (HyperLink)e.Row.FindControl("hlExtraInformatie");
                //hl.NavigateUrl = "~/ruleInformation.aspx?ruleID=" + rule_id;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (((Button)sender).ID == btnNext.ID)
            {
                string id_list = "";
                foreach (ListItem it in cbCategoryList.Items)
                {
                    if (it.Selected)
                    {
                        if (id_list == "")
                            id_list = it.Value;
                        else
                            id_list += "','" + it.Value;
                    }
                }
                SearchCategories(null, id_list);
            }
            else if(((Button)sender).ID == btnNext1.ID)
            {
                string ruleID = "";
                foreach(GridViewRow row in gvResults.Rows)
                {
                    CheckBox cb = (CheckBox)row.FindControl("cbCondition");
                    if(cb.Checked)
                    {
                        if (ruleID.Length == 0)
                            ruleID = ((HiddenField)row.FindControl("hfRuleID")).Value;
                        else
                            ruleID += "','" + ((HiddenField)row.FindControl("hfRuleID")).Value;
                    }
                }
                SearchSuggestions(ruleID);
            }
            NextTab();
        }

        protected void NextTab()
        {
            tcMain.ActiveTabIndex = tcMain.ActiveTabIndex + 1;
        }
        protected void PreviousTab()
        {
            tcMain.ActiveTabIndex = tcMain.ActiveTabIndex - 1;
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            NextTab();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            PreviousTab();
        }

        protected void lbTest_Click(object sender, EventArgs e)
        {
            Uri testUri = new Uri("http://acove-mediawiki.herokuapp.com/api.php?format=jsonfm&action=query&titles=test&prop=revisions&rvprop=content");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(testUri);
            string explanationText;
            //request.UserAgent = "";
            //request.ContentType = "";
            using(HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                string ResponseText;
                using(StreamReader reader = new StreamReader(response.GetResponseStream()) )
                {
                    ResponseText = reader.ReadToEnd();
                }               
                int start = ResponseText.IndexOf("==");
                int end = ResponseText.IndexOf("]].") + 3;
                int length = end - start;
                explanationText = ResponseText.Substring(start, length);                   
            }
            Match match = Regex.Match(explanationText, @"\=\= [A-Za-z]{0,} \=\=");
            if(match.Success)
            {
                lblUitleg.Text = "<br />" + match;
            }
        }
    }
}