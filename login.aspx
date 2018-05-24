<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <asp:Panel ID="LoginPanel" CssClass="mx-auto w-25" runat="server">
    <asp:Label ID="Label1" CssClass="h3 mb-3 font-weight-light d-block text-center" runat="server" Text="Please sign in"></asp:Label>

    <asp:TextBox ID="LoginUsernameTextBox" placeholder="Username" CssClass="form-control d-block mb-1" runat="server"></asp:TextBox>

    <asp:TextBox ID="LoginPasswordTextBox" placeholder="Password" CssClass="form-control d-block mb-1" runat="server" TextMode="Password"></asp:TextBox>

    <asp:Button ID="LoginButton" CssClass="btn btn-sm btn-outline-success d-inline-block" runat="server" Text="Sign In" OnClick="LoginButton_Click" />

    <asp:Button ID="RegisterButton" CssClass="btn btn-sm btn-outline-dark d-inline-block float-right" runat="server" Text="Sign Up" OnClick="ShouldRegister_Click" />
  </asp:Panel>

  <asp:Panel ID="RegisterPanel" CssClass="mx-auto w-25" runat="server" Visible="false">
    <asp:Label ID="Label5" CssClass="h3 mb-3 font-weight-light d-block text-center" runat="server" Text="Please sign up"></asp:Label>

    <asp:TextBox ID="FirstNameTextBox" placeholder="First Name" CssClass="form-control d-block mb-1" runat="server"></asp:TextBox>
    <asp:TextBox ID="LastNameTextBox" placeholder="Last Name" CssClass="form-control d-block mb-1" runat="server"></asp:TextBox>
    <asp:TextBox ID="RegisterUsernameTextBox" placeholder="Username" CssClass="form-control d-block mb-1" runat="server"></asp:TextBox>
    <asp:TextBox ID="RegisterPasswordTextBox" placeholder="Password" runat="server" CssClass="form-control d-block mb-1" TextMode="Password"></asp:TextBox>

    <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-outline-success d-inline-block" Text="Sign Up" OnClick="RegisterButton_Click" />
    <asp:Button ID="Button2" runat="server" CssClass="btn btn-sm btn-outline-dark d-inline-block float-right" Text="Sign In" OnClick="ShouldLogin_Click" />
  </asp:Panel>
</asp:Content>
