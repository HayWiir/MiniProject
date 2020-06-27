<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Text="Administrator Reports" Font-Size="X-Large" Font-Bold="true"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="SResName" runat="server" placeholder="Enter restaurant name"></asp:TextBox>
    <asp:TextBox ID="SResCus" runat="server" placeholder="Enter cuisine"></asp:TextBox>
    <asp:TextBox ID="SResLoc" runat="server" placeholder="Enter location"></asp:TextBox>
    <asp:TextBox ID="SResRate" runat="server" placeholder="Enter minimum rating"></asp:TextBox>
    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
    <asp:Button ID="ResetButton" runat="server" Text="Reset" OnClick="ResetButton_Click" />
    <asp:RangeValidator ID="RateValidation" runat="server" ControlToValidate="SResRate" ErrorMessage="Enter value between 0.0 and 5.0" MinimumValue="0.0" MaximumValue="5.0" Type="Double" EnableClientScript="false"></asp:RangeValidator>
    <br />
    <br />


    <asp:SqlDataSource ID="ResData" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kunal\Desktop\A6\MiniProject\App_Data\Database.mdf;Integrated Security=True" SelectCommand="SELECT * FROM restaurants"></asp:SqlDataSource>
    <asp:GridView ID="ResView" runat="server" DataSourceID="ResData" AutoGenerateColumns="false" OnRowCommand="ResView_RowCommand" AllowPaging="True" PageSize="10">
        <Columns>
            <%--<asp:BoundField DataField = "restaurant_id" HeaderText = "ID" />--%>
            <asp:BoundField DataField="restaurant_name" HeaderText="Name" />
            <asp:BoundField DataField="restaurant_cusine" HeaderText="Cuisine" />
            <asp:BoundField DataField="restaurant_location" HeaderText="Location" />
            <asp:BoundField DataField="restaurant_rating" HeaderText="Rating" />
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:Button ID="GoToButton" runat="server" CausesValidation="false" CommandName="GoTo"
                        Text="Page" CommandArgument='<%# Eval("restaurant_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

