using Painel.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Painel.Controllers
{
    //[Authorize(AuthenticationSchemes = "auth")]
    public class WShare : Controller
    {
        #region PorpriedadesSessao
        public string MsgAviso
        {
            get
            {
                return HttpContext.Session.GetString("msgaviso");
            }
            set
            {
                HttpContext.Session.SetString("msgaviso", value);
            }
        }

        public string MsgErro
        {
            get
            {
                return HttpContext.Session.GetString("msgerro");
            }
            set
            {
                HttpContext.Session.SetString("msgerro", value);
            }
        }
        #endregion
    }
}
