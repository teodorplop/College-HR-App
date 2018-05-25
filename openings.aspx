<%@ Page Language="C#" Title="" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="openings.aspx.cs" Inherits="openings" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="Server">
  <asp:Button ID="LoginButton2" CssClass="btn btn-primary" runat="server" Text="Login" OnClick="LoginButton_Click"/>

  <asp:Panel ID="MainPanel" CssClass="container" runat="server">

  </asp:Panel>

  <asp:Panel ID="AddCatPanel" CssClass="container" runat="server">
    <div class="row">
      <div class="col-4">
        <asp:TextBox ID="AddCatTextBox" CssClass="form-control" runat="server" />
      </div>
      <div class="col-4">
        <asp:Button ID="AddCatBtn" CssClass="btn btn-primary" runat="server" OnClick="AddCatBtn_Click" Text="Add Category" />
      </div>
    </div>
  </asp:Panel>
</asp:Content>
