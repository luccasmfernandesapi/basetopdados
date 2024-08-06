<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailsVinculados.aspx.cs" Inherits="Topdados20.EmailsVinculados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" type="text/css" rel="stylesheet" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div>
                <div class="card">
                    <img src="img/email.png" width="100px" />
                    <div class="container">
                        <h1>E-MAIL</h1>
                        <br />
                        <h2>
                            <asp:Label ID="LbNome" runat="server" Text="Label"></asp:Label></h2>
                        <br />
                        <asp:GridView ID="GridProvEmail" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" AutoGenerateColumns="False" EmptyDataText="Sem Dados Vinculados" OnRowCommand="GridProvEmail_RowCommand">

                            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server"
                                            CommandArgument='<%# Eval("id_email") %>'
                                            OnClientClick="if (!confirm('Deseja adicionar este Email a Base?')) return false;"
                                            Height="20px" ImageUrl="~/img/validado.png" CommandName="validar" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="id_email" HeaderText="Email ID" Visible="True" />
                                <asp:BoundField DataField="contatos_id" HeaderText="contatos_id" Visible="False" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />

                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </div>
            <br />
            <asp:Label ID="LbExcl" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="LbEmailExis" runat="server" Text="Label"></asp:Label>
        </center>
    </form>
</body>
</html>
