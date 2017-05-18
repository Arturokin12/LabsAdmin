<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="LabsAdminASP.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Labs Admin Login</title>
    <link href="dist/css/style.css" rel="stylesheet" />
    <script src="dist/js/jquery-3.2.1.min.js"></script>
    <script src="dist/js/index.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="width:100%;min-height:100%;height:100%">
        <div style="height:100%;min-height:100%">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="wrapper">
                        <div class="container" style="margin-top:10%;">
                            <h1>Bienvenido a Labs Admin</h1>
                            <div class="form">
                                <asp:TextBox ID="txtUser" runat="server" placeholder="Usuario"></asp:TextBox>
                                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                                <asp:Button ID="loginButton" runat="server" Text="Entrar" OnClick="loginButton_Click" />
                            </div>
                            <asp:Label ID="lbRes" ForeColor="Red" runat="server" Text=""></asp:Label>
                        </div>

                        <ul class="bg-bubbles">
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                            <li></li>
                        </ul>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
