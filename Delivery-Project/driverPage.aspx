<%@ Page Title="" Language="C#" MasterPageFile="~/Client-Side.Master" AutoEventWireup="true" CodeBehind="driverPage.aspx.cs" Inherits="Delivery_Project.driverPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
     <h1 class="driver-page-title">ברוכים הבאים לדף הנהגים</h1>
    <div class="driver-info">
        <p>כאן תוכל לנהל את המסלולים שלך, לסמן משלוחים כהושלמו ולעקוב אחרי שעות העבודה שלך.</p>
        <asp:Button ID="StartBtn" runat="server" Text="התחל עבודה" OnClick="StartBtn_Click" /></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
