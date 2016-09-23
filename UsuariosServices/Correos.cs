using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace UsuariosServices
{
    public class Correos
    {
        /*
         * Cliente SMTP
         * Gmail:  smtp.gmail.com  puerto:587
         * Hotmail: smtp.liva.com  puerto:25
         */
        //SmtpClient server = new SmtpClient("mailtrap.io", 2525);
        SmtpClient server = new SmtpClient("smtp.gmail.com", 25);

        public Correos()
        {
            //server.Credentials = new System.Net.NetworkCredential("315e8416980e6f", "6241a427f47452");
            server.Credentials = new System.Net.NetworkCredential("testupcperu@gmail.com", "test159upc");
            server.EnableSsl = true;
        }

        public void MandarCorreo(MailMessage mensaje)
        {
            server.Send(mensaje);
        }
    }
}