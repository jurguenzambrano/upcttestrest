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
                ns.EnviarCorreo(usuarioACrear.Mail,"Confirma tu cuenta","Ingresa al siguiente enlace para confirmar tu cuenta");
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
