<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageConditions.aspx.cs" Inherits="AcovePortal.Admin.ManageConditions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Label {
            vertical-align: top;
            margin-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Category</h2>
    <br />
    <asp:DropDownList ID="ddl_Category" runat="server"></asp:DropDownList>
    <br />
    <br />
    <h2>Condition</h2>
    <br />
    <asp:Label ID="lblConditionID" runat="server" Text="Condition number" CssClass="Label"></asp:Label>
    <asp:TextBox ID="tbConditionID" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblOriginalCondition" runat="server" Text="Original condition" CssClass=" Label"></asp:Label>
    <asp:TextBox ID="tbOriginalCondition" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="lblDescription" runat="server" Text="Condition description" CssClass="Label"></asp:Label>
    <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Label ID="lblException" runat="server" Text="Condition exception" CssClass="Label"></asp:Label>
    <asp:TextBox ID="tbException" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="btnSaveCondition" runat="server" OnClick="btnSaveCondition_Click" Text="Save condition" />
    <br />
    <h2>Suggestions</h2>
    <br />
    <asp:Label ID="lblSuggestionDescription" runat="server" Text="Description" CssClass="Label"></asp:Label>
    <asp:TextBox ID="tbSuggestionDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:Label ID="lblSuggestionReason" runat="server" Text="Reason" CssClass="Label"></asp:Label>
    <asp:TextBox ID="tbSuggestionReason" runat="server" TextMode="MultiLine"></asp:TextBox>
    <div id="divSuggestions" runat="server">
    </div>
    <asp:Button ID="btnNewSuggestion" runat="server" Text="+ New" OnClick="btnNewSuggestion_Click" Style="float: right;" />
    <br />
    <asp:Button ID="btnSaveSuggestions" runat="server" Text="Save suggestions" OnClick="btnSaveSuggestions_Click" />
    <asp:Button ID="btnReturn" runat="server" Text="Return to summary" OnClick="btnReturn_Click" />
</asp:Content>
