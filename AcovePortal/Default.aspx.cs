﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                it.Attributes.Add("class", "listItem");
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
            tcMain.ActiveTabIndex = tcMain.ActiveTabIndex + 1;
        }
    }
}