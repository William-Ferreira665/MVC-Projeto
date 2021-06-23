using System.Collections.Generic;
using E_Players_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Players_MVC.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        [TempData]
        public string Mensagem { get; set; }
        Jogador jogadorModel = new Jogador();
        public IActionResult Index()
        {
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            List<string> csv = jogadorModel.LerTodasLinhasCSV("Database/Jogador.csv");

            var logado =
            csv.Find(
                x =>
                x.Split(";")[3] == form["Email"] &&
                x.Split(";")[4] == form["Senha"]
            );


            if (logado != null)
            {
                HttpContext.Session.SetString("_UserName", logado.Split(";")[1]);
                return LocalRedirect("~/");
                Mensagem = "";
            }
   
                Mensagem = "Dados incorretos, tente novamente...";
                return LocalRedirect("~/Login");

        }
        
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}