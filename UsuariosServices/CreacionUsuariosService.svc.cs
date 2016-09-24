using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UsuariosServices.Dominio;
using UsuariosServices.Persistencia;

namespace UsuariosServices
{
    public class CreacionUsuariosService : ICreacionUsuariosService
    {
        private UsuarioDAO usuarioDao = new UsuarioDAO();
        private Usuario usuario = new Usuario();

        public Usuario LoginUsuario(Usuario usuarioLogin)
        {
            usuario = usuarioDao.ObtenerPorEmail(usuarioLogin.Mail);

            if ( usuario == null)
            {
                throw new WebFaultException<string>("Correo Electrónico no registrado", HttpStatusCode.InternalServerError);
            }
            else if (usuario.Estado.Equals("0"))
            {
                throw new WebFaultException<string>("Usuario no confirmado", HttpStatusCode.InternalServerError);
            }
            else if (!usuario.Clave.Equals(usuarioLogin.Clave))
            {
                throw new WebFaultException<string>("Clave incorrecta", HttpStatusCode.InternalServerError);
            }
            else
            {
                return usuario;
            }
        }
    }
}
