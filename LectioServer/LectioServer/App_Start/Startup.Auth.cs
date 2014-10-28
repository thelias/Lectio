using System;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using LectioServer.App_Start;
using LectioServer.Providers;
using LectioService;
using LectioService.Entities;

namespace LectioServer
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and role manager to use a single instance per request
            app.CreatePerOwinContext(LectioContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/account/login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),
                    //                    OnApplyRedirect = ctx =>
                    //                      {
                    //                         if (!IsAjaxRequest(ctx.Request))
                    //                         {
                    //                            ctx.Response.Redirect(ctx.RedirectUri);
                    //                         }
                    //                      }
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //var tokenCorsPolicy = GenerateCorsPolicy();

            //app.UseCors(new CorsOptions
            //{
            //    PolicyProvider = new CorsPolicyProvider
            //    {
            //        PolicyResolver = request => Task.FromResult(GenerateCorsPolicy())
            //    }
            //});

            app.UseCors(CorsOptions.AllowAll);

            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                //ApplicationCanDisplayErrors = true,
                TokenEndpointPath = new PathString("/api/v1/accounts/login"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/v1/accounts/externallogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true,
            };
            app.UseOAuthBearerTokens(OAuthOptions);
            //app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalBearer);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "fFFOMXaafKnwdgCAF6wEIg",
            //   consumerSecret: "Lq9EfAmYd9yQ4p6MtNkHtT7EszvJPLb9hoNarSNxw");

            //app.UseFacebookAuthentication(
            //   appId: "731182676942650",
            //   appSecret: "278c2f686472a736b62dc98aa14808bd");

            //app.UseGooglePlusAuthentication("92532468789-cf9s5veefdu2o8td3lok9vbqb7esbcr2.apps.googleusercontent.com", "OO-qvsiESiCOne9H6dfcq9oa");
            //app.UseGoogleAuthentication();
        }

        private static CorsPolicy GenerateCorsPolicy()
        {
            var corsPolicy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true
            };

            corsPolicy.Origins.Add("http://lectioserver.azurewebsites.com");
            corsPolicy.Origins.Add("http://localhost:9000");
            corsPolicy.Origins.Add("http://localhost:9500");

            return corsPolicy;
        }

        //        private static bool IsAjaxRequest(IOwinRequest request)
        //        {
        //            IReadableStringCollection query = request.Query;
        //            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
        //            {
        //                return true;
        //            }
        //            IHeaderDictionary headers = request.Headers;
        //            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        //        }
    }
}