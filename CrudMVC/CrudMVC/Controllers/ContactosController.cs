using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using CrudMVC.Models;

namespace CrudMVC.Controllers
{
    public class ContactosController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contacto> listaContacto = new List<Contacto>();

        // GET: Contactos
        public ActionResult Inicio()
        {
            listaContacto = new List<Contacto>();

            SqlConnection con = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("select * from Contacto",con);
            cmd.CommandType = CommandType.Text;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Contacto nuevoContacto = new Contacto();

                nuevoContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                nuevoContacto.Nombre = dr["Nombre"].ToString();
                nuevoContacto.Apellido = dr["Apellido"].ToString();
                nuevoContacto.Telefono = dr["Telefono"].ToString();
                nuevoContacto.Correo = dr["Correo"].ToString();

                listaContacto.Add(nuevoContacto);
            }

            return View(listaContacto);
        }
    }
}