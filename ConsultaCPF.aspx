<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultaCPF.aspx.cs" Async="true" Inherits="Topdados20.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="style.css" type="text/css" rel="stylesheet" />
    <br>
    <br>
    <br>

    <center>
        <asp:TextBox ID="txtCPF" placeholder="CPF" runat="server" CssClass="inputcpf" MaxLength="11"></asp:TextBox>
        <asp:Button ID="BtnPesquisaCPF" runat="server" OnClick="Button1_Click" Text="Pesquisa" CssClass="button" UseSubmitBehavior="False" />


        <asp:HiddenField ID="txtContatosID" runat="server" />
        <asp:Button ID="Button_SCI" runat="server" OnClick="SCI_Consulta_Click" Text="Consulta SCI" UseSubmitBehavior="False" CssClass="button" />
        <asp:Button ID="BtnBigData" runat="server" Text="Consulta BigData" UseSubmitBehavior="False" CssClass="button" OnClick="BtnBigData_Click" />
        <br />
        <asp:Label ID="lblSCI" runat="server" Text="Consulta Já realizada no SCI" Visible="False" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        &nbsp;&nbsp;<asp:Label ID="LbBigData" runat="server" Text="Consulta Já realizada no BigData" Visible="False" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        &nbsp;&nbsp;<asp:Label ID="LblNencontrado" runat="server" Text="CPF não encontrado na Base" Visible="False" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        <br />
        <br />

    </center>
    <center>
        <div class="card" runat="server" id="cardcadastro">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/dadoscadastrais.png" CssClass="img" />
            <div class="container">
                <h1>Dados Cadastrais</h1>
                <br />
                <table runat="server" id="tbcadastro">
                    <tr>
                        <td class="label"><b>CPF</b></td>
                        <td class="label">
                            <asp:Label ID="Rpf" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>Nome</b></td>
                        <td class="label">
                            <asp:Label ID="RcNOME" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA NASCIMENTO</b></td>
                        <td class="label">
                            <asp:Label ID="RcDtN" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>SITUAÇÃO CADASTRAL</b></td>
                        <td class="label">
                            <asp:Label ID="RbSCad" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>DATA SITUAÇÃO CADASTRAL</b></td>
                        <td class="label">
                            <asp:Label ID="RcDtSC" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>DATA OBITO</b></td>
                        <td class="label">
                            <asp:Label ID="RcOB" runat="server" Text="Label"></asp:Label></td>
                    </tr>


                    <tr>
                        <td class="label"><b>IDADE</b></td>
                        <td class="label">
                            <asp:Label ID="RcId" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>SEXO</b></td>
                        <td class="label">
                            <asp:Label ID="RcSexo" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>RENDA</b></td>
                        <td class="label">
                            <asp:Label ID="RcRen" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>OCUPAÇÃO</b></td>
                        <td class="label">
                            <asp:Label ID="RcOcu" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>ESCOLARIDADE</b></td>
                        <td class="label">
                            <asp:Label ID="RecEsco" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>ESTADO CIVIL</b></td>
                        <td class="label">
                            <asp:Label ID="RcEstCvl" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>NATURALIDADE</b></td>
                        <td class="label">
                            <asp:Label ID="RcNaturalidade" runat="server" Text="Label"></asp:Label></td>

                    </tr>

                    <tr>
                        <td class="label"><b>NOME MÃE</b></td>
                        <td class="label">
                            <asp:Label ID="RcMae" runat="server" Text="Label"></asp:Label></td>

                    </tr>
                    <tr>
                        <td class="label"><b>NOME PAI</b></td>
                        <td class="label">
                            <asp:Label ID="RCPai" runat="server" Text="Label"></asp:Label></td>

                    </tr>

                </table>

            </div>

        </div>

    </center>

    <br>
    <br>
    <center>
        <div class="card" runat="server" id="carddocumento">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/img/documento.png" CssClass="img" />
            <div class="cotainer">
                <h1>Documentos</h1>
                <br />
                <table runat="server" id="tbdoc">
                    <tr>
                        <td class="label"><b>TITULO ELEITOR</b></td>
                        <td class="label">
                            <asp:Label ID="RcTe" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>RG</b></td>
                        <td class="label">
                            <asp:Label ID="RcRG" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>RG DATA EMISSAO</b></td>
                        <td class="label">
                            <asp:Label ID="RcRgDtE" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>CNS</b></td>
                        <td class="label">
                            <asp:Label ID="RcCNS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>PIS</b></td>
                        <td class="label">
                            <asp:Label ID="RcPIS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NIS</b></td>
                        <td class="label">
                            <asp:Label ID="RcNIS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NUMERO CTPS</b></td>
                        <td class="label">
                            <asp:Label ID="RcNCTPS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>SÉRIE</b></td>
                        <td class="label">
                            <asp:Label ID="RcSCTPS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DT EMISSÃO</b></td>
                        <td class="label">
                            <asp:Label ID="RcDtECTPS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="label"><b>UF</b></td>
                        <td class="label">
                            <asp:Label ID="RcUFCTPS" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                </table>

            </div>
        </div>
    </center>
    <br>
    <br>

    <center>
        <div class="card" runat="server" id="cardcnh">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/img/cnh.png" CssClass="img" />
            <div class="container">
                <h1>CNH</h1>
                <br />
                <table runat="server" id="CNH">
                    <tr>
                        <td class="label"><b>CNH</b></td>
                        <td class="label">
                            <asp:Label ID="CNHLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA PRIMEIRA HABILITACAO</b></td>
                        <td class="label">
                            <asp:Label ID="DATA_HABILITACAOLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>COD SEGURANCA</b></td>
                        <td class="label">
                            <asp:Label ID="COD_SEGURANCALabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>FORMULARIO CNH</b></td>
                        <td class="label">
                            <asp:Label ID="FORMULARIO_CNHLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>FORMULARIO RENACH</b></td>
                        <td class="label">
                            <asp:Label ID="FORMULARIO_RENACHLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>NUMERO PGU</b></td>
                        <td class="label">
                            <asp:Label ID="NUMERO_PGULabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>CATEGORIAL ATUAL</b></td>
                        <td class="label">
                            <asp:Label ID="CATEGORIAL_ATUALLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>SITUACAO CNH</b></td>
                        <td class="label">
                            <asp:Label ID="SITUACAO_CNHLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA ULTIMA EMISSAO</b></td>
                        <td class="label">
                            <asp:Label ID="DT_ULTIMA_EMISSAOLabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>DATA VALIDADE</b></td>
                        <td class="label">
                            <asp:Label ID="DATA_VALIDADELabel" runat="server" Text='Label' /></td>
                    </tr>
                    <tr>
                        <td class="label"><b>LOCAL EMISSAO</b></td>
                        <td class="label">
                            <asp:Label ID="LOCAL_EMISSAOLabel" runat="server" Text='Label' /></td>
                    </tr>

                </table>
            </div>
        </div>
    </center>
    <br>
    <br>

    <center>
        <div class="card" runat="server" id="cardrelacionamento">
            <asp:Image ID="Image4" runat="server" ImageUrl="~/img/relacoes.png" CssClass="img" />
            <div class="container">
                <h1>Relacionamentos</h1>
                <p>
                    <br />
                    <asp:GridView ID="GridRelacionamentos" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:GridView>

                </p>
            </div>
        </div>
    </center>
    <br>
    <br>

    <center>
        <div class="card" runat="server" id="cardendereco">
            <asp:Image ID="Image5" runat="server" ImageUrl="~/img/mapa.png" CssClass="img" />
            <div class="container">
                <h1>Endereços</h1>
                <br />
                <asp:GridView ID="GridEndedeco" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

            </div>
            <div>
                <br />
                <asp:Button runat="server" CommandArgument='<%# Eval("txtContatosID.Value") %>' Text='Ver +' CausesValidation="false" CssClass="button" UseSubmitBehavior="False"
                    OnClientClick="AbrirPopup(); return false;" ID="Btn_End_Rel"/>
            </div>
        </div>

    </center>
    <br>
    <br>



    <center>
        <div class="card" runat="server" id="cardsocio">
            <asp:Image ID="Image6" runat="server" ImageUrl="~/img/parceiros.png" CssClass="img" />
            <div class="container">
                <h1>Sócios de Empresa</h1>
                <br />
                <asp:GridView ID="GridSocios" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

            </div>
        </div>
    </center>
    <br>
    <br>

    <center>
        <div class="card" runat="server" id="cardtelefone">
            <asp:Image ID="Image7" runat="server" ImageUrl="~/img/telefone.png" CssClass="img" />
            <div class="container">
                <h1>Telefones</h1>
                <br />
                <asp:GridView ID="GridTelefone" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

            </div>
            <div>
                <br />
                <asp:Button runat="server" CommandArgument='<%# Eval("txtContatosID.Value") %>' Text='Ver +' CausesValidation="false" CssClass="button" UseSubmitBehavior="False"
                    OnClientClick="AbrirPopup2(); return false;" ID="Btn_Tel_Rel"/>
            </div>
        </div>
    </center>
    <br>
    <br>

    <center>
        <div class="card" runat="server" id="cardemail">
            <asp:Image ID="Image8" runat="server" ImageUrl="~/img/email.png" CssClass="img" />
            <div class="container">
                <h1>E-mail</h1>
                <br />
                <asp:GridView ID="GridEmail" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

            </div>
            <br />
            <asp:Button runat="server" CommandArgument='<%# Eval("txtContatosID.Value") %>' Text='Ver +' CausesValidation="false" CssClass="button" UseSubmitBehavior="False"
                OnClientClick="AbrirPopup3(); return false;" ID="Btn_Email_Rel"/>
        </div>
    </center>
    <br />
    <br />

    <center>
        <div class="card" width="100%" runat="server" id="Div2">
            <asp:Image ID="Image10" runat="server" ImageUrl="~/img/emprego.png" Width="100px" CssClass="img" />
            <div class="container" width="100%">
                <h1>Empregos</h1>
                <br />
                <asp:GridView ID="GridEmpreg" GridLines="None" CellPadding="4" CellSpacing="4" runat="server">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                </asp:GridView>
            </div>
        </div>
    </center>
    <br />
    <br />
    <center>
        <div class="card" width="100%" runat="server" id="Div1">
            <asp:Image ID="Image9" runat="server" ImageUrl="~/img/veiculo.png" Width="100px" CssClass="img" />
            <div class="container" width="100%">
                <h1>Veiculos</h1>
                <br />
                <asp:GridView ID="GridVeiculos" GridLines="None" CellPadding="4" CellSpacing="4" runat="server" Font-Size="Smaller">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                </asp:GridView>


            </div>
        </div>
    </center>
    <script>
        function AbrirPopup(id) {
            var id = document.getElementById('<%= txtContatosID.ClientID %>').value;
            var url = "EnderecosVinculados.aspx?id=" + id;
            console.log("AbrirPopup chamado com ID:", id);
            console.log("URL da página:", url);
            window.open(url, "EnderecosVinculados", "width=1000,height=800");
        }
        function AbrirPopup2(id) {
            var id = document.getElementById('<%= txtContatosID.ClientID %>').value;
            var url = "TelefonesVinculados.aspx?id=" + id;
            console.log("AbrirPopup chamado com ID:", id);
            console.log("URL da página:", url);
            window.open(url, "TelefonesVinculados", "width=1000,height=800");
        }
        function AbrirPopup3(id) {
            var id = document.getElementById('<%= txtContatosID.ClientID %>').value;
            var url = "EmailsVinculados.aspx?id=" + id;
            console.log("AbrirPopup chamado com ID:", id);
            console.log("URL da página:", url);
            window.open(url, "EmailsVinculados", "width=1000,height=800");
        }
    </script>

</asp:Content>
