using Sportsmanagementsystem4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sportsmanagementsystem4.Controllers
{
    public class UserController : ApiController
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

        [HttpPost]
        public HttpResponseMessage UserSignup([FromBody] User user)
        {
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User data is required.");
            }

            // Check for null or empty fields
            if (string.IsNullOrEmpty(user.name) || string.IsNullOrEmpty(user.registration_no) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.role))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "All parameters are required.");
            }

            // Additional validation checks (e.g., password length)
            if (user.password.Length < 6)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Password must be at least 6 characters long.");
            }

            try
            {
                // Add user to the database
                db.Users.Add(user);
                db.SaveChanges();

                // Return the user ID as the response
                return Request.CreateResponse(HttpStatusCode.OK, user.name);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage UserLogin(User user)
        {
            // Check if name and password are provided
            if (string.IsNullOrEmpty(user.name) || string.IsNullOrEmpty(user.password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Both name and password are required.");
            }

            try
            {
                // Find user in the database by name
                var userInDb = db.Users.FirstOrDefault(u => u.name == user.name);

                // Check if user exists and password matches
                if (userInDb != null && userInDb.password == user.password)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Login successful.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid name or password.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



        [HttpPost]
        public HttpResponseMessage DeleteUser(User user)
        {
            // Check if registration number and password are provided
            if (string.IsNullOrEmpty(user.registration_no) || string.IsNullOrEmpty(user.password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Both registration number and password are required.");
            }

            try
            {
                // Find user in the database by registration number
                var userInDb = db.Users.FirstOrDefault(u => u.registration_no == user.registration_no);

                // Check if user exists and password matches
                if (userInDb != null && userInDb.password == user.password)
                {
                    // Remove the user from the database
                    db.Users.Remove(userInDb);
                    db.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "User deleted successfully.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid registration number or password.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}