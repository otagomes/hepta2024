using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Net.Http.Headers;
using Painel.Data;
using Painel.Models;
using System.Diagnostics;

namespace Painel.Controllers
{
    //[Authorize(AuthenticationSchemes = "auth")]
    public class HomeController : WShare
    {        
        private readonly ctxContext _context;
        
        public HomeController(ctxContext context)
        {
            _context = context;            
        }

        [HttpPost("/Diagnostico")]
        public JsonResult Diagnostico(string vDados)
        {
            decimal TaxaGama10 = 0;
            decimal TaxaEpsilon10 = 0;
            decimal ConsumoEnergiaEletrica = 0;
            string[] vDadosDiagnostico = vDados.Split(',');
            if (vDadosDiagnostico.Length > 0)
            {
                int sizeDadosDiagnostico = vDadosDiagnostico[0].Length;
                int[] zeros = new int[sizeDadosDiagnostico];
                int[] uns = new int[sizeDadosDiagnostico];
                string TaxaGamma2 = "";
                string TaxaEpsilon2 = "";

                // . calcular bits MAIS e MENOS incidentes em cada posição do número
                foreach (var numero in vDadosDiagnostico)
                {
                    for (int i = 0; i < sizeDadosDiagnostico; i++)
                    {
                        if (numero[i] == '0')
                            zeros[i]++;
                        else
                            uns[i]++;
                    }
                }

                // . formar valores das taxas (em binário)
                for (int i = 0; i < sizeDadosDiagnostico; i++)
                {
                    if (uns[i] > zeros[i])
                    {
                        TaxaGamma2 += "1";
                        TaxaEpsilon2 += "0";
                    }
                    else
                    {
                        TaxaGamma2 += "0";
                        TaxaEpsilon2 += "1";
                    }
                }

                // . converter taxas de binário para decimal
                TaxaGama10 = Convert.ToInt32(TaxaGamma2, 2);
                TaxaEpsilon10 = Convert.ToInt32(TaxaEpsilon2, 2);

                // . calcular consumo de energia
                ConsumoEnergiaEletrica = TaxaGama10 * TaxaEpsilon10;

                //. todo: devolver TaxaGama10 e TaxEpsilon10 também
                return Json(ConsumoEnergiaEletrica);
            }
            else
            {
                return Json("-1");
            }            
        }
                
        [AllowAnonymous]
        public ActionResult Index()
        {        
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}