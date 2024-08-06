using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Topdados20
{
    public partial class CONSULTACNPJ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MSgCNPJne.Visible = false;
            tbddcad.Visible = false;
        }

        protected void BtnCNPJ_Click(object sender, EventArgs e)
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

            SqlDataReader reader2 = sqlMS.ReadData(@"SELECT w.[CNPJ_BASICO],w.[CNPJ_ORDEM],w.[CNPJ_DV] FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO] w
                                                     where w.CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + @"', 14), 8)
                                                    and   w.CNPJ_ORDEM = substring(right('00000000000000' + '" + txtCNPJ.Text + @"', 14), 9, 4)
                                                    and   w.CNPJ_DV = right('" + txtCNPJ.Text + @"', 2)");
            if (reader2.HasRows)
            {
                
                
            }
            else
            {
                MSgCNPJne.Visible = true;

            }
            SqlDataReader reader = sqlMS.ReadData(@"SELECT a.NOME_FANTASIA,b.RAZAO_SOCIAL,b.CAPITAL_SOCIAL,e.NATUREZA_JURIDICA,g.DS_PORTE,a.[CNPJ_BASICO]+a.[CNPJ_ORDEM]+a.[CNPJ_DV] as [CNPJTRATADO],substring(a.DATA_INICIO_ATIVIDADE,7,2) +'/'+ substring(a.DATA_INICIO_ATIVIDADE,5,2) + '/' +substring(a.DATA_INICIO_ATIVIDADE,1,4) as [DATA_INICIO_ATIVIDADE],c.SITUACAO_CADASTRAL as [MOTIVO],case when a.SITUACAO_CADASTRAL = '01' then 'NULA' when a.SITUACAO_CADASTRAL = '02' then 'ATIVA' when a.SITUACAO_CADASTRAL = '03' then 'SUSPENSA' when a.SITUACAO_CADASTRAL = '04' then 'INAPTA' when a.SITUACAO_CADASTRAL = '08' then 'BAIXADA' end as [SITUACAO_CADASTRAL], substring(a.DATA_SITUACAO_CADASTRAL,7,2) +'/'+ substring(a.DATA_SITUACAO_CADASTRAL,5,2) + '/' + substring(a.DATA_SITUACAO_CADASTRAL,1,4) as [DATA_SITUACAO_CADASTRAL], f.CNAE_DESCRICAO 
                                                    FROM Produtivo.EMPRESAS_RECEITA_ESTABELECIMENTO a
                                                    inner join Produtivo.EMPRESAS_RECEITA_EMPRESAS b on a.CNPJ_BASICO = b.CNPJ_BASICO
                                                    inner join Produtivo.EMPRESAS_RECEITA_SITUACAO_CADASTRAL c on a.MOTIVO_SITUACAO = c.CD_SITUACAO_CADASTRAL
                                                    inner join Produtivo.EMPRESAS_SITUACAO_CADASTRAL d on a.SITUACAO_CADASTRAL = d.COD_SIT_CAD
                                                    inner join Produtivo.EMPRESAS_RECEITA_NATUREZA_JURIDICA e on b.NATUREZA_JURIDICA = e.CD_NATUREZA_JURIDICA
                                                    inner join Produtivo.EMPRESAS_RECEITA_CNAE f on a.CNAE_PRINCIPAL = f.CNAE
                                                    inner join Produtivo.EMPRESAS_PORTE g on b.PORTE = g.FX_PORTE
                                                    where (a.CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + "', 14), 8) and a.CNPJ_ORDEM = substring(right('00000000000000' + '" + txtCNPJ.Text + "', 14), 9, 4) and a.CNPJ_DV = right('" + txtCNPJ.Text + "', 2))");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tbddcad.Visible = true;
                    LbCNPJ.Text = reader["CNPJTRATADO"].ToString();
                    LbRSocial.Text = reader["RAZAO_SOCIAL"].ToString();
                    LbNFatasia.Text = reader["NOME_FANTASIA"].ToString();
                    LbCapSocial.Text = reader["CAPITAL_SOCIAL"].ToString();
                    LbNatJur.Text = reader["NATUREZA_JURIDICA"].ToString();
                    LbPorte.Text = reader["DS_PORTE"].ToString();
                    LbDtFund.Text = reader["DATA_INICIO_ATIVIDADE"].ToString();
                    LbMotStCad.Text = reader["MOTIVO"].ToString();
                    LbStCad.Text = reader["SITUACAO_CADASTRAL"].ToString();
                    LbDtSt.Text = reader["DATA_SITUACAO_CADASTRAL"].ToString();
                    LbCnae.Text = reader["CNAE_DESCRICAO"].ToString();
                    LbPorte.Text = reader["DS_PORTE"].ToString();

                    CarregarEndereco();
                    CarregarFiliais();
                    CarregarSocios();
                    CarregarTelefones();
                    CarregarEmail();
                    CarregarVeiculos();
                }

                sqlMS.MSSqlConectionClose();
                reader.Close();
                reader2.Close();

            }



        }

        protected void CarregarEndereco()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEndCNPJ.DataSource = sqlMS.ReadData(@"SELECT a.[TIPO_LOGRADOURO] +' ' +a.[LOGRADOURO] +', ' +a.[NUMERO]+' '+a.[COMPLEMENTO] as 'ENDEREÇO'
                                                    ,a.[BAIRRO]
                                                    ,a.[CEP]
                                                    ,a.[UF]
                                                    ,b.MUNICIPIO
      
                                                    FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO] a
                                                    inner join Produtivo.EMPRESAS_RECEITA_MUNICIPIO b on a.MUNICIPIO = b.CD_MUNICIPIO
                                                    where (a.CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + @"', 14), 8) and a.CNPJ_ORDEM = substring(right('00000000000000' + '" + txtCNPJ.Text + @"', 14), 9, 4) and a.CNPJ_DV = right('" + txtCNPJ.Text + @"', 2))
                                                    union all
                                                    SELECT a.LOGR_TIPO +' '+a.LOGR_TITULO+' '+a.LOGR_NOME +', '+a.LOGR_NUMERO+' '+a.LOGR_COMPLEMENTO as 'ENDEREÇO'
                                                    ,a.BAIRRO,a.CEP,a.UF,a.CIDADE
                                                    from Produtivo.EMPRESAS a
                                                    where (a.CNPJ = right('00000000000000' + ' " + txtCNPJ.Text + @"',14))

                                                    UNION all

                                                    select ENDERECO collate Latin1_General_CI_AS,BAIRRO collate Latin1_General_CI_AS,CEP collate Latin1_General_CI_AS,ESTADO collate Latin1_General_CI_AS,CIDADE collate Latin1_General_CI_AS from dbo.EMPRESAS_ENDERECOS
                                                    WHERE CNPJ = '" + txtCNPJ.Text + @"'");

            GridEndCNPJ.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarFiliais()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridFiliais.DataSource = sqlMS.ReadData(@"SELECT a.[CNPJ_BASICO]+a.[CNPJ_ORDEM]+a.[CNPJ_DV] AS 'CNPJ'
                                                        ,a.[NOME_FANTASIA] AS 'NOME FANTASIA'
                                                        ,substring(a.DATA_INICIO_ATIVIDADE,7,2) +'/'+ substring(a.DATA_INICIO_ATIVIDADE,5,2) + '/' +substring(a.DATA_INICIO_ATIVIDADE,1,4) as 'DATA INICIO ATIVIDADE'
                                                        ,case when 
                                                        SITUACAO_CADASTRAL = '01' then 'NULA' when SITUACAO_CADASTRAL = '02' then 'ATIVA' when SITUACAO_CADASTRAL = '03' then 'SUSPENSA' when a.SITUACAO_CADASTRAL = '04' then 'INAPTA' when a.SITUACAO_CADASTRAL = '08' then 'BAIXADA' end as [SITUACAO CADASTRAL]
                                                        ,substring(a.[DATA_SITUACAO_CADASTRAL],7,2) +'/'+ substring(a.[DATA_SITUACAO_CADASTRAL],5,2) + '/' +substring(a.[DATA_SITUACAO_CADASTRAL],1,4) as 'DATA SITUACAO CADASTRAL'
      
                                                        ,a.[TIPO_LOGRADOURO]+' '+a.[LOGRADOURO]+' ,'+a.[NUMERO]+' '+a.[COMPLEMENTO] as 'ENDEREÇO'
                                                        ,a.[BAIRRO]
                                                        ,b.MUNICIPIO as 'CIDADE'
                                                        ,a.[UF]
                                                        ,a.[CEP]
                                                        FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO] a
                                                        inner join Produtivo.EMPRESAS_RECEITA_MUNICIPIO b on a.MUNICIPIO = b.CD_MUNICIPIO
                                                        where (CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + "', 14), 8)) and a.CNPJ_BASICO + a.CNPJ_ORDEM + a.CNPJ_DV <> right('00000000000000' + '" + txtCNPJ.Text + "', 14)");

            GridFiliais.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarSocios()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridSocios.DataSource = sqlMS.ReadData(@"SELECT [CPF_CNPJ] AS 'CPF RECEITA', 
                                                    b.CPF
                                                    ,a.[NOME_SOCIO] AS 'NOME SÓCIO'
                                                    ,substring(a.[DATA_ENTRADA_SOCIEDADE],7,2) +'/'+ substring(a.[DATA_ENTRADA_SOCIEDADE],5,2) + '/' +substring(a.[DATA_ENTRADA_SOCIEDADE],1,4) as 'DATA ENTRADA SOCIEDADE'
                                                    ,c.[QUALIFICACAO_SOCIO] AS 'QUALIFICAÇÃO'
                                                    FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_SOCIOS] a
                                                    inner join Produtivo.PESSOA_FISICA b on a.NOME_SOCIO = b.NOME AND right(left(b.CPF, 9), 6) = right(left(a.[CPF_CNPJ], 9), 6)
                                                    inner join Produtivo.EMPRESAS_RECEITA_QUALIFICACAO_SOCIO c on a.QUALIFICACAO_SOCIO = c.CD_QUALIFICACAO_SOCIO
                                                    WHERE (CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + "', 14), 8)) ORDER BY CNPJ_BASICO ASC");

            GridSocios.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarTelefones()
        {

            MSSqlConection sqlMS = new MSSqlConection();

            GridTelefone.DataSource = sqlMS.ReadData(@"SELECT [DDD1] AS 'DDD',[TELEFONE1] AS 'TELEFONE'
                                                        FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO]
                                                        where (CNPJ_BASICO = left(right('00000000000000' + '" + txtCNPJ.Text + @"', 14), 8)) AND DDD1 != ''

                                                        UNION

                                                        SELECT[DDD2] AS 'DDD',[TELEFONE2] AS 'TELEFONE'
                                                        FROM[TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO]
                                                        where(CNPJ_BASICO = left(right('00000000000000' + '"+txtCNPJ.Text+@"', 14), 8)) AND DDD2 != ''

                                                        UNION

                                                        SELECT[DDD3] AS 'DDD',[TELEFONE3] AS 'TELEFONE'
                                                        FROM[TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO]
                                                        where(CNPJ_BASICO = left(right('00000000000000' + '"+txtCNPJ.Text+@"', 14), 8)) AND DDD3 != ''

                                                        UNION

                                                        SELECT a.[DDD], a.[TELEFONE]
                                                        FROM[TopDados].[Produtivo].[EMPRESAS_TELEFONE] a
                                                        inner join Produtivo.EMPRESAS b on a.EMPRESAS_ID = b.EMPRESAS_ID
                                                        where b.cnpj = '"+txtCNPJ.Text+@"'");

            GridTelefone.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarEmail()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEmail.DataSource = sqlMS.ReadData(@"SELECT [EMAIL] collate Latin1_General_CI_AS  AS 'EMAIL'
                                                        FROM [TopDados].[Produtivo].[EMPRESAS_RECEITA_ESTABELECIMENTO]
                                                        where (CNPJ_BASICO = left(right('00000000000000' + '"+ txtCNPJ.Text+ @"', 14), 8))

                                                        union 

                                                        SELECT [EMAIL] collate Latin1_General_CI_AS  AS 'EMAIL'
                                                        FROM [TopDados].[Produtivo].[EMPRESAS_EMAIL] a 
                                                        inner join Produtivo.EMPRESAS b on a.EMPRESAS_ID = b.EMPRESAS_ID
                                                        where b.cnpj= '"+ txtCNPJ.Text+"'");

            GridEmail.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarVeiculos()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridVeiculos.DataSource = sqlMS.ReadData(@"SELECT [PLACA],[RENAVAN],[MARCA],[ANOFAB] as 'ANO FABRICAÇÃO',[ENDERECO],[BAIRRO],[CIDADE],[ESTADO],[CEP]
                                                          FROM [TopDados].[dbo].[VEICULOS_EMPRESAS]
                                                        WHERE CNPJ = '" + txtCNPJ.Text + "'");

            GridVeiculos.DataBind();

            sqlMS.MSSqlConectionClose();
        }
    }
}