using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Topdados20
{
    public partial class AtualizacaoCPF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MsgOk.Visible = false;
            LbSalvo2.Visible = false;
            LbMsg3.Visible = false;

        }

        protected void BtnBuscaCPF_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();

            }

            MSSqlConection sqlMS2 = new MSSqlConection();
            SqlDataReader reader2 = sqlMS2.ReadData(@"select * from Produtivo.PESSOA_FISICA where cpf = '" + txtCPF.Text + "'");

            if (reader2.HasRows)
            {
                MSSqlConection sqlMS = new MSSqlConection();

                sqlMS.ExecuteNonQuery(@"insert into [TopDados].[Produtivo].[USUARIO_LOG]
                                (ID_USUARIO,DT_ATIVIDADE,DS_ATIVIDADE,NU_CPF)
                                VALUES
                                ('" + id_usuario + "',GETDATE(),'Buscar CPF','" + txtCPF.Text + "')");



                SqlDataReader reader = sqlMS.ReadData(@"SELECT TOP (1)
                                                    a.CONTATOS_ID, 
                                                    a.CPF, 
                                                    a.NOME, 
                                                    a.NOME_MAE, 
                                                    a.NOME_PAI ,
                                                    a.NASC, 
                                                    a.SEXO,
                                                    a.RENDA,
                                                    case 
                                                    WHEN a.CBO IS NULL THEN '0'
                                                    ELSE a.CBO
                                                    END 'CBO', 
                                                    a.ESTCIV,
                                                    CASE
                                                    WHEN a.CD_SIT_CAD IS NULL 
                                                    THEN '4' ELSE a.CD_SIT_CAD END AS 'CD_SIT_CAD',
                                                    a.DT_SIT_CAD, 
                                                    a.DT_OB
                                                    FROM Produtivo.PESSOA_FISICA AS a 
                                                    WHERE(a.CPF = right('00000000000' + '" + txtCPF.Text + "',11))");
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtcontatoid.Value = reader["CONTATOS_ID"].ToString();
                        txtNome.Text = reader["NOME"].ToString();
                        txtNMAe.Text = reader["NOME_MAE"].ToString();
                        txtNPai.Text = reader["NOME_PAI"].ToString();
                        txtDtNasc.Text = reader["NASC"].ToString();
                        txtSx.Text = reader["SEXO"].ToString();
                        txtRda.Text = reader["RENDA"].ToString();
                        ListOcup.SelectedValue = reader["CBO"].ToString();
                        ltestcivil.SelectedValue = reader["ESTCIV"].ToString();
                        LsSit.Text = reader["CD_SIT_CAD"].ToString();
                        txtdtsitcad.Text = reader["DT_SIT_CAD"].ToString();
                        txtDtOb.Text = reader["DT_OB"].ToString();

                    }
                }


                CarregarEscolaridade();
                CarregarDocumentos();
                CarregarPIS();
                CarregarCNS();
                CarregarEndereco();
                CarregarTelefone();
                CarregarEmail();
                reader.Close();
            }

            else
            {
                CPF.Visible = true;
                CPF.Text = "CPF Não Encontrado";

                txtRda.Text = "0";
                LsSit.SelectedValue = "2";

                LimparCampos();
            }


            


        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var ds_usuario = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                ds_usuario = htParametros["ds_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"select * from Produtivo.PESSOA_FISICA where cpf = '"+txtCPF.Text+"'");


            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID) 
                                        select '" + id_usuario + "', getdate(), 'Alterar Cadastro CPF', '" + txtcontatoid.Value + "'");

                sqlMS.ExecuteNonQuery(@"UPDATE Produtivo.PESSOA_FISICA
                                  SET
                                  NOME = '" + txtNome.Text + @"', 
                                  NOME_MAE = '" + txtNMAe.Text + @"', 
                                  NOME_PAI = '" + txtNPai.Text + @"' ,
                                  NASC = '" + txtDtNasc.Text + @"', 
                                  SEXO = '" + txtSx.Text + @"',
                                  RENDA = '" + Convert.ToSingle(txtRda.Text) + @"',
                                  CBO = '" + Convert.ToInt32(ListOcup.SelectedValue) + @"', 
                                  ESTCIV = '" + ltestcivil.SelectedValue + @"',
                                  CD_SIT_CAD = '" + Convert.ToInt32(LsSit.SelectedValue) + @"',
                                  DT_SIT_CAD = '" + txtdtsitcad.Text + @"', 
                                  DT_OB = '" + txtDtOb.Text + @"'
                                  WHERE(CONTATOS_ID = '" + txtcontatoid.Value + "')");


                MsgOk.Visible = true;
                MsgOk.Text = "Salvo com Sucesso";
            }

            else
            {
                

                sqlMS.ExecuteNonQuery(@"insert into Produtivo.PESSOA_FISICA
                                        (CONTATOS_ID,NOME,NOME_MAE,NOME_PAI,SEXO,RENDA,CBO,ESTCIV,CD_SIT_CAD,DT_SIT_CAD,DT_OB,DT_INCLUSAO,ORIGEM_DADO_TOPDADOS)

                                         VALUES ((SELECT MAX (CONTATOS_ID)+1 FROM Produtivo.PESSOA_FISICA),'"+txtNome.Text+ "','"+txtNMAe.Text+"','" + txtNPai.Text + "','"+ txtSx.Text + "','" + Convert.ToSingle(txtRda.Text) + "','" + Convert.ToInt32(ListOcup.SelectedValue) + "','" + ltestcivil.SelectedValue + "','" + Convert.ToInt32(LsSit.SelectedValue) + "','" + txtdtsitcad.Text + "','" + txtDtOb.Text + @"',GETDATE(),'"+ds_usuario+"')");
                
                
                MsgOk.Visible = true;
                MsgOk.Text = "Salvo com Sucesso";
            }
            

            sqlMS.MSSqlConectionClose();
        }

        protected void CarregarEscolaridade()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT b.[ESCOLARIDADE] FROM [TopDados].[dbo].[TB_ESCOLARIDADE] b
                                                    inner join Produtivo.PESSOA_FISICA a on b.CONTATOS_ID = a.CONTATOS_ID
                                                    where a.cpf = '" + txtCPF.Text + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    LsEscol.SelectedValue = reader["ESCOLARIDADE"].ToString();

                }
            }
            reader.Close();
        }

        protected void CarregarPIS()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT b.PIS FROM [TopDados].[dbo].[TB_PIS] b
                                                    inner join Produtivo.PESSOA_FISICA a on b.CONTATOS_ID = a.CONTATOS_ID
                                                    where a.CPF = '" + txtCPF.Text + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    TxtPIS.Text = reader["PIS"].ToString();

                }
            }
            reader.Close();
        }

        protected void CarregarCNS()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT b.CNS FROM [TopDados].[dbo].[TB_CNS] b
                                                    inner join Produtivo.PESSOA_FISICA a on b.CONTATOS_ID = a.CONTATOS_ID
                                                    where a.CPF = '" + txtCPF.Text + "'");
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    txtCNS.Text = reader["CNS"].ToString();

                }
            }
            reader.Close();
        }

        protected void CarregarDocumentos()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT 
                                                    RG, 
                                                    RG_DATA_EMISSAO, 
                                                    TITULO_ELEITOR,
                                                    CNH,
                                                    DATA_HABILITACAO,
                                                    COD_SEGURANCA,
                                                    FORMULARIO_CNH,
                                                    FORMULARIO_RENACH,
                                                    NUMERO_PGU,
                                                    CATEGORIAL_ATUAL,
                                                    SITUACAO_CNH,
                                                    DT_ULTIMA_EMISSAO,
                                                    DATA_VALIDADE,
                                                    LOCAL_EMISSAO
                                                    FROM Produtivo.PESSOA_FISICA
                                                    WHERE(CPF = '" + txtCPF.Text + "')");
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    txtRG.Text = reader["RG"].ToString();
                    RgDtEmi.Text = reader["RG_DATA_EMISSAO"].ToString();
                    txtTEleitor.Text = reader["TITULO_ELEITOR"].ToString();
                    txtCNH.Text = reader["CNH"].ToString();
                    txtDtHab.Text = reader["DATA_HABILITACAO"].ToString();
                    txtCodSeg.Text = reader["COD_SEGURANCA"].ToString();
                    txtFormCNH.Text = reader["FORMULARIO_CNH"].ToString();
                    txtformren.Text = reader["FORMULARIO_RENACH"].ToString();
                    txtNpgu.Text = reader["NUMERO_PGU"].ToString();
                    txtCatAt.Text = reader["CATEGORIAL_ATUAL"].ToString();
                    txtSitCNH.Text = reader["SITUACAO_CNH"].ToString();
                    txtDtUltEmi.Text = reader["DT_ULTIMA_EMISSAO"].ToString();
                    txtDtVal.Text = reader["DATA_VALIDADE"].ToString();
                    txtLocalEmis.Text = reader["LOCAL_EMISSAO"].ToString();
                }
            }
            reader.Close();
        }

        protected void CarregarEndereco()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEnd.DataSource = sqlMS.ReadData(@"SELECT a.HISTORICO_ENDERECOS_ID AS 'ENDERECO_ID',a.ENDERECO, a.BAIRRO, a.CIDADE, a.UF, right('00000000' + a.CEP, 8) as CEP
                                                FROM[TopDados].[Produtivo].[ENDERECO] a
                                                WHERE(a.CONTATOS_ID = '" + txtcontatoid.Value + @"') AND(a.ENDERECO IS NOT NULL)
                                                UNION
                                                SELECT a.ENDERECO_NOVOS_ID AS 'ENDERECO_ID',a.ENDERECO, a.BAIRRO, a.CIDADE, a.UF, right('00000000' + a.CEP, 8) as CEP
                                                FROM[TopDados].[Produtivo].[ENDERECO_NOVOS] a
                                                WHERE(a.CONTATOS_ID = '" + txtcontatoid.Value + "') AND(a.ENDERECO IS NOT NULL)");
            GridEnd.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarTelefone()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridTel.DataSource = sqlMS.ReadData(@"SELECT a.HISTORICO_TELEFONES_ID as 'TELEFONE_ID',a.DDD,a.TELEFONE
                                                FROM [TopDados].[Produtivo].[TELEFONE] a
                                                right join [TopDados].[Produtivo].[PESSOA_FISICA] b on a.CONTATOS_ID = b.CONTATOS_ID
                                                WHERE a.CONTATOS_ID = '" + txtcontatoid.Value + @"'
  
                                                UNION

                                                SELECT a.TELEFONE_NOVOS_ID as 'TELEFONE_ID',a.DDD,a.TELEFONE
                                                FROM [TopDados].[Produtivo].[TELEFONE_NOVOS] a
                                                right join [TopDados].[Produtivo].[PESSOA_FISICA] b on a.CONTATOS_ID = b.CONTATOS_ID
                                                WHERE a.CONTATOS_ID = '" + txtcontatoid.Value + "'");
            GridTel.DataBind();

            sqlMS.MSSqlConectionClose();

        }

        protected void CarregarEmail()
        {
            MSSqlConection sqlMS = new MSSqlConection();

            GridEmail.DataSource = sqlMS.ReadData(@"select EMAIL collate Latin1_General_CI_AI as EMAIL_ID,EMAIL collate Latin1_General_CI_AI as EMAIL
                                                    FROM [TopDados].[Produtivo].[EMAIL] a
                                                    where CONTATOS_ID = '" + txtcontatoid.Value + @"'
                                                    UNION ALL
                                                    select EMAIL as EMAIL_ID,EMAIL
                                                    FROM[TopDados].[dbo].[EMAIL]
                                                    where CONTATOS_ID = '" + txtcontatoid.Value + @"'");
            GridEmail.DataBind();

            sqlMS.MSSqlConectionClose();
        }

        protected void BtnEsco_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID) 
                                        select '" + id_usuario + "', getdate(), 'Alterar Cadastro CPF', '" + txtcontatoid.Value + "'");


            SqlDataReader reader = sqlMS.ReadData(@"select * from [dbo].[TB_ESCOLARIDADE] where CONTATOS_ID = '" + txtcontatoid.Value + "'");
            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"UPDATE [TopDados].[dbo].[TB_ESCOLARIDADE]
                SET
                ESCOLARIDADE = '" + LsEscol.SelectedValue + @"',
                CONTATOS_ID = '" + txtcontatoid.Value + @"'
                WHERE CONTATOS_ID = '" + txtcontatoid.Value + "'");


            }

            else
            {
                sqlMS.ExecuteNonQuery(@"insert into [TopDados].[dbo].[TB_ESCOLARIDADE]
	                                ([CONTATOS_ID],[ESCOLARIDADE],[ORIGEM_ESC],[CONSELHO_ORGAO],[DT_INCLUSAO])
	                                VALUES ('" + txtcontatoid.Value + "','" + LsEscol.SelectedValue + "','TB ESCOL','',GETDATE())");
            }



            reader.Close();
            sqlMS.MSSqlConectionClose();

            LbSalvo2.Visible = true;
            LbSalvo2.Text = "Salvo com Sucesso";

        }

        protected void BtnSalvarRG_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID) 
                                        select '" + id_usuario + "', getdate(), 'Alterar Cadastro CPF', '" + txtcontatoid.Value + "'");

            sqlMS.ExecuteNonQuery(@"UPDATE Produtivo.PESSOA_FISICA
                                    SET
                                    RG = '" + txtRG.Text + @"',
                                    RG_DATA_EMISSAO = '" + RgDtEmi.Text + @"', 
                                    TITULO_ELEITOR = '" + txtTEleitor.Text + @"',
                                    CNH = '" + txtCNH.Text + @"',
                                    DATA_HABILITACAO = '" + txtDtHab.Text + @"',
                                    COD_SEGURANCA = '" + txtCodSeg.Text + @"',
                                    FORMULARIO_CNH = '" + txtFormCNH.Text + @"',
                                    FORMULARIO_RENACH = '" + txtformren.Text + @"',
                                    NUMERO_PGU = '" + txtNpgu.Text + @"',
                                    CATEGORIAL_ATUAL = '" + txtCatAt.Text + @"',
                                    SITUACAO_CNH = '" + txtSitCNH.Text + @"',
                                    DT_ULTIMA_EMISSAO = '" + txtDtUltEmi.Text + @"',
                                    DATA_VALIDADE = '" + txtDtVal.Text + @"',
                                    LOCAL_EMISSAO = '" + txtLocalEmis.Text + @"'
                                    WHERE CPF = '" + txtCPF.Text + "'");

            sqlMS.MSSqlConectionClose();

            LbMsg3.Visible = true;
            LbMsg3.Text = "Salvo com Sucesso";
        }

        protected void BtnPIS_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID) 
                                        select '" + id_usuario + "', getdate(), 'Atualização PIS', '" + txtcontatoid.Value + "'");


            SqlDataReader reader = sqlMS.ReadData(@"SELECT * FROM [TopDados].[dbo].[TB_PIS] where CONTATOS_ID = '" + txtcontatoid.Value + "'");
            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"UPDATE TopDados.dbo.TB_PIS
                SET
                PIS = '" + TxtPIS.Text + @"'
                WHERE CONTATOS_ID = '" + txtcontatoid.Value + "'");

            }

            else
            {
                sqlMS.ExecuteNonQuery(@"insert into TopDados.dbo.TB_PIS
	                                ([CONTATOS_ID],[PIS],[CADASTRO_ID],[DT_INCLUSAO])
	                                VALUES ('" + txtcontatoid.Value + "','" + TxtPIS.Text + "',NULL,GETDATE())");
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();

            LbPIS.Visible = true;
            LbPIS.Text = "Salvo com Sucesso";
        }

        protected void BtnCNS_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID) 
                                        select '" + id_usuario + "', getdate(), 'Atualização CNS', '" + txtcontatoid.Value + "'");


            SqlDataReader reader = sqlMS.ReadData(@"SELECT * FROM [TopDados].[dbo].[TB_CNS] where CONTATOS_ID = '" + txtcontatoid.Value + "'");
            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"UPDATE TopDados.dbo.TB_CNS
                SET
                CNS = '" + TxtPIS.Text + @"'
                WHERE CONTATOS_ID = '" + txtcontatoid.Value + "'");

            }

            else
            {
                sqlMS.ExecuteNonQuery(@"insert into TopDados.dbo.TB_CNS
	                                ([CONTATOS_ID],[CPF],[CNS])
	                                VALUES ('" + txtcontatoid.Value + "','" + txtCPF.Text + "','" + txtCNS.Text + "')");
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();

            LbCNS.Visible = true;
            LbCNS.Text = "Salvo com Sucesso";

        }

        protected void GridEnd_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            GridView grv = ((GridView)sender);
            grv.SelectedIndex = -1;
            switch (e.CommandName)
            {
                case "editar":

                    MSSqlConection sqlMS = new MSSqlConection();

                    SqlDataReader reader = sqlMS.ReadData(@"SELECT HISTORICO_ENDERECOS_ID, ENDERECO, BAIRRO, CIDADE, UF, right('00000000' + CEP, 8) as CEP
                                                                FROM [TopDados].[Produtivo].[ENDERECO] 
                                                                WHERE HISTORICO_ENDERECOS_ID = " + e.CommandArgument.ToString());
                    Endereco_Id.Value = e.CommandArgument.ToString();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtBairro.Text = reader["BAIRRO"].ToString();
                            txtCID.Text = reader["CIDADE"].ToString();
                            txtUF.Text = reader["UF"].ToString();
                            txtCEP.Text = reader["CEP"].ToString();
                            txtEnd.Text = reader["ENDERECO"].ToString();
                        }
                    }

                    else
                    {
                        MSSqlConection sqlMS2 = new MSSqlConection();
                        SqlDataReader reader2 = sqlMS2.ReadData(@"select * from Produtivo.ENDERECO_NOVOS where ENDERECO_NOVOS_ID = " + e.CommandArgument.ToString());
                        Endereco_Id.Value = e.CommandArgument.ToString();

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                txtBairro.Text = reader2["BAIRRO"].ToString();
                                txtCID.Text = reader2["CIDADE"].ToString();
                                txtUF.Text = reader2["UF"].ToString();
                                txtCEP.Text = reader2["CEP"].ToString();
                                txtEnd.Text = reader2["ENDERECO"].ToString();
                            }
                        }

                        sqlMS2.MSSqlConectionClose();
                        reader2.Close();
                        reader.Close();
                    }

                    BtnSalEnd.Text = "Gravar";

                    sqlMS.MSSqlConectionClose();


                    break;





                case "excluir":

                    Hashtable htParametros = new Hashtable();
                    var id_usuario = "0";

                    if (Session["parametros"] != null)
                    {
                        htParametros = (Hashtable)Session["parametros"];
                        id_usuario = htParametros["id_usuario"].ToString();
                    }

                    MSSqlConection sqlMS3 = new MSSqlConection();


                    SqlDataReader reader3 = sqlMS3.ReadData(@"SELECT HISTORICO_ENDERECOS_ID, ENDERECO, BAIRRO, CIDADE, UF, right('00000000' + CEP, 8) as CEP
                                                                FROM [TopDados].[Produtivo].[ENDERECO] 
                                                                WHERE HISTORICO_ENDERECOS_ID = " + e.CommandArgument.ToString());

                    Endereco_Id.Value = e.CommandArgument.ToString();



                    if (reader3.HasRows)
                    {
                        sqlMS3.ExecuteNonQuery(@"insert into Produtivo.ENDERECO_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from Produtivo.ENDERECO
                                                 where HISTORICO_ENDERECOS_ID = " + e.CommandArgument.ToString());

                        sqlMS3.ExecuteNonQuery(@"delete from Produtivo.ENDERECO where HISTORICO_ENDERECOS_ID = " + e.CommandArgument.ToString());
                    }
                    else
                    {

                        sqlMS3.ExecuteNonQuery(@"insert into Produtivo.ENDERECO_NOVOS_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from Produtivo.ENDERECO_NOVOS
                                                 where ENDERECO_NOVOS_ID = " + e.CommandArgument.ToString());

                        sqlMS3.ExecuteNonQuery(@"delete from Produtivo.ENDERECO_NOVOS where ENDERECO_NOVOS_ID = " + e.CommandArgument.ToString());
                    }
                    LbExcl.Visible = true;
                    LbExcl.Text = "Endereço Excluido";

                    CarregarEndereco();

                    sqlMS3.MSSqlConectionClose();

                    break;
            }
        }

        protected void BtnSalEnd_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var ds_usuario = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                ds_usuario = htParametros["ds_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID,DS_ENDERECO) 
                                        select '" + id_usuario + "', getdate(), 'Atualização Endereço', '" + txtcontatoid.Value + "','"+txtEnd.Text+"'");


            MSSqlConection sqlMS2 = new MSSqlConection();

            SqlDataReader reader2 = sqlMS2.ReadData(@"SELECT HISTORICO_ENDERECOS_ID FROM [TopDados].[Produtivo].[ENDERECO] WHERE HISTORICO_ENDERECOS_ID =  '" + Endereco_Id.Value + "'");

            SqlDataReader reader = sqlMS.ReadData(@"SELECT HISTORICO_ENDERECOS_ID FROM [TopDados].[Produtivo].[ENDERECO] WHERE HISTORICO_ENDERECOS_ID =  '" + Endereco_Id.Value + "'");

            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.ENDERECO_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'A'
                                                 from Produtivo.ENDERECO
                                                 where HISTORICO_ENDERECOS_ID = '" + Endereco_Id.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE [TopDados].[Produtivo].[ENDERECO] 
                                        SET
                                        ENDERECO = '" + txtEnd.Text + "', BAIRRO = '" + txtBairro.Text + "' , CIDADE = '" + txtCID.Text + "', UF = '" + txtUF.Text + "', CEP = '" + txtCEP.Text + @"'
                                        where HISTORICO_ENDERECOS_ID = '" + Endereco_Id.Value + "'");

            }

            else if (reader2.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.ENDERECO_NOVOS_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'A'
                                                 from Produtivo.ENDERECO_NOVOS
                                                 where ENDERECO_NOVOS_ID = '" + Endereco_Id.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE [TopDados].[Produtivo].[ENDERECO_NOVOS] 
                                        SET
                                        ENDERECO = '" + txtEnd.Text + "', BAIRRO = '" + txtBairro.Text + "' , CIDADE = '" + txtCID.Text + "', UF = '" + txtUF.Text + "',ORIGEM_DADO_TOPDADOS = '" + ds_usuario + "',CEP = '" + txtCEP.Text + @"'
                                        where ENDERECO_NOVOS_ID = '" + Endereco_Id.Value + "'");
            }

            else
            {
                sqlMS.ExecuteNonQuery(@"insert into Topdados.Produtivo.ENDERECO_NOVOS
                                    (ENDERECO_NOVOS_ID, CONTATOS_ID, ENDERECO, BAIRRO, CEP, CIDADE, UF, ORIGEM_DADO_TOPDADOS, DT_INCLUSAO)
                                    VALUES  
                                    ((SELECT MAX(ENDERECO_NOVOS_ID)+1 from Topdados.Produtivo.ENDERECO_NOVOS ),'" + txtcontatoid.Value + "','" + txtEnd.Text + "','" + txtBairro.Text + "','" + txtCEP.Text + "','" + txtCID.Text + "','" + txtUF.Text + "','" + ds_usuario + "',GETDATE())");
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            CarregarEndereco();


            LbExcl.Visible = true;
            LbExcl.Text = "Adicionado com Sucesso";
        }

        protected void GridTel_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            GridView grv1 = ((GridView)sender);
            grv1.SelectedIndex = -1;
            switch (e.CommandName)
            {
                case "editar":

                    MSSqlConection sqlMS = new MSSqlConection();

                    SqlDataReader reader = sqlMS.ReadData(@"SELECT HISTORICO_TELEFONES_ID,DDD,TELEFONE
                                                            FROM [TopDados].[Produtivo].[TELEFONE] 
                                                            WHERE CONTATOS_ID = '" + txtcontatoid.Value + "' AND HISTORICO_TELEFONES_ID = " + e.CommandArgument.ToString());
                    TELEFONE_ID.Value = e.CommandArgument.ToString();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            txtDDD.Text = reader["DDD"].ToString();
                            txtTelefone.Text = reader["TELEFONE"].ToString();

                        }
                    }

                    else
                    {
                        MSSqlConection sqlMS2 = new MSSqlConection();
                        SqlDataReader reader2 = sqlMS2.ReadData(@"SELECT TELEFONE_NOVOS_ID,DDD,TELEFONE FROM [TopDados].[Produtivo].[TELEFONE_NOVOS] WHERE CONTATOS_ID = '" + txtcontatoid.Value + "' AND TELEFONE_NOVOS_ID = " + e.CommandArgument.ToString());
                        TELEFONE_ID.Value = e.CommandArgument.ToString();

                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                txtDDD.Text = reader["DDD"].ToString();
                                txtTelefone.Text = reader["TELEFONE"].ToString();

                            }
                        }

                        sqlMS2.MSSqlConectionClose();
                        reader2.Close();
                        reader.Close();
                    }

                    BtnSalTelefone.Text = "Gravar";

                    sqlMS.MSSqlConectionClose();


                    break;





                case "excluir":

                    Hashtable htParametros = new Hashtable();
                    var id_usuario = "0";

                    if (Session["parametros"] != null)
                    {
                        htParametros = (Hashtable)Session["parametros"];
                        id_usuario = htParametros["id_usuario"].ToString();
                    }

                    MSSqlConection sqlMS3 = new MSSqlConection();


                    SqlDataReader reader3 = sqlMS3.ReadData(@"SELECT HISTORICO_TELEFONES_ID,DDD,TELEFONE
                                                            FROM [TopDados].[Produtivo].[TELEFONE] 
                                                            WHERE CONTATOS_ID = '" + txtcontatoid.Value + "' AND HISTORICO_TELEFONES_ID = " + e.CommandArgument.ToString());

                    TELEFONE_ID.Value = e.CommandArgument.ToString();



                    if (reader3.HasRows)
                    {
                        sqlMS3.ExecuteNonQuery(@"insert into Produtivo.TELEFONE_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from Produtivo.TELEFONE 
                                                 where CONTATOS_ID = '" + txtcontatoid.Value + "' AND HISTORICO_TELEFONES_ID = " + e.CommandArgument.ToString());

                        sqlMS3.ExecuteNonQuery(@"delete from Produtivo.TELEFONE where CONTATOS_ID = '" + txtcontatoid.Value + "' AND HISTORICO_TELEFONES_ID = " + e.CommandArgument.ToString());
                        LbTel.Visible = true;
                        LbTel.Text = "Telefone Excluido";

                        CarregarTelefone();
                    }
                    else
                    {

                        sqlMS3.ExecuteNonQuery(@"insert into Produtivo.TELEFONE_NOVOS_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from Produtivo.TELEFONE_NOVOS
                                                 where CONTATOS_ID = '" + txtcontatoid.Value + "' AND TELEFONE_NOVOS_ID = " + e.CommandArgument.ToString());

                        sqlMS3.ExecuteNonQuery(@"delete from Produtivo.TELEFONE_NOVOS where CONTATOS_ID = '" + txtcontatoid.Value + "' AND  TELEFONE_NOVOS_ID = " + e.CommandArgument.ToString());

                        LbTel.Visible = true;
                        LbTel.Text = "Telefone Excluido";

                        CarregarTelefone();

                    }



                    sqlMS3.MSSqlConectionClose();

                    break;
            }
        }

        protected void BtnSalTelefone_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var ds_usuario = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                ds_usuario = htParametros["ds_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID,NU_TELEFONE) 
                                        select '" + id_usuario + "', getdate(), 'Atualização/Adicionar Telefone', '" + txtcontatoid.Value + "','" + txtDDD.Text + "'+'" + txtTelefone.Text + "'");


            MSSqlConection sqlMS2 = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT * FROM [TopDados].[Produtivo].[TELEFONE] WHERE HISTORICO_TELEFONES_ID = '" + TELEFONE_ID.Value + "' AND CONTATOS_ID = '" + txtcontatoid.Value + "' ");

            SqlDataReader reader2 = sqlMS2.ReadData(@"SELECT * FROM [TopDados].[Produtivo].[TELEFONE_NOVOS] WHERE TELEFONE_NOVOS_ID = '" + TELEFONE_ID.Value + "' AND CONTATOS_ID = '" + txtcontatoid.Value + "' ");

            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.TELEFONE_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'A'
                                                 from Produtivo.TELEFONE
                                                 where CONTATOS_ID = '" + txtcontatoid.Value + "'  AND HISTORICO_TELEFONES_ID = '" + TELEFONE_ID.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE [TopDados].[Produtivo].[TELEFONE] 
                                        SET
                                        DDD = '" + txtDDD.Text + "', TELEFONE = '" + txtTelefone.Text + @"' 
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "' AND HISTORICO_TELEFONES_ID = '" + TELEFONE_ID.Value + "'");
                LbTel.Visible = true;
                LbTel.Text = "Telefone Atualizado com Sucesso";

            }

            else if (reader2.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.TELEFONE_NOVOS_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'A'
                                                 from Produtivo.TELEFONE_NOVOS
                                                 where CONTATOS_ID = '" + txtcontatoid.Value + "'  AND TELEFONE_NOVOS_ID = '" + TELEFONE_ID.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE [TopDados].[Produtivo].[TELEFONE_NOVOS]
                                        SET
                                        DDD = '" + txtDDD.Text + "', TELEFONE = '" + txtTelefone.Text + @"' 
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "' AND TELEFONE_NOVOS_ID = '" + TELEFONE_ID.Value + "'");
                LbTel.Visible = true;
                LbTel.Text = "Telefone Atualizado com Sucesso";

            }

            else
            {
                sqlMS.ExecuteNonQuery(@"insert into Topdados.Produtivo.TELEFONE_NOVOS
                                        (TELEFONE_NOVOS_ID,CONTATOS_ID ,DDD ,TELEFONE ,ORIGEM_DADO_TOPDADOS ,DT_INCLUSAO)
                                        VALUES
                                        ((SELECT MAX(TELEFONE_NOVOS_ID)+1 from Topdados.Produtivo.TELEFONE_NOVOS),'" + txtcontatoid.Value + "','" + txtDDD.Text + "','" + txtTelefone.Text + "','" + ds_usuario + "',GETDATE())");
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            CarregarTelefone();


            LbTel.Visible = true;
            LbTel.Text = "Adicionado com Sucesso";
        }

        protected void GridEmail_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            GridView grv = ((GridView)sender);
            grv.SelectedIndex = -1;
            switch (e.CommandName)
            {
                case "editar":

                    
                    email_id.Value = e.CommandArgument.ToString();

                    txtemail.Text = e.CommandArgument.ToString();


                    break;

                    case "excluir":

                    Hashtable htParametros = new Hashtable();
                    var id_usuario = "0";

                    if (Session["parametros"] != null)
                    {
                        htParametros = (Hashtable)Session["parametros"];
                        id_usuario = htParametros["id_usuario"].ToString();
                    }

                    MSSqlConection sqlMS3 = new MSSqlConection();


                    SqlDataReader reader3 = sqlMS3.ReadData(@"select * FROM [TopDados].[Produtivo].[EMAIL] a
                                                             where EMAIL ='"+ e.CommandArgument.ToString()+ @"' AND CONTATOS_ID = '" + txtcontatoid.Value+"'");

                    email_id.Value = e.CommandArgument.ToString();



                    if (reader3.HasRows)
                    {
                        sqlMS3.ExecuteNonQuery(@"insert into Produtivo.EMAIL_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from Produtivo.EMAIL 
                                                 where EMAIL = '" + e.CommandArgument.ToString() +"' AND CONTATOS_ID = '" + txtcontatoid.Value + "'" );

                        sqlMS3.ExecuteNonQuery(@"delete from Produtivo.EMAIL where CONTATOS_ID = '" + txtcontatoid.Value + "' AND EMAIL = '" + e.CommandArgument.ToString()+"'");
                        LbEmail.Visible = true;
                        LbEmail.Text = "Email Excluido";

                        CarregarEmail();
                    }
                    else
                    {

                        sqlMS3.ExecuteNonQuery(@"insert into dbo.EMAIL_HISTORICO_ALTERACAO
                                                 select *, getdate(), " + id_usuario + @", 'E'
                                                 from dbo.EMAIL
                                                 where CONTATOS_ID = '" + txtcontatoid.Value + "' AND TELEFONE_NOVOS_ID = '"+ e.CommandArgument.ToString()+"'");

                        sqlMS3.ExecuteNonQuery(@"delete from dbo.EMAIL where CONTATOS_ID = '" + txtcontatoid.Value + "' AND EMAIL = '" + e.CommandArgument.ToString() + "'");

                        LbEmail.Visible = true;
                        LbEmail.Text = "Email Excluido";

                        CarregarEmail();

                    }



                    sqlMS3.MSSqlConectionClose();

                    break;
            }
        }

        protected void BtnEmail_Click(object sender, EventArgs e)
        {
            Hashtable htParametros = new Hashtable();
            var id_usuario = "0";
            var ds_usuario = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                id_usuario = htParametros["id_usuario"].ToString();
                ds_usuario = htParametros["ds_usuario"].ToString();
            }

            MSSqlConection sqlMS = new MSSqlConection();

            sqlMS.ExecuteNonQuery(@"insert into Produtivo.USUARIO_LOG (ID_USUARIO, DT_ATIVIDADE, DS_ATIVIDADE, CONTATOS_ID,DS_EMAIL) 
                                        select '" + id_usuario + "', getdate(), 'Atualização/Adicionar Email', '" + txtcontatoid.Value + "','"+txtemail.Text+"'");


            MSSqlConection sqlMS2 = new MSSqlConection();

            SqlDataReader reader = sqlMS.ReadData(@"SELECT * FROM [TopDados].[Produtivo].[EMAIL] WHERE EMAIL = '" + email_id.Value + "' AND CONTATOS_ID = '" + txtcontatoid.Value + "' ");

            SqlDataReader reader2 = sqlMS2.ReadData(@"SELECT * FROM [TopDados].[dbo].[EMAIL] WHERE EMAIL = '" + email_id.Value + "' AND CONTATOS_ID = '" + txtcontatoid.Value + "' ");

            if (reader.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into Produtivo.EMAIL_HISTORICO_ALTERACAO
                                        select *, getdate(), " + id_usuario + @", 'A'
                                        from Produtivo.EMAIL
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "'  AND EMAIL = '" + email_id.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE Produtivo.EMAIL
                                        SET
                                        EMAIL = '" + txtemail.Text + @"'
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "' AND EMAIL = '" + email_id.Value + "'");
                LbEmail.Visible = true;
                LbEmail.Text = "Email Atualizado com Sucesso";

            }

            else if (reader2.HasRows)
            {
                sqlMS.ExecuteNonQuery(@"insert into dbo.EMAIL_HISTORICO_ALTERACAO
                                        select *, getdate(), " + id_usuario + @", 'A'
                                        from dbo.EMAIL
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "'  AND EMAIL = '" + email_id.Value + "'");


                sqlMS.ExecuteNonQuery(@"UPDATE dbo.EMAIL
                                        SET
                                        EMAIL = '" + txtemail.Text + @"'
                                        where CONTATOS_ID = '" + txtcontatoid.Value + "' AND EMAIL = '" + email_id.Value + "'");
                LbEmail.Visible = true;
                LbEmail.Text = "Email Atualizado com Sucesso";

            }

            else
            {
                sqlMS.ExecuteNonQuery(@"INSERT INTO [TopDados].[Produtivo].[EMAIL]
                                        ([CONTATOS_ID],[EMAIL],[DT_INCLUSAO],[ORIGEM_DADO_TOPDADOS])
                                        VALUES ('"+txtcontatoid.Value+"','"+txtemail.Text+"',GETDATE(),'"+ds_usuario+"')");
            }

            reader.Close();
            sqlMS.MSSqlConectionClose();
            CarregarEmail();


            LbEmail.Visible = true;
            LbEmail.Text = "Adicionado com Sucesso";
        }
        
        protected void LimparCampos()
        {
            txtNome.Text = "";
            txtDtNasc.Text = "";
            txtSx.Text = "";
            ListOcup.SelectedValue = "0";
            txtdtsitcad.Text = "0";
            txtDtOb.Text = "01/01/1900";
            txtNMAe.Text = "";
            txtNPai.Text="";
            LsEscol.SelectedValue = "";
            TxtPIS.Text = "";
            txtCNS.Text = "";
            txtRG.Text = "";
            RgDtEmi.Text = "";
            txtTEleitor.Text = "";
            txtCNH.Text = "";
            txtDtHab.Text = "";
            txtCodSeg.Text = "";
            txtformren.Text = "";
            txtFormCNH.Text = "";
            txtNpgu.Text = "";
            txtCatAt.Text = "";
            txtSitCNH.Text = "";
            txtDtUltEmi.Text = "";
            txtDtVal.Text = "";
            txtLocalEmis.Text = "";
            GridEnd.DataSource = "";
            GridEnd.DataBind();
            GridTel.DataSource = "";
            GridTel.DataBind();
            GridEmail.DataSource = "";
            GridEmail.DataBind();
            
        
        }
    }
}
