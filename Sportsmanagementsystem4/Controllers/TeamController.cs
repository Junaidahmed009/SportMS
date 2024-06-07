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

        [HttpPost]
        public HttpResponseMessage PlayerEntry(PlayerentryDTO playerDto)
        {
            // Check if player name, registration number, role, and user ID are provided
            if (string.IsNullOrEmpty(playerDto.name) || string.IsNullOrEmpty(playerDto.reg_number) || string.IsNullOrEmpty(playerDto.role) || playerDto.id <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Player name, registration number, role, and valid user ID are required.");
            }

            try
            {
                // Check if the user ID exists in the User table
                var userInDb = db.Users.FirstOrDefault(u => u.id == playerDto.id);
                if (userInDb == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Event manager ID.");
                }

                // Check if the user is an event manager
                if (userInDb.role != "event manager")
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Only event managers can add players.");
                }

                var playerInDb = db.Players.FirstOrDefault(p => p.reg_number == playerDto.reg_number);

                // If the player does not exist, create a new player
                if (playerInDb == null)
                {
                    var newPlayer = new Player
                    {
                        name = playerDto.name,
                        reg_number = playerDto.reg_number,
                        role = playerDto.role
                    };

                    db.Players.Add(newPlayer);
                    db.SaveChanges();

                    // Assign the newly created player to playerInDb
                    playerInDb = newPlayer;
                }


                // Find the sport associated with the user ID
                var sport = db.Sports.FirstOrDefault(s => s.user_id == playerDto.id);
                if (sport == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No sport associated with the user.");
                }

                // Check if the player is already registered for the sport in Playerteaminfo table
                var playerTeamInfo = db.Playerteaminfoes.FirstOrDefault(pti => pti.player_regno == playerDto.reg_number && pti.sport_id == sport.id);
                if (playerTeamInfo != null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Player is already registered for this sport.");
                }
                // Find the team based on the provided team name
                var team = db.Teams.FirstOrDefault(t => t.name == playerDto.teamName);
                if (team == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid team name.");
                }
                // Check if the user ID and sport ID match the team's user ID and sport ID
                if (team.user_id != userInDb.id || team.sport_id != sport.id)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "User ID and sport ID do not match the team.");
                }
                // Add player to Playerteaminfo table
                var newPlayerTeamInfo = new Playerteaminfo
                {
                    player_regno = playerDto.reg_number,
                    sport_id = sport.id,
                    team_id = team.id
                };
                db.Playerteaminfoes.Add(newPlayerTeamInfo);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, "Player data successfully entered in db and registered for the sport.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }





    }
}

