using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Topdados20
{
    public partial class PaginaPrincipal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            atualizaCPF.Visible = true;

            if (Session["parametros"] == null)
            {
                Response.Redirect("index.aspx");
            }

            Hashtable htParametros = new Hashtable();
            var ds_usuario = "";
            var id_perfil = "";

            if (Session["parametros"] != null)
            {
                htParametros = (Hashtable)Session["parametros"];
                ds_usuario = htParametros["ds_usuario"].ToString();
                id_perfil = htParametros["id_perfil"].ToString();
                Logado.Text = "Usuário: " + ds_usuario;
                

            }

            if (id_perfil != "1")
            {
                atualizaCPF.Visible = false;
            }

        }
    }
}