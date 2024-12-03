using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Painel.Lib
{
    [Authorize(AuthenticationSchemes = "auth")]
    public class EnviarWhatsapp : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public static async Task EnviarWhatsappCliente(string numero,string mensagem)
        {
            try
            {   
                numero = numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Substring(0, 2) +
                         numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Substring(3);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.blueticks.co/messages");
                var content = new StringContent("{\n    \"to\": \"+55" + numero + "\",\n    \"message\": \""+mensagem+ "\",\n    \"apiKey\": \"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\"\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
        [HttpPost]
        [AllowAnonymous]
        public static async Task EnviarWhatsappClienteAnexo(string numero,string mensagem, string anexo)
        {
            try
            {   
                numero = numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Substring(0, 2) +
                         numero.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Substring(3);
                var urlMensagem = "https://zaptelemensagem.com.br/audios/" + anexo;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.blueticks.co/messages");
                var content = new StringContent("{ \"to\": \"" + "+55" + numero + "\", \"message\": \"" +  mensagem + "\", \"apiKey\": \"XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX\", \"asset\": { \"url\": \"" + urlMensagem + "\", \"type\": \"image/jpg\" } }", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
    }
}