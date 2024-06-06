using Sportsmanagementsystem4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sportsmanagementsystem4.Controllers
{
    public class TeamController : ApiController
    {

        // Crud Team and player table
        SportsManagementDBEntities db=new SportsManagementDBEntities();
     
        [HttpGet]//read team table data
        public HttpResponseMessage Getteam()
        {
            try
            {
                var teams = db.Teams.OrderBy(b => b.name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, teams);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
        [HttpPost]//create team table data
        public HttpResponseMessage Addteam(Team team)
        {
            try
            {
                db.Teams.Add(team);
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, team.id);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }

        }
        //update team table data
        [HttpPost]
        public HttpResponseMessage Updateteamdetail(Team team)
        {
            try
            {
                var orignal = db.Teams.Find(team.id);
                if (orignal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "team Not Found");
                }
                db.Entry(orignal).CurrentValues.SetValues(team);
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "team is Modified");


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }

        }
        //delete team table data
        [HttpGet]
        public HttpResponseMessage Deleteteam(int id)
        {
            try
            {
                var orignal = db.Teams.Find(id);
                if (orignal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "team Not Found");
                }
                db.Entry(orignal).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "team is Deleted");


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }


        /*-------------------------------------------------------------------*/

        [HttpGet]//read Player table data
        public HttpResponseMessage GetPlayer()
        {
            try
            {
                var players = db.Players.OrderBy(b => b.name).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, players);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
        [HttpPost]//create player table data
        public HttpResponseMessage Addplayer(Player player)
        {
            try
            {
                db.Players.Add(player);
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, player.reg_number);


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }

        }

        //update player table data
        [HttpPost]
        public HttpResponseMessage Updateplayerdetail(Player player)
        {
            try
            {
                var orignal = db.Players.Find(player.reg_number);
                if (orignal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "player Not Found");
                }
                db.Entry(orignal).CurrentValues.SetValues(player);
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "player is Modified");


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }

        }
        //delete team table data
        [HttpGet]
        public HttpResponseMessage Deleteplayer(string reg_number)
        {
            try
            {
                var orignal = db.Players.Find(reg_number);
                if (orignal == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "player Not Found");
                }
                db.Entry(orignal).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "player is Deleted");


            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

            }
        }
    }
}