<%@ Page Language="C#" Title="HR App - Job Description" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="job.aspx.cs" Inherits="job" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="Server">
  <asp:Panel ID="BodyPanel" runat="server" CssClass="container">
    <asp:Panel ID="DescriptionPanel" runat="server">
    </asp:Panel>

    <h5 class="mt-3">Candidates</h5>

    <asp:Table ID="CandidatesTablePanel" CssClass="table table-hover" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="College" />
        <asp:TableHeaderCell runat="server" />
      </asp:TableHeaderRow>
    </asp:Table>

    <asp:Table ID="AddCandidatesTable" CssClass="table table-hover" runat="server">
      <asp:TableRow runat="server">
        <asp:TableCell runat="server"><asp:TextBox ID="TextBoxName" CssClass="form-control" runat="server"/></asp:TableCell>
        <asp:TableCell runat="server"><asp:TextBox ID="TextBoxEmail" CssClass="form-control" runat="server"/></asp:TableCell>
        <asp:TableCell runat="server"><asp:TextBox ID="TextBoxCollege" CssClass="form-control" runat="server"/></asp:TableCell>
        <asp:TableCell runat="server"><asp:Button ID="AddCandBtn" CssClass="btn btn-primary" runat="server" Text="Add Candidate" OnClick="AddCandBtn_Click"/></asp:TableCell>
      </asp:TableRow>
    </asp:Table>
  </asp:Panel>
</asp:Content>
