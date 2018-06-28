using MVC.Models;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCommon;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Page()
        {
            DataContext dc = new DataContext();
            var U = dc.Users.ToArray();
            return View();
        }
        [HttpPost]
        public ActionResult gg(int page)
        {
            DataContext dc = new DataContext();
            UsersDTOList list = new UsersDTOList();
            int Number = 2;
            list.UsersList = dc.Users.OrderByDescending(p => p.ID).ToList().Skip((page - 1) * Number).Take(Number).ToList().Select(p => new UsersDTO { ID = p.ID, Name = p.Name }).ToArray();
            list.PageSize = (int)Math.Ceiling((dc.Users.Count()) / 2.0);

            return Json(new AjaxResult { Data = list });
        }
        public ActionResult Calendar()
        {
            return View(); 
        }
        public ActionResult FWB()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult text(string txt)
        {
            DataContext dc = new DataContext();

            UserEntity U = new UserEntity();
            U.Name = txt;
            dc.Users.Add(U);
            dc.SaveChanges();

            return Json(new AjaxResult { Msg = "ok" });
        }
        public ActionResult upload(HttpPostedFileBase file)
        {
            //var path= file.

            if (Directory.Exists(Server.MapPath("~/Upload/images")) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(Server.MapPath("~/Upload/images"));
            }
            string paths = Server.MapPath("~/Upload/images");
            string postaddpath = DateTime.Now.ToString("yyyyMMddhhmmss");
            string path = paths + "/" + postaddpath + "." + file.FileName.Split('.')[1];
            file.SaveAs(path);

            return Json(new { errno = "0", Data = "../../Upload/images/" + postaddpath + "." + file.FileName.Split('.')[1] });
        }
        public class A
        {
            [Required]
            [Range(13, 18)]
            [Display(Name = "年龄")]
            public int Age { get; set; }
        }

        public ActionResult ggg(A p)
        {

            string aaa = "";
            foreach (var item in ModelState.Keys)
            {
                foreach (ModelError modelError in ModelState[item].Errors)
                {
                    aaa += modelError + ".";
                }


            }
            if (!ModelState.IsValid)
            {
                return Json(new AjaxResult { Msg = "no" });
            }

            return Json(new AjaxResult { Msg = "ok" });
        }
    }
}