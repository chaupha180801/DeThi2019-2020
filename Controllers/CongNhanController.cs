using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DETHI20.Models;
using Microsoft.AspNetCore.Mvc;

namespace DETHI20.Controllers
{
    public class CongNhanController:Controller
    {
        public IActionResult LietKeCongNhanTheoTrieuChung()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ListByTrieuChung(int sotrieuchung)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DETHI20.Models.DataContext)) as DataContext;
            return View(context.sqlListByTrieuChungCongNhan(sotrieuchung));
        }

        public IActionResult ListCongNhanByDCL(string madiemcachly)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DETHI20.Models.DataContext)) as DataContext;
            return View(context.sqlListByCongNhanDCL(madiemcachly));
        }

        public IActionResult Delete(string macn)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DETHI20.Models.DataContext)) as DataContext;
            return View(context.sqlDeteleCN(macn));
        }

        public IActionResult showCN(string macn)
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DETHI20.Models.DataContext)) as DataContext;
            return View(context.sqlGetCN(macn));
        }

    }
}
