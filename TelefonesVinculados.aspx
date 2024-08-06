<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TelefonesVinculados.aspx.cs" Inherits="Topdados20.TelefonesVinculados" %>

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
                <div class="card" runat="server" id="cardtelefone">
            <asp:Image ID="Image7" runat="server" ImageUrl="~/img/telefone.png" CssClass="img" />
            <div class="container">
                <h1>Telefones-Vinculados</h1>
                <br />
                <h2><asp:Label ID="LbNome" runat="server" Text="Label"></asp:Label></h2>
                <br />
                <asp:GridView ID="GridProvTel" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" AutoGenerateColumns="False" EmptyDataText="Sem Dados Vinculados" OnRowCommand="GridProvTel_SelectedIndexChanged">

                    <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("ID_TELEFONE") %>'
                                    OnClientClick="if (!confirm('Deseja adicionar este telefone a Base?')) return false;"
                                    Height="20px" ImageUrl="~/img/validado.png" CommandName="validar" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="ID_TELEFONE" HeaderText="Id Telefone" Visible="True" />
                        <asp:BoundField DataField="contatos_id" HeaderText="contatos_id" Visible="False" />
                        <asp:BoundField DataField="DDD" HeaderText="DDD" />
                        <asp:BoundField DataField="Telefone" HeaderText="Telefone" />

                        
                    </Columns>

                </asp:GridView>
                </div>
                    </div>
            </div>
            <br />
            <asp:Label ID="LbExcl" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="LbTelExis" runat="server" Text="Label"></asp:Label>
        </center>
    </form>
</body>
</html>
