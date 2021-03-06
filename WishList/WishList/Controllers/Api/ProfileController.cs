﻿using SecretSanta.Data.Models;
using SecretSanta.Services.AuthorizeService;
using SecretSanta.Services.FeedbackService;
using SecretSanta.Services.UserServices;
using SecretSanta.Services.WishService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WishList.ViewModel;

namespace WishList.Controllers.Api
{
    public class ProfileController : ApiController
    {
        public ProfileController(IWishService wishService, IUserService userService, IFeedbackService feedbackService, IAuthorizeService authorizeService)
        {
            _wishService = wishService;
            _userService = userService;
            _feedbackService = feedbackService;
            _authorizeService = authorizeService;
        }

        #region Get
        [HttpGet, Route("api/MakeMagic")]
        public IHttpActionResult MakeMagic()
        {
            string email = User.Identity.Name;
            var user = _userService.GetCurrentUser(email);
            if(user.IsAdmin)
            {
                _userService.SetRecipient();
                return Ok("Success");
            }
            return Ok("Error");
        }
        #endregion

        #region Post
        [HttpPost, Route("api/ChangePassword")]
        public IHttpActionResult ChangePassword([FromBody]ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string email = User.Identity.Name;
                var user = _userService.GetCurrentUser(email);
                if (_authorizeService.ChangePassword(user, model.OldPassword, model.NewPassword))
                {
                    return Ok("Success");
                }
            }
            return Ok("Password mismatch");
        }

        [HttpPost, Route("api/ChangeAvatar")]
        public IHttpActionResult ChangeAvatar()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["file"];
            var filePath = HttpContext.Current.Server.MapPath("~/Files/" + postedFile.FileName);
            postedFile.SaveAs(filePath);
            string email = User.Identity.Name;
            var user = _userService.GetCurrentUser(email);
            user.PathToPicture = postedFile.FileName;
            _userService.UpdateUser(user);

            return Ok(postedFile.FileName);
        }

        [HttpPost, Route("api/ChangeProfileDetails")]
        public IHttpActionResult ChangeProfileDetails([FromBody]User user)
        {
            string email = User.Identity.Name;
            var _user = _userService.GetCurrentUser(email);
            if (user != null)
            {
                _user.FirstName = user.FirstName;
                _user.LastName = user.LastName;
                _user.Address = user.Address;
                _user.DateOfBirth = Convert.ToDateTime(user.DateOfBirth);
                _userService.UpdateUser(_user);
            }
            return Ok(true);
        }
        #endregion

        #region Private
        private IUserService _userService;
        private IWishService _wishService;
        private IFeedbackService _feedbackService;
        private IAuthorizeService _authorizeService;
        #endregion
    }
}
