<%@ Page Language="C#" Title="HR App - Employees" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="employees.aspx.cs" Inherits="employees" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="Server">
  <asp:Panel ID="EmpMainPanel" CssClass="container" runat="server">
    <h5>Employees</h5>
    <asp:Table ID="Employees_Table" CssClass="table table-hover mb-5" runat="server">
      <asp:TableHeaderRow runat="server">
        <asp:TableHeaderCell runat="server" Text="Name" />
        <asp:TableHeaderCell runat="server" Text="Email" />
        <asp:TableHeaderCell runat="server" Text="Position" />
        <asp:TableHeaderCell runat="server" Text="Hire Date" />
      </asp:TableHeaderRow>
    </asp:Table>
  </asp:Panel>
</asp:Content>
