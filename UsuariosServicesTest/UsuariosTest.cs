using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace UsuariosServicesTest
{
    [TestClass]
    public class UsuariosTest
    {
        [TestMethod]
        public void test()
        {
            //insertarUsuario("10243091");
            
            obtenerUsuario("99999999");
            /*modificarUsuario("10243093");
            eliminarUsuario("10243093");
            modificarUsuario("10243093");
            eliminarUsuario("10243093");
            obtenerUsuario("10243093");
            */
        }

        public void insertarUsuario(string dni)
        {
            string usuario;
            byte[] data;
            HttpWebRequest req;
            StreamReader reader;
            string usuarioJson;
            HttpWebResponse res;
            JavaScriptSerializer js;

            // Prueba de creación de usuario 
            usuario = "{\"Apellidos\":\"ZAMBRANO\",\"Celular\":\"992330838\",\"Direccion\":\"su casa\",\"Dni\":\"" + dni +"\",\"Mail\":\"jurguenzambrano@gmail.com\",\"Nombres\":\"Jurguen\"}";
            data = Encoding.UTF8.GetBytes(usuario);
            req = (HttpWebRequest) WebRequest.Create("http://upc-test-rest.apphb.com/Usuarios.svc/Usuarios");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            try { 
                res = (HttpWebResponse) req.GetResponse();
                reader = new StreamReader(res.GetResponseStream());
                usuarioJson = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                Usuario usuarioCreado = js.Deserialize<Usuario>(usuarioJson);
                Assert.AreEqual(dni, usuarioCreado.Dni);
                Assert.AreEqual("ZAMBRANO", usuarioCreado.Apellidos);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Número de DNI ya registrado", mensaje);
            }
        }
        public void obtenerUsuario(string dni){
            HttpWebRequest req;
            StreamReader reader;
            string usuarioJson;
            HttpWebResponse res;
            JavaScriptSerializer js;

            // Obtener Usuario
            req = (HttpWebRequest)WebRequest.Create("http://upc-test-rest.apphb.com/Usuarios.svc/Usuarios/" + dni);
            req.Method = "GET";
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                reader = new StreamReader(res.GetResponseStream());
                usuarioJson = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                Usuario usuarioObtenido = js.Deserialize<Usuario>(usuarioJson);
                Assert.AreEqual(dni, usuarioObtenido.Dni);
                Assert.AreEqual("DE LOS PALOTES", usuarioObtenido.Apellidos);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Alumno no existe", mensaje);
            }
        }

        private void modificarUsuario(string dni){
            string usuario;
            byte[] data;
            HttpWebRequest req;
            StreamReader reader;
            string usuarioJson;
            HttpWebResponse res;
            JavaScriptSerializer js;

            // Modifica Usuario
            usuario = "{\"Apellidos\":\"ZAMBRANO MORENO\",\"Celular\":\"992330838\",\"Direccion\":\"su casa\",\"Dni\":\""+ dni +"\",\"Mail\":\"jurguenzambrano@gmail.com\",\"Nombres\":\"Jurguen\"}";
            data = Encoding.UTF8.GetBytes(usuario);
            req = (HttpWebRequest)WebRequest.Create("http://upc-test-rest.apphb.com/Usuarios.svc/Usuarios");
            req.Method = "PUT";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                reader = new StreamReader(res.GetResponseStream());
                usuarioJson = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                Usuario usuarioModificado = js.Deserialize<Usuario>(usuarioJson);
                Assert.AreEqual(dni, usuarioModificado.Dni);
                Assert.AreEqual("ZAMBRANO MORENO", usuarioModificado.Apellidos);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Alumno no existe", mensaje);
            }
        }

        private void eliminarUsuario(string dni){
            HttpWebRequest req;
            StreamReader reader;
            HttpWebResponse res;
            JavaScriptSerializer js;

            // Eliminar Usuario
            req = (HttpWebRequest)WebRequest.Create("http://upc-test-rest.apphb.com/Usuarios.svc/Usuarios/" + dni);
            req.Method = "DELETE";
            try {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Alumno no existe", mensaje);
            }

        }
    }
}
