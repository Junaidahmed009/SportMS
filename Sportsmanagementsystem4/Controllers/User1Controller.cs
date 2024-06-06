using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sportsmanagementsystem4.Models;

namespace Sportsmanagementsystem4.Controllers
{
    public class User1Controller : ApiController
    {
        SportsManagementDBEntities db = new SportsManagementDBEntities();
        [HttpGet]
        public HttpResponseMessage Getuser()
        {
            try
            {
                var user = db.Users.OrderBy(b => b.id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, user);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }

    }
}