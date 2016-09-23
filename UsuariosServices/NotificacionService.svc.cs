using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UsuariosServices
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NotificacionService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NotificacionService.svc o NotificacionService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class NotificacionService : INotificacionService
    {

        public void EnviarCorreo(string destino, string asunto, string mensaje)
        {
            try
            {
                Correos Cr = new Correos();
                MailMessage mnsj = new MailMessage();

                mnsj.Subject = "Hola Mundo";
                mnsj.To.Add(new MailAddress(destino));
                mnsj.From = new MailAddress("accounts@mobipay.com", asunto);
                mnsj.Body = mensaje;
                mnsj.IsBodyHtml = true;
                /* Enviar */
                Cr.MandarCorreo(mnsj);
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        /*
        public static RestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = "https://api.mailgun.net/v3";
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-0747792f0fcb980b44c5842a86e7af60");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "sandboxd624b64941b5466abb1e5b07f516f69c.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxd624b64941b5466abb1e5b07f516f69c.mailgun.org>");
            request.AddParameter("to", "Jurguen Zambrano <jurguenzambrano@gmail.com>");
            request.AddParameter("subject", "Hello Jurguen Zambrano");
            request.AddParameter("text", "Congratulations Jurguen Zambrano, you just sent an email with Mailgun!  You are truly awesome!  You can see a record of this email in your logs: https://mailgun.com/cp/log .  You can send up to 300 emails/day from this sandbox server.  Next, you should add your own domain so you can send 10,000 emails/month for free.");
            request.Method = Method.POST;
            return client.Execute(request);
        }
        */
    }
}
