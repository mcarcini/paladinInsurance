using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Paladin.Infrastructure
{
    public class HttpAuthenticate : FilterAttribute, IAuthenticationFilter
    {

        private string user { get; set; }
        private string password { get; set; }

        public HttpAuthenticate(string user, string password) {
            this.user = user;
            this.password = password;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var authHeaders = filterContext.HttpContext.Request.Headers["Authorization"];
            if (! String.IsNullOrEmpty(authHeaders)) {
                var credentials = ASCIIEncoding.ASCII.GetString(
                    Convert.FromBase64String(authHeaders.Replace("Basic",""))).Split(':');

                var userName = credentials[0];
                var password = credentials[1];

                if (this.user != userName || this.password != password) {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }
    }
}