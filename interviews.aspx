<%@ Page Language="C#" Title="HR App - Interviews" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="interviews.aspx.cs" Inherits="interviews" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="Server">
  <div class="container">
    <h5>Past Interviews</h5>
    <asp:Table ID="Past_InterviewsTable" CssClass="table table-hover mb-5" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="Position" />
        <asp:TableHeaderCell runat="server" Text="Date" />
        <asp:TableHeaderCell runat="server" Text="Result" />
      </asp:TableHeaderRow>
    </asp:Table>

    <h5>Today Interviews</h5>
    <asp:Table ID="Today_InterviewsTable" CssClass="table table-hover mb-5" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="Position" />
        <asp:TableHeaderCell runat="server" Text="Result" />
      </asp:TableHeaderRow>
    </asp:Table>

    <h5>Future Interviews</h5>
    <asp:Table ID="Future_InterviewsTable" CssClass="table table-hover" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="Position" />
        <asp:TableHeaderCell runat="server" Text="Date" />
      </asp:TableHeaderRow>
    </asp:Table>

    <h5>To Be Scheduled</h5>
    <asp:Table ID="Schedule_InterviewsTable" CssClass="table table-hover" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="Position" />
        <asp:TableHeaderCell runat="server" Text="Date" />
      </asp:TableHeaderRow>
    </asp:Table>
  </div>
</asp:Content>
