/*
 * Author:
 * Will Czifro
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using LectioServer.App_Start;
using LectioService;
using LectioService.Entities;
using LectioServer.Models;

namespace LectioServer.Controllers.Api.V1
{
    [Authorize]
    [RoutePrefix("api/v1/accounts")]
    public class AccountsController : ApiController
    {
        private const string UsersContainer = "users";
        private readonly LectioContext _context = new LectioContext();
        private ApplicationUserManager _userManager;
        private ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }

        public AccountsController()
        {
        }

        public AccountsController(ApplicationUserManager userManager,
        ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        private ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Username, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
            await UserManager.CreateAsync(user);
            var confirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //await UserManager.ConfirmEmailAsync(user.Id, confirmationToken);
            var callbackUrl = Url.Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
            callbackUrl += "/#/accounts/confirmemail?userid=" + user.Id + "&code=";
            callbackUrl += HttpUtility.UrlEncode(confirmationToken);

            string content;
            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/confirm-email-body.html");
            if (path == null) return InternalServerError();
            using (var reader = new StreamReader(path))
            {
                content = await reader.ReadToEndAsync();
            }
            content = content.Replace("{{USERNAME}}", user.FirstName + " " + user.LastName);
            content = content.Replace("{{callbackUrl}}", callbackUrl);
            await UserManager.SendEmailAsync(user.Id, "Lectio: Confirm Your Account", content);
            if (Request.Headers.Contains("Test"))
            {
                await UserManager.DeleteAsync(user); // delete for ease of testing
            }


            return Ok("Please confirm email.");

        }

        //
        // POST: /Account/Confirmation
        [HttpPost]
        [AllowAnonymous]
        [Route("confirmation")]
        public async Task<IHttpActionResult> Confirmation(ConfirmationModel model)
        {
            var confirmationtoken = HttpUtility.UrlDecode(model.ConfirmationToken);
            await UserManager.ConfirmEmailAsync(model.UserId, confirmationtoken);
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
                return InternalServerError(new Exception("User not found"));

            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);

            if (result.Succeeded)
            {
                return Ok("Please login.");
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RequestPasswordResetToken")]
        public async Task<IHttpActionResult> RequestPasswordResetToken(RequestResetModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                return InternalServerError(new Exception("User not found"));

            var resetToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = Url.Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
            callbackUrl += "/#/accounts/resetpassword?userid=" + user.Id + "&code=";
            callbackUrl += HttpUtility.UrlEncode(resetToken);

            string content;
            var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/reset-password-body.html");
            if (path == null) return InternalServerError(); //todo: need to add this file
            using (var reader = new StreamReader(path))
            {
                content = await reader.ReadToEndAsync();
            }
            content = content.Replace("{{USERNAME}}", user.FirstName + " " + user.LastName);
            content = content.Replace("{{callbackUrl}}", callbackUrl);
            await UserManager.SendEmailAsync(user.Id, "Lectio: Confirm Your Account", content);

            return Ok("Please check your email");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);
            if (user == null)
                return InternalServerError(new Exception("User not found"));
            var resetToken = HttpUtility.UrlDecode(model.ResetToken);

            var result = await UserManager.ResetPasswordAsync(user.Id, resetToken, model.Password);

            if (result.Succeeded)
            {
                return Ok("Please login.");
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return BadRequest(ModelState);
        }

            //
        // POST: /Accounts/ProfileImage
        [HttpPost]
        [Route("profileimage", Name = "profileimage")]
        [Authorize]
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> ProfileImage()
        {
            throw new NotImplementedException();
            //var user = await UserManager.FindByNameAsync(User.Identity.Name);
            //if (user == null)
            //    return InternalServerError(new Exception("User does not exist."));

            //var httpRequest = HttpContext.Current.Request;
            //if (httpRequest.Files.Count <= 0)
            //{
            //    return BadRequest("No file");
            //}

            //var file = new HttpPostedFileWrapper(httpRequest.Files[0]);

            ////try
            ////{
            ////    var amazonService = new AmazonService();
            ////
            ////}
            ////catch (WebException)
            ////{
            ////    return InternalServerError(new Exception("Unable to upload file"));
            ////}
            //var result = await UserManager.UpdateAsync(user);
            //return Ok(user);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TestGetAllAccounts")]
        public IHttpActionResult TestGetAllAccounts()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("TestEndpoint")]
        public IHttpActionResult TestEndpoint()
        {
            var url = Url.Request.RequestUri.Scheme + "://" + Request.RequestUri.Authority;
            return Ok(url);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("facebooklogin")]
        //public async Task<IHttpActionResult> FacebookLogin(FacebookLoginModel model)
        //{
        //    var facebook = new FacebookClient(model.Token);
        //    dynamic me;
        //    try
        //    {
        //        me = facebook.Get("me");

        //    }
        //    catch (FacebookOAuthException)
        //    {
        //        return BadRequest("invalid token");
        //    }
        //    var externalLogin = new UserLoginInfo("Facebook", me.id);

        //    var username = model.UserName ?? me.name;
        //    var email = model.Email ?? me.email;
        //    var firstName = me.first_name ?? "";
        //    var lastName = me.last_name ?? "";

        //    return await ExternalLoginOrRegistration(facebook, externalLogin, firstName, lastName, username, email);
        //}

        //private async Task<IHttpActionResult> ExternalLoginOrRegistration(FacebookClient fbClient, UserLoginInfo loginInfo, string firstName, string lastName, string username, string email)
        //{
        //    var newUser = false;
        //    var user = await UserManager.FindAsync(loginInfo);

        //    if (user == null)
        //    {
        //        newUser = true;
        //        var userByName = await UserManager.FindByNameAsync(username);
        //        if (userByName != null)
        //        {
        //            var modelState = new ModelStateDictionary();
        //            modelState.AddModelError("errorMessage", "Username is already taken");
        //            modelState.AddModelError("username", username);
        //            return BadRequest(modelState);
        //        }

        //        user = new ApplicationUser
        //        {
        //            UserName = username,
        //            FirstName = firstName,
        //            LastName = lastName
        //        };

        //        if (email != null)
        //        {
        //            var userByEmail = await UserManager.FindByEmailAsync(email);
        //            if (userByEmail != null) return BadRequest("Email is already taken");
        //            user.Email = email;
        //        }

        //        var createUserResult = await UserManager.CreateAsync(user);
        //        if (!createUserResult.Succeeded)
        //        {
        //            return InternalServerError(new Exception("User not created"));
        //        }

        //        var addLoginResult = await UserManager.AddLoginAsync(user.Id, loginInfo);
        //        if (!addLoginResult.Succeeded)
        //        {
        //            return InternalServerError(new Exception("Add login failed"));
        //        }
        //    }

        //    var identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
        //    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName, null, loginInfo.LoginProvider));
        //    if (email != null) identity.AddClaim(new Claim(ClaimTypes.Email, user.Email, null, loginInfo.LoginProvider));
        //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id, null, loginInfo.LoginProvider));

        //    //var authProp = ApplicationOAuthProvider.CreateProperties(user);
        //    //var ticket = new AuthenticationTicket(identity, authProp);
        //    var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
        //    var currentUtc = new SystemClock().UtcNow;
        //    ticket.Properties.IssuedUtc = currentUtc;
        //    var tokenExpirationTimeSpan = TimeSpan.FromDays(14);
        //    ticket.Properties.ExpiresUtc = currentUtc.Add(tokenExpirationTimeSpan);
        //    var accesstoken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
        //    //AuthenticationManager.SignIn(identity);

        //    var response = new JObject(
        //        new JProperty("access_token", accesstoken),
        //        new JProperty("token_type", "bearer"),
        //        new JProperty("expires_in", tokenExpirationTimeSpan.TotalSeconds.ToString(CultureInfo.InvariantCulture)),
        //        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
        //        new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()),
        //        new JProperty("UserName", user.UserName ?? ""),
        //        new JProperty("FirstName", user.FirstName ?? ""),
        //        new JProperty("LastName", user.LastName ?? ""),
        //        new JProperty("Email", user.Email ?? ""),
        //        new JProperty("ProfileImageUrl", user.PhotoUrl ?? ""),
        //        new JProperty("Id", user.Id ?? "")
        //        );


        //    return Ok(response);
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("TestFacebook")]
        //public async Task<IHttpActionResult> TestFacebook(string code)
        //{
        //    var facebook = new FacebookClient(code);
        //    dynamic me;
        //    try
        //    {
        //        me = facebook.Get("me");
        //        var start = DateTime.Now;
        //        var data = await _facebookService.GetFacebookShares(me.id, facebook);
        //        var end = DateTime.Now;
        //        var timelapse = end - start;
        //        var response = new Dictionary<string, object>();
        //        response.Add("time_taken", timelapse);
        //        response.Add("facebook", data);
        //        return Ok(response);
        //        //_facebookService.PostToFacebook("", facebook, "", null);
        //        //return Ok();
        //    }
        //    catch (FacebookOAuthException)
        //    {
        //        return BadRequest("invalid token");
        //    }
        //    catch (Exception)
        //    {
        //        return InternalServerError();
        //    }
        //}

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return Request.GetOwinContext().Authentication;
        //    }
        //}

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        #endregion
    }
}
