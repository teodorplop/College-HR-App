<%@ Page Language="C#" Title="" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="openings.aspx.cs" Inherits="openings" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="body" runat="Server">
  <asp:Panel ID="MainPanel" CssClass="container" runat="server">
    <asp:Label ID="OpenPositions" runat="server"></asp:Label>

  </asp:Panel>
</asp:Content>