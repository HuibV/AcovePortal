<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AcovePortal._Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <%--<h1><%: Title %>.</h1>--%>
                <h2>Zoek informatie over uw doktersadvies.</h2>
            </hgroup>
            <p>
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        .listItem {
            font-size: small;
            font-weight: normal;
            color: blue;
        }
    </style>
    <br /><br />
    <asp:Panel ID="pnlExplanation" runat="server" Width="256px" Height="512px" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" Style="float: right;padding-top:25px;">
        Uitleg tekst
    </asp:Panel>

    <asp:TabContainer ID="tcMain" runat="server" Width="512">
        <asp:TabPanel ID="tpIntroduction" runat="server" HeaderText="Introductie">
            <ContentTemplate>
                Hier komt de introductietekst
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpCategory" runat="server" HeaderText="Stap 1 - Categorie selectie">
            <ContentTemplate>
                <asp:CheckBoxList ID="cbCategoryList" runat="server" RepeatColumns="3" Font-Bold="false" Font-Size="Small"></asp:CheckBoxList><br />
                <asp:Button ID="btnNext" runat="server" Text="Verder" OnClick="btnNext_Click" CausesValidation="false" Style="float: right;" />
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpPremisse" runat="server" HeaderText="Stap 2">
            <ContentTemplate>
                <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvResults_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCondition" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Beschrijving">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfRuleID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>' />
                                <asp:Label ID="lblDefinition" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"originalText").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpConclusion" runat="server" HeaderText="Stap 3">
            <ContentTemplate>
                Suggesties
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

    <asp:Panel ID="pnlMap" runat="server" Style="padding-top: 30px;">
        Gezocht op:
        <br />
    </asp:Panel>
</asp:Content>
