using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UsuariosServices.Persistencia;

namespace UsuariosServices
{
    public class Usuarios : IUsuarios
    {
        private UsuarioDAO usuarioDao = new UsuarioDAO();
        public Usuario CrearUsuario(Usuario usuarioACrear)
        {
            if (usuarioDao.Obtener(usuarioACrear.Dni) == null)
            {
                NotificacionService ns = new NotificacionService();
                string mensajeConfirmacion = "Hola " + usuarioACrear.Nombres + " " + usuarioACrear.Apellidos;
                mensajeConfirmacion = mensajeConfirmacion + "<br/><br/>";
                mensajeConfirmacion = mensajeConfirmacion + "Confirma la creación de tu cuenta ingresando al <a href=\"www.google.com\" target=\"_blank\">siguiente enlace</a>.";
                mensajeConfirmacion = mensajeConfirmacion + "<br/><br/>MobiPay";
                ns.EnviarCorreo(usuarioACrear.Mail,"Confirma tu cuenta", mensajeConfirmacion);
                return usuarioDao.Crear(usuarioACrear);
            }
            else
            {
                throw new WebFaultException<string>("Número de DNI ya registrado",HttpStatusCode.InternalServerError );
            }
            
        }

        public Usuario ObtenerUsuario(string dni)
        {
            Usuario usuario = usuarioDao.Obtener(dni);
            if (usuario == null)
            {
                throw new WebFaultException<string>("Alumno no existe", HttpStatusCode.InternalServerError);
            }
            else
            {
                return usuario;
            }
        }

        public Usuario ModificarUsuario(Usuario usuarioAModificar)
        {
            if (usuarioDao.Obtener(usuarioAModificar.Dni) == null)
            {
                throw new WebFaultException<string>("Alumno no existe", HttpStatusCode.InternalServerError);
            }
            else
            {
                return usuarioDao.Modificar(usuarioAModificar);
            }
        }

        public void EliminarUsuario(string dni)
        {
            
            if (usuarioDao.Obtener(dni) == null)
            {
                throw new WebFaultException<string>("Alumno no existe", HttpStatusCode.InternalServerError);
            }
            else
            {
                usuarioDao.Eliminar(dni); 
            }
        }

        public List<Usuario> ListarUsuario()
        {
            return usuarioDao.Listar();
        }
    }
}
