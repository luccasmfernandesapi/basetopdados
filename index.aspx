<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Topdados20.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="index.css" type="text/css" rel="stylesheet"/>
    <title>Login - Sistema de Busca Topdados</title> 
</head>
<body>
    <div class="telalogin">  
        <form id="form1" runat="server">
       <asp:Image ID="Image1" runat="server" ImageUrl="~/img/logotipo.png" />
            <br />
            <br />
            <br />
            <br />
            <asp:TextBox ID="txtusuario" placeholder="usuario" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="txtsenha" placeholder="senha"  runat="server" TextMode="Password" ></asp:TextBox>
            <br> <br>
           <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Entrar" />
        <p>
        <div class="alert">
        <asp:Label ID="MsgErro" runat="server" Visible="false" Text="Label"></asp:Label>
        </p>
    </form>
</div>
</body>
</html>
