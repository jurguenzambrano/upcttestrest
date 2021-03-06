﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UsuariosServices.Persistencia;
using System.ServiceModel.Web;
using System.Net;

namespace UsuariosServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UsuariosService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UsuariosService.svc o UsuariosService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UsuariosService : IUsuariosService
    {
        private UsuarioDAO usuarioDao = new UsuarioDAO();

        public Usuario CrearUsuario(Usuario usuarioACrear)
        {
            if (usuarioDao.Obtener(usuarioACrear.Dni) != null)
            {
                throw new WebFaultException<string>("Número de DNI ya registrado", HttpStatusCode.InternalServerError);
            }

            if (usuarioDao.ObtenerPorEmail(usuarioACrear.Mail) != null)
            {
                throw new WebFaultException<string>("Correo electrónico ya registrado", HttpStatusCode.InternalServerError);
            }

            NotificacionService ns = new NotificacionService();
            string mensajeConfirmacion = "Hola " + usuarioACrear.Nombres + " " + usuarioACrear.Apellidos;
            mensajeConfirmacion = mensajeConfirmacion + "<br/><br/>";
            mensajeConfirmacion = mensajeConfirmacion + "Confirma la creación de tu cuenta ingresando al <a href=\"www.google.com\" target=\"_blank\">siguiente enlace</a>.";
            mensajeConfirmacion = mensajeConfirmacion + "<br/><br/>MobiPay";
            ns.EnviarCorreo(usuarioACrear.Mail, "Confirma tu cuenta", mensajeConfirmacion);
            return usuarioDao.Crear(usuarioACrear);
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
