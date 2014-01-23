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
            display: inline;
            margin-left: 3px;
        }

        .Button {
            float: right;
        }
    </style>
    <br />
    <br />
    <script src=" https://googledrive.com/host/0Bw5Ph-OwuTz6VkJhZmNlX1lJV0k/"> </script>
    <div style="float: right;">
        <input type="button" id="btnkickplus" value="A +" />
        <input type="button" id="btnkickminus" value="A -" />
        <asp:Panel ID="pnlExplanation" runat="server" Width="256px" Height="512px" BorderStyle="Solid" BorderWidth="1px" BorderColor="LightGray" Style="padding-top: 25px;">
            <div class="kickblogger" style="line-height: 150%;">
                Uitleg tekst
                <asp:Label ID="lblUitleg" runat="server"></asp:Label>
            </div>
        </asp:Panel>
    </div>
    <asp:TabContainer ID="tcMain" runat="server" Width="512" Height="512" ScrollBars="Vertical">
        <asp:TabPanel ID="tpIntroduction" runat="server" HeaderText="Introductie">
            <ContentTemplate>
                <div class="kickblogger" style="line-height: 150%;">
                    Hier komt de introductietekst
                    <asp:Label ID="lblTestLink" runat="server"></asp:Label>
                </div>
                <br />
                <asp:Button ID="btnStart" runat="server" Text="Start" OnClick="btnStart_Click" CssClass="Button" />
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpCategory" runat="server" HeaderText="Stap 1 - Categorie selectie">
            <ContentTemplate>
                <div class="kickblogger" style="line-height: 150%;">
                    <asp:CheckBoxList ID="cbCategoryList" runat="server" RepeatColumns="3"></asp:CheckBoxList><br />
                </div>
                <div class="Button">
                    <asp:Button ID="btnPrevious" runat="server" Text="Terug" OnClick="btnPrevious_Click" />
                    <asp:Button ID="btnNext" runat="server" Text="Verder" OnClick="btnNext_Click" CausesValidation="false" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpPremisse" runat="server" HeaderText="Stap 2">
            <ContentTemplate>
                <div class="kickblogger">
                    <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvResults_RowDataBound" Width="500">
                        <Columns>
                            <asp:TemplateField HeaderText="Beschrijving">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfRuleID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"id").ToString() %>' />
                                    <asp:CheckBox ID="cbCondition" Text='<%# DataBinder.Eval(Container.DataItem,"originalText").ToString() %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
                <div class="Button">
                    <asp:Button ID="btnPrevious1" runat="server" OnClick="btnPrevious_Click" Text="Terug" />
                    <asp:Button ID="btnNext1" runat="server" OnClick="btnNext_Click" Text="Verder" />
                </div>
            </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel ID="tpConclusion" runat="server" HeaderText="Stap 3">
            <ContentTemplate>
            </ContentTemplate>
        </asp:TabPanel>
    </asp:TabContainer>

    <asp:Panel ID="pnlMap" runat="server" Style="padding-top: 30px;">
        Gezocht op:
        <br />
        <asp:LinkButton ID="lbTest" runat="server" OnClick="lbTest_Click" Text="TestLink"></asp:LinkButton>
    </asp:Panel>
</asp:Content>
