using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UsuariosServices.Dominio;

namespace UsuariosServices
{
    [ServiceContract]
    public interface ICreacionUsuariosService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "usuarios/login", ResponseFormat = WebMessageFormat.Json)]
        Usuario LoginUsuario(Usuario usuario);
    }
}
