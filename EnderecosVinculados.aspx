<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnderecosVinculados.aspx.cs" Inherits="Topdados20.EnderecosVinculados" %>

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
                <div class="card" runat="server" id="cardendereco">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/img/mapa.png" CssClass="img" />
                    <div class="container">
                        <h1>Endereços-Vinculados</h1>
                        <br />
                        <h2>
                            <asp:Label ID="LbNome" runat="server" Text="Label"></asp:Label></h2>
                        <br />
                        <asp:GridView ID="GridProvEnd" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" EmptyDataText="Sem Dados Vinculados" AutoGenerateColumns="False" OnRowCommand="GridProvEnd_RowCommand">
                            <EditRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <Columns>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server"
                                            CommandArgument='<%# Eval("id_endereco") %>'
                                            OnClientClick="if (!confirm('Deseja adicionar este Endereco a Base?')) return false;"
                                            Height="20px" ImageUrl="~/img/validado.png" CommandName="validar" CausesValidation="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID_ENDERECO" HeaderText="Endereco ID" Visible="True" />
                                <asp:BoundField DataField="CONTATOS_ID" Visible="False" />
                                <asp:BoundField DataField="logradouro" HeaderText="Logradouro" Visible="True" />
                                <asp:BoundField DataField="BAIRRO" HeaderText="Bairro" Visible="True" />
                                <asp:BoundField DataField="CIDADE" HeaderText="Cidade" Visible="True" />
                                <asp:BoundField DataField="UF" HeaderText="UF" Visible="True" />
                                <asp:BoundField DataField="CEP" HeaderText="CEP" Visible="True" />
                                

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <asp:Label ID="LbExcl" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="LbEndExis" runat="server" Text="Label"></asp:Label>
        </center>
    </form>
</body>
</html>
