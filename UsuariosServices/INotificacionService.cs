using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UsuariosServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "INotificacionService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface INotificacionService
    {
        [OperationContract]
        void EnviarCorreo(string destino, string asunto, string mensaje);
    }
}
