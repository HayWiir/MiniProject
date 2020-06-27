
<%@ Page Title="Login" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableTheming="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Label ID="Label4" runat="server" Text="¯\_(ツ)_/¯" Font-Size="XX-Large" Font-Bold="true"></asp:Label><br />
    <asp:Label ID="Label5" runat="server" Text="Restaurant Aggregator" Font-Size="X-Large" Font-Bold="true"></asp:Label>
    <br /><br />
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>        
        <br />
        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter username!" ControlToValidate="TextBox1" EnableClientScript="false"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter password!" ControlToValidate="TextBox2" EnableClientScript="false"></asp:RequiredFieldValidator>
   </asp:Panel>
</asp:Content>
