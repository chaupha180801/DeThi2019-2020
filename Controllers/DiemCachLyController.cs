using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DETHI20.Models;
using Microsoft.AspNetCore.Mvc;

namespace DETHI20.Controllers
{
    public class DiemCachLyController:Controller
    {
        public IActionResult ThemDiemCachLy()
        {
            return View();
        }
        [HttpPost]
        public string AddKCL(DiemCachLyModel diemcachly)
        {
            int count;
            DataContext context = HttpContext.RequestServices.GetService(typeof(DataContext)) as DataContext;
            count = context.sqlInsertDiemCachLy(diemcachly);
            if (count == 1)
            {
                return "Thêm thành công";
            }
            return "Thêm thất bại";
        }

        public IActionResult ListByDiemCachLy()
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(DETHI20.Models.DataContext)) as DataContext;
            return View(context.sqlListDiemCachLy());
        }
       
    }
}
