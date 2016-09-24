using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace UsuariosServices
{
    [ServiceContract]
    public interface IUsuariosService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Usuarios", ResponseFormat = WebMessageFormat.Json)]
        Usuario CrearUsuario(Usuario usuarioACrear);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Usuarios/{dni}", ResponseFormat = WebMessageFormat.Json)]
        Usuario ObtenerUsuario(string dni);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "Usuarios", ResponseFormat = WebMessageFormat.Json)]
        Usuario ModificarUsuario(Usuario usuarioAModificar);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "Usuarios/{dni}", ResponseFormat = WebMessageFormat.Json)]
        void EliminarUsuario(string dni);

        [OperationContract]
        List<Usuario> ListarUsuario();
    }
}
