<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="userText" runat="server" style="position:absolute; top: 11px; left: 127px; width: 179px;" Height="16px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
    
        <asp:Label ID="loginLabel" runat="server" style="position:absolute; top: 14px; left: 54px;" Text="Login : " Font-Bold="True"></asp:Label>
    
    </div>
        <asp:TextBox ID="passText" style="position:absolute; top: 50px; left: 128px; width: 179px;" runat="server"></asp:TextBox>
        <p>
            <asp:Label ID="passLabel" runat="server"  style="position:absolute; top: 56px; left: 31px;" Font-Bold="True" Text="Password : "></asp:Label>
        </p>
        <asp:Button ID="loginButton" runat="server" style="position:absolute; top: 93px; left: 115px; height: 26px; right: 933px;" Text="Login" Font-Bold="True" OnClick="loginButton_Click" />
    </form>
</body>
</html>
