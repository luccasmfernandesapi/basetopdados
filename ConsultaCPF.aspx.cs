using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;


namespace Topdados20
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            CNH.Visible = false;
            LblNencontrado.Visible = false;
            lblSCI.Visible = false;
            LbBigData.Visible = false;
            tbcadastro.Visible = false;
            tbdoc.Visible = false;
            Btn_Email_Rel.Visible = false;
            Btn_Tel_Rel.Visible = false;
            Btn_End_Rel.Visible = false;
            string pesquisa = "0";

            if (!IsPostBack)
            {

                if (Request.QueryString.Count > 0)
                {
                    pesquisa = Request.QueryString.Get("pesquisa").ToString();
                    if (pesquisa == "1")
                    {
                        txtCPF.Text = Request.QueryString.Get("CPF").ToString();
                        Button1_Click(null, null);
                    }
                    else
                    {

                        if (pesquisa == "2")
                        {
                            txtCPF.Text = Request.QueryString.Get("CPF").ToString();
                            Button1_Click(null, null);

                        }
                    }
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var id_perfil = "";



            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                id_perfil = htParametros["id_perfil"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();



            sqlMS.ExecuteNonQuery(@"insert into [TopDados].[Produtivo].[USUARIO_LOG]
                                (ID_USUARIO,DT_ATIVIDADE,DS_ATIVIDADE,NU_CPF)
                                VALUES
                                ('" + id_usuario + "',GETDATE(),'Buscar CPF','" + txtCPF.Text + "')");

            SqlDataReader reader2 = sqlMS.ReadData(@"select CPF from[TopDados].[Produtivo].[PESSOA_FISICA] where CPF =  right('00000000000' +'" + txtCPF.Text + "',11)");
            if (reader2.HasRows)
            {

            }
            else
            {
                LblNencontrado.Visible = true;

            }


            SqlDataReader reader = sqlMS.ReadData(@"SELECT a.CONTATOS_ID, a.CPF, a.NOME, a.NOME_MAE, a.NOME_PAI , CONVERT(varchar(10), a.NASC, 103) AS[DATA NASCIMENTO], FLOOR(DATEDIFF(DAY, a.NASC, GETDATE()) / 365.25) AS 'IDADE', a.SEXO, format(a.RENDA,'c','pt-br') as RENDA, b.titulo AS OCUPAÇÃO, c.ESCOLARIDADE, CASE WHEN a.ESTCIV = 'C' THEN 'Casado' WHEN a.ESTCIV = 'S' THEN 'Solteiro' WHEN a.ESTCIV = 'D' THEN 'Divorciado' WHEN a.ESTCIV = 'V' THEN 'Viúvo' WHEN a.ESTCIV = 'O' THEN 'Outros' ELSE a.ESTCIV END AS[ESTADO CIVIL], d.NATURALIDADE_CIDADE, e.DS_SIT_CAD AS[SITUAÇÃO CADASTRAL], CONVERT(varchar(10), a.DT_SIT_CAD, 103) AS[DATA SITUAÇÃO CADASTRAL], CASE WHEN a.DT_OB = '19000101' THEN 'NULO' ELSE CONVERT(varchar(10), a.DT_OB, 103) END AS[DATA OBITO], a.CONTATOS_ID_CONJUGE FROM Produtivo.PESSOA_FISICA AS a LEFT OUTER JOIN Produtivo.OCUPACAO AS b ON a.CBO = b.CBO AND Tipo='OCUPAÇÃO' LEFT OUTER JOIN TB_ESCOLARIDADE AS c ON a.CONTATOS_ID = c.CONTATOS_ID LEFT OUTER JOIN NATURALIDADE AS d ON a.CONTATOS_ID = d.CONTATOS_ID LEFT OUTER JOIN Produtivo.SITUACAO_CADASTRAL_PF AS e ON CONVERT(varchar(10), a.CD_SIT_CAD, 103) = e.CD_SIT_CAD WHERE(a.CPF = right('00000000000' + '" + txtCPF.Text + "',11))");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbcadastro.Visible = true;
                    txtContatosID.Value = reader["CONTATOS_ID"].ToString();
                    Rpf.Text = reader["CPF"].ToString();
                    RcNOME.Text = reader["NOME"].ToString();
                    RcMae.Text = reader["NOME_MAE"].ToString();
                    RCPai.Text = reader["NOME_PAI"].ToString();
                    RcDtN.Text = reader["DATA NASCIMENTO"].ToString();
                    RcId.Text = reader["IDADE"].ToString();
                    RcSexo.Text = reader["SEXO"].ToString();
                    RcRen.Text = reader["RENDA"].ToString();
                    RcOcu.Text = reader["OCUPAÇÃO"].ToString();
                    RecEsco.Text = reader["ESCOLARIDADE"].ToString();
                    RcEstCvl.Text = reader["ESTADO CIVIL"].ToString();
                    RcNaturalidade.Text = reader["NATURALIDADE_CIDADE"].ToString();
                    RbSCad.Text = reader["SITUAÇÃO CADASTRAL"].ToString();
                    RcDtSC.Text = reader["DATA SITUAÇÃO CADASTRAL"].ToString();
                    RcOB.Text = reader["DATA OBITO"].ToString();


                    CarregarDocumento();
                    CarregarCNH();
                    CarregarRelacionamentos();
                    CarregarEndereco();
                    CarregarSocios();
                    CarregarTelefone();
                    CarregarEmail();
                    CarregarVeiculos();
                    CarregarEmpregos();
                    CarregarButttonEnd();
                    CarregarButttonTel();
                    CarregarButttonEmail();
                }
            }
            SqlDataReader reader4 = sqlMS.ReadData(@"select top 1 DT_CONSULTA from Produtivo.SCI_CONSULTA where NU_DOCUMENTO = right('00000000000' + '" + txtCPF.Text + "', 11) and IC_ERRO = 'N'");
            if (reader4.HasRows)
            {
                while (reader4.Read())
                {
                    lblSCI.Visible = true;
                    Button_SCI.Enabled = false;
                }


            }

            else
            {
                Button_SCI.Enabled = true;
            }

            SqlDataReader reader5 = sqlMS.ReadData(@"select top 1 DT_CONSULTA from Produtivo.REPICC_CONSULTA where NU_DOCUMENTO = right('00000000000' + '" + txtCPF.Text + "', 11) and IC_ERRO = 'N'");
            if (reader5.HasRows)
            {
                while (reader5.Read())
                {
                    lblSCI.Visible = true;
                    Button_SCI.Enabled = false;
                }


            }

            else
            {
                Button_SCI.Enabled = true;
            }

            SqlDataReader reader6 = sqlMS.ReadData(@"select top 1 DT_CONSULTA from Produtivo.BIGDATA_CONSULTA where NU_DOCUMENTO = right('00000000000' + '" + txtCPF.Text + "', 11) and IC_ERRO = 'N'");
            if (reader6.HasRows)
            {

                LbBigData.Visible = true;
                BtnBigData.Enabled = false;

            }

            else
            {
                BtnBigData.Enabled = true;
            }

            
            reader.Close();
            reader4.Close();
            reader5.Close();
            reader6.Close();
            sqlMS.MSSqlConectionClose();




        }

        protected void SCI_Consulta_Click(object sender, EventArgs e)
        {
            Consulta();
            Consulta2();

        }

        //private async void Consulta() //string strEmail
        //{
        //    Hashtable htParametros = new Hashtable();
        //    var id_usuario = "0";

        //    if (Session["parametros"] != null)
        //    {
        //        htParametros = (Hashtable)Session["parametros"];
        //        id_usuario = htParametros["id_usuario"].ToString();
        //    }

        //    MSSqlConection sqlMS1 = new MSSqlConection();
        //    MSSqlConection sqlMS2 = new MSSqlConection();

        //    string TOKEN = "";

        //    using (var client = new HttpClient(new HttpClientHandler()))
        //    {

        //        using (var responseGetToken = await client.GetAsync("https://ws.scibhive.com.br//Seguranca.svc/API/GetToken/TWF0aGV1cyBUb3A=/VGRhZG9zJTU5MzM="))
        //        {
        //            if (responseGetToken.IsSuccessStatusCode)
        //            {
        //                var ProdutoJsonString = await responseGetToken.Content.ReadAsStringAsync();

        //                var jObject = JObject.Parse(ProdutoJsonString);
        //                TOKEN = jObject.SelectToken("Token").ToString();

        //                sqlMS1.WriteData(@"insert into Produtivo.SCI_TOKEN (DS_TOKEN) select '" + TOKEN + "'");

        //                sqlMS1.WriteData(@"insert into Produtivo.SCI_CONSULTA (NU_DOCUMENTO, ID_USUARIO_CONSULTOU) select right('00000000000' + '" + txtCPF.Text + "', 11), " + id_usuario);
        //                string CPF = txtCPF.Text;

        //                using (var responseGetConsulta = new HttpClient(new HttpClientHandler()))
        //                {

        //                    using (var responseUpdatePerson = await client.GetAsync("https://ws.scibhive.com.br/APIDados.svc/ConsultaPessoaRelacionamentos/" + CPF + "/" + TOKEN))
        //                    {
        //                        if (responseUpdatePerson.IsSuccessStatusCode)
        //                        {

        //                            var ProdutoJsonString_2 = await responseUpdatePerson.Content.ReadAsStringAsync();
        //                            sqlMS2.WriteData(@"update Produtivo.SCI_CONSULTA set IC_ERRO = 'N', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_JSON_RESULTADO = '" + ProdutoJsonString_2.Replace("'", "") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
        //                        }
        //                        else
        //                        {
        //                            sqlMS2.WriteData(@"update Produtivo.SCI_CONSULTA set IC_ERRO = 'S', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    sqlMS2.WriteData(@" declare @CPF varchar(11)
        //                        set @CPF = right('00000000000' + '" + txtCPF.Text + @"', 11)
        //                        EXEC Produtivo.SP_TOPDADOS_RETORNO_API @CPF, " + id_usuario);

        //    Response.Redirect("ConsultaCPF.aspx?Pesquisa=1&CPF=" + txtCPF.Text);
        //    sqlMS1.MSSqlConectionClose();
        //    sqlMS2.MSSqlConectionClose();
        //}
        private async void Consulta()
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS1 = new MSSqlConection();
            MSSqlConection sqlMS2 = new MSSqlConection();

            string TOKEN = "";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api-seguranca.repicc.com.br/Seguranca/GetToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("TOPDADOS:Apidbm1521")));
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var ProdutoJsonString = await response.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(ProdutoJsonString);
                TOKEN = jObject["token"].ToString();

                sqlMS1.WriteData(@"insert into Produtivo.SCI_TOKEN (DS_TOKEN) select '" + TOKEN + "'");

                sqlMS1.WriteData(@"insert into Produtivo.SCI_CONSULTA (NU_DOCUMENTO, ID_USUARIO_CONSULTOU) select right('00000000000' + '" + txtCPF.Text + "', 11), " + id_usuario);
                string CPF = txtCPF.Text;

                var consultaUrl = "https://api.repicc.com.br/Dados/Pessoa/Consulta?cpf=" + CPF;
                var consultaRequest = new HttpRequestMessage(HttpMethod.Get, consultaUrl);
                consultaRequest.Headers.Add("Authorization", "Bearer " + TOKEN);

                var consultaResponse = await client.SendAsync(consultaRequest);

                if (consultaResponse.IsSuccessStatusCode)
                {
                    var ProdutoJsonString_2 = await consultaResponse.Content.ReadAsStringAsync();
                    sqlMS2.WriteData(@"update Produtivo.SCI_CONSULTA set IC_ERRO = 'N', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_JSON_RESULTADO = '" + ProdutoJsonString_2.Replace("'", "") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                }
                else
                {
                    sqlMS2.WriteData(@"update Produtivo.SCI_CONSULTA set IC_ERRO = 'S', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                }



            }

            sqlMS2.WriteData(@" declare @CPF varchar(11)
                        set @CPF = right('00000000000' + '" + txtCPF.Text + @"', 11)
                        EXEC Produtivo.SP_TOPDADOS_RETORNO_API @CPF, " + id_usuario);

            sqlMS1.MSSqlConectionClose();
            sqlMS2.MSSqlConectionClose();
        }

        private async void Consulta2()
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS1 = new MSSqlConection();
            MSSqlConection sqlMS2 = new MSSqlConection();

            string TOKEN = "";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api-seguranca.repicc.com.br/Seguranca/GetToken");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("TOPDADOS:Apidbm1521")));
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                var ProdutoJsonString = await response.Content.ReadAsStringAsync();

                var jObject = JObject.Parse(ProdutoJsonString);
                TOKEN = jObject["token"].ToString();

                sqlMS1.WriteData(@"insert into Produtivo.SCI_TOKEN (DS_TOKEN) select '" + TOKEN + "'");

                sqlMS1.WriteData(@"insert into [Produtivo].[REPICC_CONSULTA] (NU_DOCUMENTO, ID_USUARIO_CONSULTOU) select right('00000000000' + '" + txtCPF.Text + "', 11), " + id_usuario);
                string CPF = txtCPF.Text;

                var consultaUrl = "https://api.repicc.com.br/Dados/Pessoa/ConsultaRelacionamentos?cpf=" + CPF;
                var consultaRequest = new HttpRequestMessage(HttpMethod.Get, consultaUrl);
                consultaRequest.Headers.Add("Authorization", "Bearer " + TOKEN);

                var consultaResponse = await client.SendAsync(consultaRequest);

                if (consultaResponse.IsSuccessStatusCode)
                {
                    var ProdutoJsonString_2 = await consultaResponse.Content.ReadAsStringAsync();
                    sqlMS2.WriteData(@"update [Produtivo].[REPICC_CONSULTA] set IC_ERRO = 'N', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_JSON_RESULTADO = '" + ProdutoJsonString_2.Replace("'", "") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                }
                else
                {
                    sqlMS2.WriteData(@"update [Produtivo].[REPICC_CONSULTA] set IC_ERRO = 'S', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                }



            }

            sqlMS2.WriteData(@" declare @CPF varchar(11)
                        set @CPF = right('00000000000' + '" + txtCPF.Text + @"', 11)
                        EXEC Produtivo.SP_TOPDADOS_RETORNO_API_REPICC_RELACIONAMENTO @CPF, " + id_usuario);

            Response.Redirect("ConsultaCPF.aspx?Pesquisa=1&CPF=" + txtCPF.Text);

            sqlMS1.MSSqlConectionClose();
            sqlMS2.MSSqlConectionClose();
        }

        protected void CarregarDocumento()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT a.CONTATOS_ID,A.cpf,a.TITULO_ELEITOR,a.RG,A.RG_DATA_EMISSAO,c.CNS,b.PIS,d.NIS,e.NUMERO_CTPS,e.SERIE_CTPS,e.DT_EMISSAO_CTPS,e.UF_EMISSAO_CTPS
            FROM Produtivo.PESSOA_FISICA  a 
            left join dbo.TB_PIS b on a.CONTATOS_ID = b.CONTATOS_ID
            left join dbo.TB_CNS c on a.CONTATOS_ID = c.CONTATOS_ID
            left join dbo.TB_NIS d on a.CONTATOS_ID = d.CONTATOS_ID
			left join dbo.TB_CTPS e on a.CONTATOS_ID = e.CONTATOS_ID
            where a.CONTATOS_ID ='" + txtContatosID.Value + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbdoc.Visible = true;
                    txtContatosID.Value = reader["CONTATOS_ID"].ToString();
                    RcTe.Text = reader["TITULO_ELEITOR"].ToString();
                    RcRG.Text = reader["RG"].ToString();
                    RcRgDtE.Text = reader["RG_DATA_EMISSAO"].ToString();
                    RcCNS.Text = reader["CNS"].ToString();
                    RcPIS.Text = reader["PIS"].ToString();
                    RcNIS.Text = reader["NIS"].ToString();
                    RcNCTPS.Text = reader["NUMERO_CTPS"].ToString();
                    RcSCTPS.Text = reader["SERIE_CTPS"].ToString();
                    RcDtECTPS.Text = reader["DT_EMISSAO_CTPS"].ToString();
                    RcUFCTPS.Text = reader["UF_EMISSAO_CTPS"].ToString();

                }
            }
            reader.Close();
            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarCNH()
        {

            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT CONTATOS_ID,CNH,DATA_HABILITACAO,COD_SEGURANCA,FORMULARIO_CNH,FORMULARIO_RENACH,NUMERO_PGU,CATEGORIAL_ATUAL,SITUACAO_CNH,DT_ULTIMA_EMISSAO,DATA_VALIDADE,LOCAL_EMISSAO
            FROM Produtivo.PESSOA_FISICA
            where CONTATOS_ID = '" + txtContatosID.Value + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    CNH.Visible = true;
                    txtContatosID.Value = reader["CONTATOS_ID"].ToString();
                    CNHLabel.Text = reader["CNH"].ToString();
                    DATA_HABILITACAOLabel.Text = reader["DATA_HABILITACAO"].ToString();
                    COD_SEGURANCALabel.Text = reader["COD_SEGURANCA"].ToString();
                    FORMULARIO_CNHLabel.Text = reader["FORMULARIO_CNH"].ToString();
                    FORMULARIO_RENACHLabel.Text = reader["FORMULARIO_RENACH"].ToString();
                    NUMERO_PGULabel.Text = reader["NUMERO_PGU"].ToString();
                    CATEGORIAL_ATUALLabel.Text = reader["CATEGORIAL_ATUAL"].ToString();
                    SITUACAO_CNHLabel.Text = reader["SITUACAO_CNH"].ToString();
                    DT_ULTIMA_EMISSAOLabel.Text = reader["DT_ULTIMA_EMISSAO"].ToString();
                    DATA_VALIDADELabel.Text = reader["DATA_VALIDADE"].ToString();
                    LOCAL_EMISSAOLabel.Text = reader["LOCAL_EMISSAO"].ToString();

                }
            }
            reader.Close();
            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarRelacionamentos()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridRelacionamentos.DataSource = sqlMS.ReadData(@"select b.CPF,b.NOME,Convert(varchar(10), b.NASC,103) AS 'DT NASC'
                                                            ,case when b.SEXO = 'F' then 'ESPOSA'
                                                            when b.SEXO = 'M' then 'MARIDO'
                                                            else b.sexo end as 'VINCULO'
                                                            from Produtivo.PESSOA_FISICA as ab
                                                            inner join TopDados.Produtivo.PESSOA_FISICA as b on ab.CONTATOS_ID_CONJUGE = b.CONTATOS_ID
                                                            where (ab.CONTATOS_ID = " + txtContatosID.Value + @") AND (b.NOME IS NOT NULL)

                                                            UNION

                                                                select distinct b.CPF_RELACIONAMENTO, b.NOME, Convert(varchar(10), a.NASC, 103) as 'DT NASC', b.VINCULO
                                                            from TopDados.Produtivo.RELACIONAMENTO b
                                                            left join Produtivo.PESSOA_FISICA a on b.CPF_RELACIONAMENTO = a.CPF 
                                                            where(b.CONTATOS_ID = " + txtContatosID.Value + @") AND(b.NOME IS NOT NULL)

                                                            UNION

                                                            select CPF = '', a.NOME_MAE, 'DT NASC' = '', VINCULO = 'MÃE'
                                                            from Produtivo.PESSOA_FISICA a
                                                            where (a.CONTATOS_ID = " + txtContatosID.Value + @") and(a.NOME_MAE IS NOT NULL)

                                                            UNION

                                                            select CPF = '', a.NOME_PAI, 'DT NASC' = '', VINCULO = 'PAI'
                                                            from Produtivo.PESSOA_FISICA a
                                                            where (a.CONTATOS_ID = " + txtContatosID.Value + @") AND(a.NOME_PAI IS NOT NULL)

                                                            UNION

                                                            SELECT b.CPF, b.NOME, Convert(varchar(10), b.NASC, 103) AS 'DT NASC', VINCULO = 'MESMA RESIDENCIA'
                                                            FROM Produtivo.PESSOA_FISICA a
                                                            INNER JOIN Produtivo.PESSOA_FISICA b on b.HOUSEHOLD_ID_2016 = a.HOUSEHOLD_ID_2016
                                                            WHERE(a.CONTATOS_ID = " + txtContatosID.Value + @") AND(b.CONTATOS_ID <> a.CONTATOS_ID)

                                                            UNION

                                                            SELECT b.CPF, b.NOME, Convert(varchar(10), b.NASC, 103) AS 'DT NASC', VINCULO = 'MESMA RESIDENCIA'
                                                            FROM Produtivo.PESSOA_FISICA a
                                                            INNER JOIN Produtivo.PESSOA_FISICA b on b.HOUSEHOLD_ID = a.HOUSEHOLD_ID
                                                            WHERE(a.CONTATOS_ID = " + txtContatosID.Value + @") AND(b.CONTATOS_ID <> a.CONTATOS_ID) ORDER BY VINCULO ASC");

            GridRelacionamentos.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarEndereco()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEndedeco.DataSource = sqlMS.ReadData(@"SELECT  d.TIPO_LOGRADOURO + ' ' + d.LOGRADOURO + ' ' + d.NUMERO + ' ' + d.COMPLEMENTO AS ENDERECO, d.BAIRRO, e.MUNICIPIO as CIDADE, d.UF, right('00000000' + d.CEP, 8) as CEP 
                                                                FROM[TopDados].[Produtivo].PESSOA_FISICA a
                                                                inner join[TopDados].[Produtivo].[EMPRESAS_SOCIOS] b on a.CONTATOS_ID = b.CONTATOS_ID_SOCIO
                                                                inner join[TopDados].[Produtivo].[EMPRESAS] c on b.EMPRESAS_ID = c.EMPRESAS_ID
                                                                inner join[TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO] d on left(right('00000000000000' + c.cnpj, 14), 8) = CNPJ_BASICO and substring(right('00000000000000' + c.CNPJ, 14), 9, 4) = d.CNPJ_ORDEM AND right(c.CNPJ, 2) = d.CNPJ_DV
                                                                Inner join[TopDados].[Produtivo].EMPRESAS_RECEITA_MUNICIPIO e on d.MUNICIPIO = e.CD_MUNICIPIO


                                                                where(a.CONTATOS_ID = " + txtContatosID.Value + @") AND(d.BAIRRO IS NOT NULL)

                                                                UNION

                                                                SELECT a.ENDERECO, a.BAIRRO, a.CIDADE, a.UF, right('00000000' + a.CEP, 8) as CEP
                                                                FROM[TopDados].[Produtivo].[ENDERECO] a
                                                                WHERE(a.CONTATOS_ID = " + txtContatosID.Value + @") AND(a.ENDERECO IS NOT NULL)

                                                                UNION

                                                                SELECT a.ENDERECO, a.BAIRRO, a.CIDADE, a.UF, right('00000000' + a.CEP, 8) as CEP
                                                                FROM[TopDados].[Produtivo].[ENDERECO_NOVOS] a
                                                                WHERE(a.CONTATOS_ID = " + txtContatosID.Value + @") AND(a.ENDERECO IS NOT NULL)");
            GridEndedeco.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarSocios()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridSocios.DataSource = sqlMS.ReadData(@"SELECT c.CNPJ,c.RAZAO_SOCIAL as 'RAZAO SOCIAL',c.NOME_FANTASIA as 'NOME FANTASIA',b.PARTICIPACAO_SOCIO AS 'PARTICIPACAO SOCIEDADE', d.CNAE_DESCRICAO AS 'CNAE'
                                                    FROM [TopDados].[Produtivo].PESSOA_FISICA a 
                                                    inner join [TopDados].[Produtivo].[EMPRESAS_SOCIOS] b on a.CONTATOS_ID = b.CONTATOS_ID_SOCIO
                                                    inner join [TopDados].[Produtivo].[EMPRESAS] c on b.EMPRESAS_ID = c.EMPRESAS_ID
                                                    inner join [TopDados].[Produtivo].[EMPRESAS_CNAE] d on c.CNAE_SG = d.CNAE_SUBCLASSE
                                                    where a.CONTATOS_ID = '" + txtContatosID.Value + @"'


                                                    union

                                                    SELECT d.CNPJ_BASICO + d.CNPJ_ORDEM + d.CNPJ_DV as CNPJ
                                                          , c.RAZAO_SOCIAL as 'RAZAO SOCIAL'
                                                          , d.NOME_FANTASIA as 'NOME FANTASIA'
                                                          , '0.0'
                                                          , e.CNAE_DESCRICAO
                                                    FROM[TopDados].[Produtivo].[EMPRESAS_RECEITA_SOCIOS] a
                                                    inner join Produtivo.PESSOA_FISICA b on a.NOME_SOCIO = b.NOME AND right(left(b.CPF, 9), 6) = right(left(a.[CPF_CNPJ], 9), 6)
                                                    inner join[TopDados].[Produtivo].[EMPRESAS_RECEITA_EMPRESAS] c on a.CNPJ_BASICO = c.CNPJ_BASICO
                                                    inner join[TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO] d on a.CNPJ_BASICO = d.CNPJ_BASICO
                                                    inner join[TopDados].[Produtivo].[EMPRESAS_RECEITA_CNAE] e on d.CNAE_PRINCIPAL = e.CNAE
                                                    where b.CONTATOS_ID = '" + txtContatosID.Value + "'");
            GridSocios.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarTelefone()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridTelefone.DataSource = sqlMS.ReadData(@"SELECT DDD,TELEFONE
                                                    FROM [TopDados].[Produtivo].[TELEFONE] 
                                                    WHERE (CONTATOS_ID = '" + txtContatosID.Value + @"')
                                                    UNION
                                                    SELECT dDD,TELEFONE
                                                    FROM [TopDados].[Produtivo].[TELEFONE_NOVOS] 
                                                    WHERE (CONTATOS_ID = '" + txtContatosID.Value + @"')");
            GridTelefone.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarEmail()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEmail.DataSource = sqlMS.ReadData(@"select EMAIL collate Latin1_General_CI_AI as EMAIL
                                                    FROM [TopDados].[Produtivo].[EMAIL] a
                                                    where CONTATOS_ID = '" + txtContatosID.Value + @"'
                                                    UNION
                                                    select EMAIL
                                                    FROM[TopDados].[dbo].[EMAIL]
                                                    where CONTATOS_ID = '" + txtContatosID.Value + "'");
            GridEmail.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarVeiculos()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridVeiculos.DataSource = sqlMS.ReadData(@"SELECT [PLACA],[RENAVAN],[MARCA],[ANOFAB] AS 'ANO FABRICAÇÃO',[END]+', '+[NUM]+' '+[COMPL] as 'ENDEREÇO',[BAIRRO],[CIDADE],[ESTADO],[CEP]
                                    FROM [TopDados].[dbo].[VEICULO_PESSOA_FISICA]
                                    where contatos_id='" + txtContatosID.Value + "'");
            GridVeiculos.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarEmpregos()
        {

            MSSqlConection sqlMS = new MSSqlConection();

            GridEmpreg.DataSource = sqlMS.ReadData(@"SELECT [RAZAO_SOCIAL] AS 'RAZÃO SOCIAL'
                                                    ,a.[TIPO] AS 'TIPO DOC'
                                                    ,[DOC] AS 'DOC EMPRESA'
                                                    ,format ([REMUNERACAO],'c','pt-br') AS 'SALÁRIO'
                                                    ,b.titulo AS 'CARGO'
                                                    FROM [TopDados].[dbo].[TB_EMPREGOS] a
                                                    LEFT join TopDados.Produtivo.OCUPACAO b on a.CBO = b.CBO
                                                    WHERE A.CONTATOS_ID = '" + txtContatosID.Value + "'");
            GridEmpreg.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void BtnBigData_Click(object sender, EventArgs e)
        {
            ConsultaBigData();
        }

        private async void ConsultaBigData()
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS1 = new MSSqlConection();
            MSSqlConection sqlMS2 = new MSSqlConection();

            string TOKEN = "";
            string TOKENID = "";

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://plataforma.bigdatacorp.com.br/tokens/gerar"),
                Headers = { { "accept", "application/json" }, },
                Content = new StringContent("{\"password\":\"37jvgnyq\",\"login\":\"luciano.ribeiro@topdados.com.br\",\"expires\":24}")
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                }
            };
            using (var response = await client.SendAsync(request))
            {

                if (response.IsSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var ProdutoJsonString = await response.Content.ReadAsStringAsync();

                    var jObject = JObject.Parse(ProdutoJsonString);
                    TOKEN = jObject["token"].ToString();
                    TOKENID = jObject["tokenID"].ToString();


                    sqlMS1.WriteData(@"insert into Produtivo.BIGDATA_TOKEN (DS_TOKEN,DS_TOKEN_ID) select '" + TOKEN + "','" + TOKENID + "' ");

                    sqlMS1.WriteData(@"insert into Produtivo.BIGDATA_CONSULTA (NU_DOCUMENTO, ID_USUARIO_CONSULTOU) select right('00000000000' + '" + txtCPF.Text + "', 11), " + id_usuario);

                    string CPF = txtCPF.Text;



                    string jsonurl = "{\"q\":\"doc{'" + CPF + "'}\",\"Datasets\":\"basic_data,phones_extended,addresses_extended,emails_extended,business_relationships,related_people,occupation_data\"}";
                    var consultarequest = new HttpRequestMessage
                    {

                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://plataforma.bigdatacorp.com.br/pessoas"),
                        Headers = { { "accept", "application/json" }, { "AccessToken", TOKEN }, { "TokenId", TOKENID }, },
                        Content = new StringContent(jsonurl)
                        {
                            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                        }
                    };
                    using (var consultaResponse = await client.SendAsync(consultarequest))
                    {
                        if (consultaResponse.IsSuccessStatusCode)
                        {
                            consultaResponse.EnsureSuccessStatusCode();
                            var body2 = await consultaResponse.Content.ReadAsStringAsync();

                            var ProdutoJsonString_2 = await consultaResponse.Content.ReadAsStringAsync();

                            sqlMS2.WriteData(@"update Produtivo.BIGDATA_CONSULTA set IC_ERRO = 'N', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_JSON_RESULTADO = '" + ProdutoJsonString_2.Replace("'", "") + "', DS_TOKEN = '" + TOKEN + "', DS_TOKEN_ID = '" + TOKENID + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                        }
                        else
                        {
                            sqlMS2.WriteData(@"update Produtivo.BIGDATA_CONSULTA set IC_ERRO = 'S', DT_CONSULTA = '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', DS_TOKEN = '" + TOKEN + "' where NU_DOCUMENTO = right('00000000000' + '" + CPF + "', 11) and DT_CONSULTA is null");
                        }
                    }


                }


                sqlMS2.WriteData(@" declare @CPF varchar(11)
                        set @CPF = right('00000000000' + '" + txtCPF.Text + @"', 11)
                        EXEC Produtivo.SP_BIGDATA_RETORNO_API @CPF, " + id_usuario);



                sqlMS1.MSSqlConectionClose();
                sqlMS2.MSSqlConectionClose();

                Response.Redirect("ConsultaCPF.aspx?Pesquisa=1&CPF=" + txtCPF.Text);
            }


        }

        protected void CarregarButttonEnd()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"select * from topdados.dbo.PROVAVEIS_ENDERECO
                                                   where cpf =right('00000000000'+'" + txtCPF.Text + "',11)");
            if (reader.HasRows)
            {
                Btn_End_Rel.Visible = true;
            }
            else
            {
                Btn_End_Rel.Visible = false;
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            
        }

        protected void CarregarButttonTel()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"select * from topdados.dbo.PROVAVEIS_TELEFONE
                                                   where cpf =right('00000000000'+'" + txtCPF.Text + "',11)");
            if (reader.HasRows)
            {
                Btn_Tel_Rel.Visible = true;
            }
            else
            {
                Btn_Tel_Rel.Visible = false;
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            

        }

        protected void CarregarButttonEmail()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"select * from topdados.dbo.PROVAVEIS_email
                                                   where cpf =right('00000000000'+'" + txtCPF.Text + "',11)");
            if (reader.HasRows)
            {
                Btn_Email_Rel.Visible = true;
            }
            else
            {
                Btn_Email_Rel.Visible = false;
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            

        }

    }
}