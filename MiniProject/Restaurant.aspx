<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Restaurant.aspx.cs" Inherits="Restaurant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="ResName" runat="server" Font-Size="X-Large" Font-Bold="true">Dummy Name</asp:Label><br />
    <asp:Label ID="ResCuisine" runat="server" Font-Italic="true" >Cuisine</asp:Label><br />
    ⭐<asp:Label ID="ResRating" runat="server" Font-Bold="true" >5</asp:Label><br /><br />
    <asp:Label ID="ResLocation" runat="server" >Some address, Blah, Blah</asp:Label><br /><br />

    
    <asp:Panel ID="RatingPanel" runat="server" Visible="false">  
        <asp:Label ID="Label1" runat="server" Text="Rate the Restaurant"></asp:Label><br />
        <asp:RadioButtonList ID="RatingList" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1" Text="Worst"></asp:ListItem>
            <asp:ListItem Value="2" Text="Fair"></asp:ListItem>
            <asp:ListItem Value="3" Text="Average"></asp:ListItem>
            <asp:ListItem Value="4" Text="Good"></asp:ListItem>
            <asp:ListItem Value="5" Text="Best"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="RateButton" runat="server" Text="Rate" OnClick="RateButton_Click" CausesValidation="false" />
        <asp:Label ID="RateLabel" runat="server" Text=""></asp:Label><br />
        
    </asp:Panel>
    <br /><br />
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Panel ID="CommentPanel" runat="server" Visible="false">
                    <asp:TextBox ID="CommentBox" runat="server" placeholder="Type a comment" maxlength="100" TextMode="MultiLine"  Width="500px" Height="100px"></asp:TextBox><br />
                    <asp:Button ID="CommentButton" runat="server" Text="Comment" OnClick="CommentButton_Click" />
                    <asp:Label ID="CommentMsg" runat="server" Text=""></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cannot submit empty comment!" ControlToValidate="CommentBox" EnableClientScript="false"></asp:RequiredFieldValidator>
                </asp:Panel>
            </td>
            <td>
                
                <asp:GridView ID="CommentView" runat="server" DataSourceID="CommentData" AutoGenerateColumns="false" AllowPaging="True" PageSize="5" Width="500px" >
                    <Columns>
                        <asp:BoundField DataField="comment_content" SortExpression="comment_content" HeaderText="User Comments" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="CommentData" runat="server" ConnectionString='<%$ ConnectionStrings:ConnectionString %>'  SelectCommand="SELECT [comment_content] FROM [user_comments] WHERE ([restaurant_id] = @restaurant_id) AND is_approved=1">
                    <SelectParameters>
                        <asp:QueryStringParameter QueryStringField="restaurant_id" Name="restaurant_id" Type="Int32"></asp:QueryStringParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </td> 
            
        </tr>
    </table>
    <div style="position: fixed;bottom:10px; width: 100%; text-align:center">
        <asp:Label ID="ViewsLabel" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>

