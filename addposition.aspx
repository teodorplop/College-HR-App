<%@ Page ValidateRequest="false" Language="C#" Title="Add Position" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addposition.aspx.cs" Inherits="addposition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
  <h3 class="h3 mb-3 font-weight-light d-block text-center">New position</h3>

  <asp:Panel ID="PublishPanel" CssClass="mx-auto w-50 mb-5" runat="server">
    <asp:TextBox ID="TextBoxTitle" CssClass="form-control d-block mb-1" runat="server" placeholder="Title"></asp:TextBox>
    <asp:TextBox ID="TextBoxProject" CssClass="form-control d-block mb-1" runat="server" placeholder="Project"></asp:TextBox>
    <asp:TextBox ID="TextBoxLocation" CssClass="form-control d-block mb-1" runat="server" placeholder="Location"></asp:TextBox>
    <div class="row">
      <div class="col-6">
        <asp:DropDownList ID="DropdownType" CssClass="form-control mb-1" runat="server">
          <asp:ListItem Text="Internship" />
          <asp:ListItem Text="Temporary" />
          <asp:ListItem Text="Full-time" />
          <asp:ListItem Text="Part-time" />
        </asp:DropDownList>
      </div>
      <div class="col-6">
        <asp:DropDownList ID="DropdownCategory" CssClass="form-control" runat="server"></asp:DropDownList>
      </div>
    </div>
    <asp:TextBox ID="TextBoxContent" CssClass="form-control d-block mb-1" runat="server" placeholder="Content" Rows="20" TextMode="Multiline"></asp:TextBox>

    <asp:Button ID="SubmitButton" CssClass="btn btn-block btn-outline-success" runat="server" Text="Submit" OnClick="SubmitButton_Click" />

  </asp:Panel>
</asp:Content>

