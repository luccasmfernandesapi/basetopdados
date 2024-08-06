<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="AtualizacaoCPF.aspx.cs" Inherits="Topdados20.AtualizacaoCPF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="style.css" type="text/css" rel="stylesheet" />
    <br />
    <br />
    <center>
        <div>
            <asp:Label ID="CPF" runat="server"></asp:Label>
            <asp:TextBox ID="txtCPF" placeholder="CPF" runat="server" CssClass="inputcpf" MaxLength="11"></asp:TextBox>
            <asp:HiddenField ID="txtcontatoid" runat="server" />
            <asp:Button ID="BtnBuscaCPF" runat="server" Text="Buscar" OnClick="BtnBuscaCPF_Click" CssClass="button" UseSubmitBehavior="False" />

        </div>
    </center>
    <br />
    <br />
    <center>
        <div class="card" runat="server" id="cardcadastro">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>Atualização de Dados</h1>
                <table>
                    <tr>
                        <td class="label"><b>NOME</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtNome" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <td class="label"><b>DATA NASCIMENTO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtDtNasc" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>SEXO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtSx" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>RENDA</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtRda" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>OCUPAÇÃO</b></td>
                        <td class="label">
                            <asp:DropDownList ID="ListOcup" runat="server" CssClass="inputcpf" Width="200px" DataSourceID="ocup" DataTextField="titulo" DataValueField="CBO"></asp:DropDownList>
                            <asp:SqlDataSource ID="ocup" runat="server" ConnectionString="<%$ ConnectionStrings:TopDadosConnectionString %>" SelectCommand="SELECT * FROM Produtivo.OCUPACAO"></asp:SqlDataSource>
                        </td>
                    </tr>

                    <tr>
                        <td class="label"><b>ESTADO CIVIL </b></td>
                        <td class="label">
                            <asp:DropDownList ID="ltestcivil" runat="server" CssClass="inputcpf" Width="200px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="C" Text="Casado"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Solteiro"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Divorciado"></asp:ListItem>
                                <asp:ListItem Value="V" Text="Viúvo"></asp:ListItem>
                                <asp:ListItem Value="O" Text="Outros"></asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td class="label"><b>SITUAÇÃO CADASTRAL</b></td>
                        <td class="label">
                            <asp:DropDownList ID="LsSit" runat="server" CssClass="inputcpf" Width="200px" DataSourceID="SitCada" DataTextField="DS_SIT_CAD" DataValueField="CD_SIT_CAD"></asp:DropDownList>
                            <asp:SqlDataSource ID="SitCada" runat="server" ConnectionString="<%$ ConnectionStrings:TopDadosConnectionString %>" SelectCommand="SELECT *
FROM Produtivo.SITUACAO_CADASTRAL_PF"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA SITUAÇÃO CADASTRAL</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtdtsitcad" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA OBITO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtDtOb" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NOME MÃE</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtNMAe" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NOME PAI</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtNPai" runat="server" CssClass="inputcpf" Width="200px"></asp:TextBox></td>
                    </tr>
                </table>

                <br />
                <br />

                <center>
                    <asp:Button ID="BtnGuardar" runat="server" Text="Salvar" UseSubmitBehavior="False" OnClick="BtnGuardar_Click" CssClass="button" Width="200px" higth="100%" /></center>
                <center>
                    <asp:Label ID="MsgOk" runat="server" Text=""></asp:Label></center>



            </div>
        </div>
    </center>
    <br />
    <br />
    <center>
        <div class="card" runat="server" id="Div1">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>DIVERSOS</h1>
                <table>
                    <tr>
                        <td class="label"><b>Escolaridade</b></td>
                        <td class="label">
                            <asp:DropDownList ID="LsEscol" runat="server" CssClass="inputcpf" Width="200px">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem Value="ANALFABETO">ANALFABETO</asp:ListItem>
                                <asp:ListItem Value="DOUTORADO COMPLETO">DOUTORADO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="FUNDAMENTAL COMPLETO">FUNDAMENTAL COMPLETO</asp:ListItem>
                                <asp:ListItem Value="FUNDAMENTAL INCOMPLETO">FUNDAMENTAL INCOMPLETO</asp:ListItem>
                                <asp:ListItem Value="MEDIO COMPLETO">MEDIO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="MEDIO INCOMPLETO">MEDIO INCOMPLETO</asp:ListItem>
                                <asp:ListItem Value="MESTRADO COMPLETO">MESTRADO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="POS-DOUTORADO COMPLETO">POS-DOUTORADO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="POS-GRADUACAO COMPLETO">POS-GRADUACAO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="PRIMARIO COMPLETO">PRIMARIO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="PRIMARIO INCOMPLETO">PRIMARIO INCOMPLETO</asp:ListItem>
                                <asp:ListItem Value="SUPERIOR COMPLETO">SUPERIOR COMPLETO</asp:ListItem>
                                <asp:ListItem Value="SUPERIOR INCOMPLETO">SUPERIOR INCOMPLETO</asp:ListItem>
                                <asp:ListItem Value="TECNICO COMPLETO">TECNICO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="TECNICO INCOMPLETO">TECNICO INCOMPLETO</asp:ListItem>
                                <asp:ListItem Value="TECNOLOGO COMPLETO">TECNOLOGO COMPLETO</asp:ListItem>
                                <asp:ListItem Value="TECNOLOGO INCOMPLETO">TECNOLOGO INCOMPLETO</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:Button ID="BtnEsco" runat="server" Text="Salvar" UseSubmitBehavior="False" CssClass="button" OnClick="BtnEsco_Click" /></td>
                        <td>
                            <asp:Label runat="server" ID="LbSalvo2" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>PIS</b></td>
                        <td class="label">
                            <asp:TextBox ID="TxtPIS" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="BtnPIS" runat="server" CssClass="button" UseSubmitBehavior="False" Text="Salvar" OnClick="BtnPIS_Click" /></td>
                        <td>
                            <asp:Label runat="server" ID="LbPIS" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>CNS</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtCNS" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="BtnCNS" runat="server" CssClass="button" Text="Salvar" UseSubmitBehavior="False" OnClick="BtnCNS_Click" /></td>
                        <td>
                            <asp:Label runat="server" ID="LbCNS" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </center>
    <br />
    <br />
    <center>
        <div class="card" runat="server" id="Div2">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>DADOS</h1>
                <table>
                    <tr>
                        <td class="label"><b>RG</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtRG" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>RG DATA EMISSAO</b></td>
                        <td class="label">
                            <asp:TextBox ID="RgDtEmi" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>TITULO ELEITOR</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtTEleitor" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>CNH</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtCNH" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA HABILITACAO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtDtHab" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>COD SEGURANCA</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtCodSeg" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>FORMULARIO CNH</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtFormCNH" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>FORMULARIO RENACH</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtformren" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NUMERO PGU</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtNpgu" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>CATEGORIAL ATUAL</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtCatAt" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>SITUACAO CNH</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtSitCNH" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DT ULTIMA EMISSAO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtDtUltEmi" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA VALIDADE</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtDtVal" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="label"><b>LOCAL EMISSAO</b></td>
                        <td class="label">
                            <asp:TextBox ID="txtLocalEmis" CssClass="inputcpf" Width="200px" runat="server"></asp:TextBox></td>
                    </tr>



                </table>

                <center>
                    <asp:Button ID="BtnSalvarRG" runat="server" Text="Salvar" CssClass="button" UseSubmitBehavior="False" Width="200px" higth="100%" OnClick="BtnSalvarRG_Click" /></center>
                <center>
                    <asp:Label ID="LbMsg3" runat="server" Text=""></asp:Label></center>
            </div>
        </div>
    </center>
    <br />
    <br />


    <center>
        <div class="card" runat="server" id="Div3">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>ENDEREÇO</h1>
                <asp:GridView ID="GridEnd" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" AutoGenerateColumns="False" OnRowCommand="GridEnd_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("Endereco_Id") %>'
                                    Height="20px" ImageUrl="~/img/editar.png" CommandName="editar" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="Endereco_Id" HeaderText="ENDERECO_ID" Visible="False" />
                        <asp:BoundField DataField="ENDERECO" HeaderText="ENDEREÇO" />
                        <asp:BoundField DataField="BAIRRO" HeaderText="BAIRRO" />
                        <asp:BoundField DataField="CIDADE" HeaderText="CIDADE" />
                        <asp:BoundField DataField="UF" HeaderText="UF" />
                        <asp:BoundField DataField="CEP" HeaderText="CEP" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("Endereco_Id") %>'
                                    Height="20px" ImageUrl="~/img/excluir.png" CommandName="excluir" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br />

                <table>
                    <tr>
                        <td class="label">ENDEREÇO </td>
                        <td class="label">BAIRRO</td>
                        <td class="label">CIDADE</td>
                        <td class="label">UF</td>
                        <td class="label">CEP</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:TextBox ID="txtEnd" runat="server" CssClass="inputcpf" Width="250px"></asp:TextBox></td>
                        <td class="label">
                            <asp:TextBox ID="txtBairro" runat="server" CssClass="inputcpf" Width="150px"></asp:TextBox></td>
                        <td class="label">
                            <asp:TextBox ID="txtCID" runat="server" CssClass="inputcpf" Width="100px"></asp:TextBox></td>
                        <td class="label">
                            <asp:TextBox ID="txtUF" Width="40px" runat="server" CssClass="inputcpf"></asp:TextBox></td>
                        <td class="label">
                            <asp:TextBox ID="txtCEP" runat="server" CssClass="inputcpf" Width="77px"></asp:TextBox></td>
                        <td class="label">
                            <asp:Button ID="BtnSalEnd" runat="server" Text="Salvar" UseSubmitBehavior="False" CssClass="button" Width="75px" OnClick="BtnSalEnd_Click" />
                        </td>
                    </tr>
                    <asp:HiddenField ID="Endereco_Id" runat="server" />

                </table>
                <asp:Label ID="LbExcl" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
    </center>
    <br />
    <br />


    <center>
        <div class="card" runat="server" id="Div4">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>TELEFONE</h1>
                <asp:GridView ID="GridTel" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" AutoGenerateColumns="False" OnRowCommand="GridTel_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    ID="teste"
                                    CommandArgument='<%# Eval("TELEFONE_ID") %>'
                                    Height="20px" ImageUrl="~/img/editar.png" CommandName="editar" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="TELEFONE_ID" HeaderText="TELEFONE_ID" Visible="False" />
                        <asp:BoundField DataField="DDD" HeaderText="DDD" />
                        <asp:BoundField DataField="TELEFONE" HeaderText="TELEFONE" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("TELEFONE_ID") %>'
                                    Height="20px" ImageUrl="~/img/excluir.png" CommandName="excluir" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br />

                <table>
                    <tr>
                        <td class="label">DDD </td>
                        <td class="label">TELEFONE</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:TextBox ID="txtDDD" runat="server" CssClass="inputcpf" Width="150px" MaxLength="2"></asp:TextBox></td>
                        <td class="label">
                            <asp:TextBox ID="txtTelefone" runat="server" CssClass="inputcpf" Width="250px" MaxLength="9"></asp:TextBox></td>
                        <td class="label">
                            <asp:Button ID="BtnSalTelefone" runat="server" Text="Salvard" UseSubmitBehavior="False" CssClass="button" Width="75px" OnClick="BtnSalTelefone_Click" />
                        </td>
                    </tr>
                    <asp:HiddenField ID="TELEFONE_ID" runat="server" />

                </table>
                <asp:Label ID="LbTel" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
    </center>

    <br />
    <br />


    <center>
        <div class="card" runat="server" id="Div5">
            <asp:Image ID="Image6" runat="server" ImageUrl="~/img/atualizacao.png" Width="100px" CssClass="img" />
            <div class="container">
                <h1>E-MAIL</h1>
                <asp:GridView ID="GridEmail" runat="server" GridLines="None" CellPadding="4" CellSpacing="4" AutoGenerateColumns="False" OnRowCommand="GridEmail_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <Columns>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("EMAIL_ID") %>'
                                    Height="20px" ImageUrl="~/img/editar.png" CommandName="editar" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="EMAIL" HeaderText="EMAIL" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server"
                                    CommandArgument='<%# Eval("EMAIL_ID") %>'
                                    Height="20px" ImageUrl="~/img/excluir.png" CommandName="excluir" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                <br />

                <table>
                    <tr>
                        <td class="label">EMAIL </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="label">
                            <asp:TextBox ID="txtemail" runat="server" CssClass="inputcpf" Width="250px"></asp:TextBox></td>
                        <td class="label">
                            <asp:Button ID="BtnEmail" runat="server" Text="Salvar" UseSubmitBehavior="False" CssClass="button" Width="75px" OnClick="BtnEmail_Click" />
                        </td>
                    </tr>

                </table>
                <asp:HiddenField runat="server" ID="email_id" />
                <asp:Label ID="LbEmail" runat="server" Text="Label" Visible="false"></asp:Label>
            </div>
        </div>
    </center>
</asp:Content>
