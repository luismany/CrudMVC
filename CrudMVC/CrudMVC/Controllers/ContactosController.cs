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
        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Agregar(Contacto agregarContacto)
        {
            SqlConnection con = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("sp_Agregar", con);
            cmd.Parameters.AddWithValue("Nombre",agregarContacto.Nombre);
            cmd.Parameters.AddWithValue("Apellido",agregarContacto.Apellido);
            cmd.Parameters.AddWithValue("Telefono",agregarContacto.Telefono);
            cmd.Parameters.AddWithValue("Correo",agregarContacto.Correo);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();


            return RedirectToAction("Inicio","Contactos");

        }
        [HttpGet]
        public ActionResult Modificar(int? id)
        {
            if (id == null)
                return RedirectToAction("Inicio", "Contactos");

            var contactoAModificar = (from contacto in listaContacto
                                           where contacto.IdContacto == id
                                           select contacto).FirstOrDefault();
            /*Contacto contactoAModificar = (from contacto in listaContacto
                                      where contacto.IdContacto == id
                                      select contacto).FirstOrDefault();*/

            return View(contactoAModificar);

        }
        [HttpPost]
        public ActionResult Modificar(Contacto contactoModificado)
        {
            SqlConnection con = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("sp_Editar",con);
            cmd.Parameters.AddWithValue("IdContacto", contactoModificado.IdContacto);
            cmd.Parameters.AddWithValue("Nombre", contactoModificado.Nombre);
            cmd.Parameters.AddWithValue("Apellido", contactoModificado.Apellido);
            cmd.Parameters.AddWithValue("Telefono",contactoModificado.Telefono);
            cmd.Parameters.AddWithValue("Correo",contactoModificado.Correo);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Inicio","Contactos");
        }

        [HttpGet]
        public ActionResult Eliminar(int? id)
        {

            if (id == null)
                return RedirectToAction("Inicio","Contactos");

            var contactoAEliminar = (from contacto in listaContacto
                                     where contacto.IdContacto == id
                                     select contacto).FirstOrDefault();

            return View(contactoAEliminar);
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            SqlConnection con = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand("sp_Eliminar", con);
            cmd.Parameters.AddWithValue("Idcontact", id);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Inicio", "Contactos");
        }

    }
}