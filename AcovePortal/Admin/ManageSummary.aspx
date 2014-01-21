<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSummary.aspx.cs" Inherits="AcovePortal.Admin.ManageSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <%--<h1><%: Title %>.</h1>--%>
                <h2>Conditions overview</h2>
            </hgroup>
            <p>
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">  
    <h2>Condition overview</h2>
    <br />
    <asp:GridView ID="gvConditions" runat="server" AutoGenerateColumns="false" CellPadding="10" OnRowCommand="gvConditions_RowCommand">       
        <Columns>
            <asp:TemplateField HeaderText="Condition ID">
                <ItemTemplate>
                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Beschrijving">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"conditionDescription").ToString() %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CommandName="EditCondition" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br />
    <asp:Button ID="btnNewCondition" runat="server" Text="New" OnClick="btnNewCondition_Click" Style="float: right;" />
    <br />
    <h2>Edit/New category</h2>
    <br />
    <asp:GridView ID="gvCategory" runat="server"
        OnRowCommand="gvCategory_RowCommand" OnRowUpdating="gvCategory_RowUpdating" OnRowCancelingEdit="gvCategory_RowCancelingEdit"
        OnRowEditing="gvCategory_RowEditing" ShowFooter="true" AutoGenerateColumns="false">
        <EmptyDataTemplate>
            New category: &ensp;
            <asp:TextBox ID="tbNewCategory" runat="server"></asp:TextBox><br />
            Description : &ensp;
            <asp:TextBox ID="tbNewDescription" runat="server"></asp:TextBox><br />
            <asp:LinkButton ID="lbEmptyInsert" runat="server" Text="Click here to insert a new category" CommandName="InsertEmpty"></asp:LinkButton>
        </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>' />
                    <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"label").ToString() %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:HiddenField ID="hfCategoryID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>' />
                    <asp:TextBox ID="tbEditCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"label").ToString() %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="tbInsertCategory" runat="server"></asp:TextBox>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:LinkButton ID="Edit" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="Update" runat="server" Text="Update" CommandName="Update"></asp:LinkButton>
                    <asp:LinkButton ID="Cancel" runat="server" Text="Cancel" CommandName="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="Insert" runat="server" Text="Insert" CommandName="New"></asp:LinkButton>
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
