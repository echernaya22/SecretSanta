﻿using System.Web;
using System.Web.Mvc;
using SecretSanta.Data.Models;
using SecretSanta.Services.UserServices;

namespace WishList.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        //[Authorize]
        public ActionResult Index()
        {
            #region Initialize
            _userService.CreateUser(new User
            {
                FirstName = "Lena",
                LastName = "Chernaya",
                Email = "9@mail.ru",
                Password = "panda_super_secret_password",
                PathToPicture = "path to picture",
                IsAdmin = false
            });
            _userService.CreateUser(new User
            {
                FirstName = "Lena",
                LastName = "Chernaya",
                Email = "99@mail.ru",
                Password = "panda_super_secret_password",
                PathToPicture = "path to picture",
                IsAdmin = false
            });
            _userService.CreateUser(new User
            {
                FirstName = "Lena",
                LastName = "Chernaya",
                Email = "88@mail.ru",
                Password = "panda_super_secret_password",
                PathToPicture = "path to picture",
                IsAdmin = false
            });
            _userService.CreateUser(new User
            {
                FirstName = "Lena",
                LastName = "Chernaya",
                Email = "77@mail.ru",
                Password = "panda_super_secret_password",
                PathToPicture = "path to picture",
                IsAdmin = false
            });
            //_userService.CreateUser(new User
            //{
            //    FirstName = "Lena",
            //    LastName = "Chernaya",
            //    Email = "66@mail.ru",
            //    Password = "panda_super_secret_password",
            //    PathToPicture = "path to picture",
            //    IsAdmin = false,
            //    GroupId = 1
            //});
            //_userService.CreateUser(new User
            //{
            //    FirstName = "Lena",
            //    LastName = "Chernaya",
            //    Email = "55@mail.ru",
            //    Password = "panda_super_secret_password",
            //    PathToPicture = "path to picture",
            //    IsAdmin = false,
            //    GroupId = 1
            //});
            #endregion
            ////var user = _userService.GetUser(1);
            //var group = _groupService.GetGroup(1);
            //_groupService.SetRecipient(group);
            //var user = _userService.GetUser(1);
            //var user = _userService.GetUser(1);
            // UserViewModel 
            ViewBag.User = user;
            return View();
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            _userService.CreateUser(new User
            {
                FirstName = "Lena",
                LastName = "Chernaya",
                Email = "9@mail.ru",
                Password = "panda_super_secret_password",
                PathToPicture = "~/Files/GeyYBmUpySU.jpg",
                IsAdmin = false
            });
            if (upload != null)
            {
                //var picture = new Picture
                //{
                //    Name = upload.FileName,
                //    Path = "~/Files/" + upload.FileName
                //};
                //using (SantaContext context = new SantaContext())
                //{
                //    context.Pictures.Add(picture);
                //    context.SaveChanges();
                //}
                //upload.SaveAs(Server.MapPath("~/Files/" + picture.Name));
            }
            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}