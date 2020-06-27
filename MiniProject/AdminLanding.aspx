<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLanding.aspx.cs" Inherits="AdminLanding" %>--%>

<%@ Page Title="Admin Landing" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminLanding.aspx.cs" Inherits="AdminLanding" EnableTheming="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <%--<asp:Label ID="Label1" runat="server" Text="Hello"></asp:Label>--%>
        <%--<asp:Button ID="Button1" runat="server" Text="Logout" OnClick="Button1_Click" />--%>
        <%--<br />--%>
        <strong>
            <span class="style6">Insert Restaurant Data</span>
        </strong>
        <br />
        <table style="width: 100%;">
            <tr>
                <td class="style1"></td>
                <td class="style2"></td>
                <td></td>
            </tr>
            <tr>
                <td class="style1">Name</td>
                <td class="style2">
                    <asp:TextBox ID="ResNameBox" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Restaurant Name!" EnableClientScript="false" ControlToValidate="ResNameBox" ValidationGroup="G1" ></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="style1">Cuisine</td>
                <td class="style2">
                    <asp:TextBox ID="ResCuisineBox" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Cuisine!" EnableClientScript="false" ControlToValidate="ResCuisineBox" ValidationGroup="G1"></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="style1">Location</td>
                <td class="style2">
                    <asp:TextBox ID="ResLocBox" runat="server" EnableViewState="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Location!" EnableClientScript="false" ControlToValidate="ResLocBox" ValidationGroup="G1"  ></asp:RequiredFieldValidator>
                </td>
                <td></td>
            </tr>

            <tr>
                <td class="style1"></td>
                <td class="style2">
                    <asp:Button ID="Button2" runat="server" BorderColor="#CCFF66"
                        ForeColor="#0066FF" OnClick="Button2_Click" Text="Insert Data" ValidationGroup="G1" />
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:SqlDataSource ID="ResData" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kunal\Desktop\A6\MiniProject\App_Data\Database.mdf;Integrated Security=True" SelectCommand="SELECT * FROM restaurants"></asp:SqlDataSource>
                    <asp:GridView ID="ResView" runat="server" DataSourceID="ResData" AutoGenerateColumns="false" OnRowCommand="ResView_RowCommand">
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
                    <br />
                    <asp:Button ID="ReportsButton" runat="server" Text="Reports" OnClick="ReportsButton_Click" />
                </td>
                <td>
                    <asp:SqlDataSource ID="AppCommSource" runat="server" ConnectionString='<%$ ConnectionStrings:ConnectionString %>' SelectCommand="SELECT restaurant_name, comment_content, comment_id FROM user_comments as u INNER JOIN restaurants as r ON u.restaurant_id = r.restaurant_id WHERE u.is_approved=0 " UpdateCommand="UPDATE user_comments SET comment_content=@comment_content WHERE comment_id=@comment_id">
                        <UpdateParameters>
                            <asp:Parameter Name="comment_content" Type="String" />
                            <asp:Parameter Name="comment_id" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:GridView ID="AppCommGrid" runat="server" DataSourceID="AppCommSource" AllowPaging="True" AutoGenerateColumns="False" PageSize="5" OnRowCommand="AppCommGrid_RowCommand" Width="600px" DataKeyNames="comment_id" AutoGenerateEditButton="true">
                        <Columns>
                            <%--<asp:BoundField DataField="comment_id" HeaderText="ID"></asp:BoundField>--%>
                           <%--<asp:CommandField  ShowEditButton="true" CausesValidation="false"/>--%>
                            <asp:BoundField DataField="restaurant_name" ReadOnly="True" HeaderText="Restaurants"></asp:BoundField>
                            <asp:BoundField DataField="comment_content" HeaderText="Comments"></asp:BoundField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="ApproveButton" runat="server" CausesValidation="false" CommandName="Approve"
                                                    Text="Approve" CommandArgument='<%# Eval("comment_id") %>' />
                                            </td>

                                            <td>
                                                <asp:Button ID="RejectButton" runat="server" CausesValidation="false" CommandName="Reject"
                                                    Text="Reject" CommandArgument='<%# Eval("comment_id") %>' />
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                            </asp:TemplateField>
                            
                        </Columns>
                    </asp:GridView>
                </td>

            </tr>
        </table>

    </div>
</asp:Content>

