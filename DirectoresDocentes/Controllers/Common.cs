using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DirectoresDocentes.Context;
using DirectoresDocentes.Models.BachilleratoModels;
using Syncfusion.JavaScript.Models;

namespace Bachillerato.Controllers
{
    public class Common
    {
        private AppDbContext _db;                

        public string Md5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            // Cambios para poder gestionar las contraseñas con caracteres especiales
            // No usar ni utf-8 ni ascii encoding
            md5.ComputeHash(Encoding.Default.GetBytes(text));

            var result = md5.Hash;
            var strBuilder = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public string Encrypt(string clearText)
        {
            var EncryptionKey = "MAKV2SPBNI99212";
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            var EncryptionKey = "MAKV2SPBNI99212";
            var text = cipherText.Replace(" ", "+");
            var cipherBytes = Convert.FromBase64String(text);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public int PeriodoActual()
        {
            _db = new AppDbContext();
            var periodo =
                _db.Database.SqlQuery<GetPeriodo>(
                    "select max(CODIGO) as CODIGO from ACADEMICO.PERIODOS WHERE TIPO = 'R' AND ESTADO = 'A' AND NOMBRE LIKE to_char(sysdate, 'yyyy') || '%'");
            return periodo.First().CODIGO;
        }

        public List<int> Facultades(string persona)
        {
            _db = new AppDbContext();
            var facultades =
                _db.Database.SqlQuery<PERSONA_FACULTADES>(
                    "select Count(f.CODIGO) as Cantidad, f.CODIGO as Facultad from ACADEMICO.PERSONAS p, ACADEMICO.PROGRAMAS_ESTUDIOS g, ACADEMICO.FACULTADES_ESCUELAS f, ACADEMICO.PROGRAMAS_ESTUDIOS_PERSONAS e where p.CODIGO = e.PERSONA_RESPONSABLE and g.CODIGO = e.PROG_EDTUD_CODIGO and g.FACUL_ESCU_CODIGO = f.CODIGO and p.CODIGO = " +
                    persona + " and e.PERSONA_ROL in (2, 6, 7, 8, 9, 18, 22, 26, 32, 35, 40) group by (f.CODIGO)");
            var listFacultades = new List<int>();
            foreach (var f in facultades)
            {
                listFacultades.Add(f.FACULTAD);
            }
            return listFacultades;
        }

        public string RenderTemplate(string usuario, string expositor, string lugar, string fecha, string hora,
            string url)
        {
            var body = string.Empty;
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Email/plantilla.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{usuario}", usuario);
            body = body.Replace("{expositor}", expositor);
            body = body.Replace("{lugar}", lugar);
            body = body.Replace("{fecha}", fecha);
            body = body.Replace("{hora}", hora);
            body = body.Replace("{url}", url);
            return body;
        }

        public string RenderTemplatePdf(string usuario, string qr, string expositor, string lugar, string fecha,
            string hora, string url)
        {
            var body = string.Empty;
            using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Email/plantilla.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{usuario}", usuario);
            body = body.Replace("{qr}", qr);
            body = body.Replace("{expositor}", expositor);
            body = body.Replace("{lugar}", lugar);
            body = body.Replace("{fecha}", fecha);
            body = body.Replace("{hora}", hora);
            body = body.Replace("{url}", url);
            return body;
        }

        public string RenderTemplateSupervisor(string usuario, string alumno, string link)
        {
            var body = string.Empty;
            using (
                var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Email/plantilla-supervisor.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{usuario}", usuario);
            body = body.Replace("{alumno}", alumno);
            body = body.Replace("{link}", link);
            return body;
        }

        public string RenderTemplateResponsable(string usuario, string alumno, string link)
        {
            var body = string.Empty;
            using (
                var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Email/plantilla-responsable.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{usuario}", usuario);
            body = body.Replace("{alumno}", alumno);
            body = body.Replace("{link}", link);
            return body;
        }

//        public GridProperties ConvertGridObject(string gridProperty)
//        {
//            var serializer = new JavaScriptSerializer();
//            var div = (IEnumerable) serializer.Deserialize(gridProperty, typeof (IEnumerable));
//            var gridProp = new GridProperties();
//            foreach (KeyValuePair<string, object> ds in div)
//            {
//                var property = gridProp.GetType()
//                    .GetProperty(ds.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
//                if (property != null)
//                {
//                    var type = property.PropertyType;
//                    var serialize = serializer.Serialize(ds.Value);
//                    var value = serializer.Deserialize(serialize, type);
//                    property.SetValue(gridProp, value, null);
//                }
//            }
//            return gridProp;
//        }

//        public class JsonpResult : ActionResult
//        {
//            public JsonpResult(object data) : this(data, null)
//            {
//            }

//            public JsonpResult(object data, string callbackFunction)
//            {
//                Data = data;
//                CallbackFunction = callbackFunction;
//            }

//            public string CallbackFunction { get; set; }
//            public Encoding ContentEncoding { get; set; }
//            public string ContentType { get; set; }
//            public object Data { get; set; }

//            public override void ExecuteResult(ControllerContext context)
//            {
//                if (context == null) throw new ArgumentNullException("context");

//                var response = context.HttpContext.Response;

//                response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/x-javascript" : ContentType;

//                if (ContentEncoding != null) response.ContentEncoding = ContentEncoding;

//                if (Data != null)
//                {
//                    var request = context.HttpContext.Request;

//                    var callback = CallbackFunction ?? request.Params["callback"] ?? "callback";

//#pragma warning disable 0618 // JavaScriptSerializer is no longer obsolete
//                    var serializer = new JavaScriptSerializer();
//                    response.Write(string.Format("{0}({1});", callback, serializer.Serialize(Data)));
//#pragma warning restore 0618
//                }
//            }
//        }
    }
}