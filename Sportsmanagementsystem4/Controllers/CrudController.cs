using Sportsmanagementsystem4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Sportsmanagementsystem4.Controllers
{
    public class CrudController : ApiController
    {

        SportsManagementDBEntities db=new SportsManagementDBEntities();

        //Event read
        [HttpGet]

        public HttpResponseMessage GetEvent()
        {
            try
            {
                var events = db.Events
                               .Select(e => new
                               {
                                   e.id,
                                   e.eventname,
                                   e.year,
                                   e.user_id,
                                   e.start_date,
                                   e.end_date
                               })
                               .ToList();

                return Request.CreateResponse(HttpStatusCode.OK, events);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }






        // Event read by id
        [HttpGet]

        public HttpResponseMessage GetEventbyname(string name)
        {
            try
            {
                var eventItem = db.Events
                                  .Where(e => e.eventname == name)
                                  .Select(e => new
                                  {
                                      e.id,
                                      e.eventname,
                                      e.year,
                                      e.user_id,
                                      e.start_date,
                                      e.end_date
                                  })
                                  .FirstOrDefault();

                if (eventItem == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found");
                }

                return Request.CreateResponse(HttpStatusCode.OK, eventItem);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        // Event post
        [HttpPost]

        public HttpResponseMessage PostEvent([FromBody] Event eventItem)
        {
            try
            {
                if (eventItem == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data");
                }

                db.Events.Add(eventItem);
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.Created, eventItem);
                response.Headers.Location = new Uri(Request.RequestUri + "/" + eventItem.id);
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // Event update
        [HttpPut]

        public HttpResponseMessage UpdateEvent(int id, [FromBody] Event eventItem)
        {
            try
            {
                if (eventItem == null || eventItem.id != id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data");
                }

                var existingEvent = db.Events.FirstOrDefault(e => e.id == id);
                if (existingEvent == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found");
                }

                existingEvent.eventname = eventItem.eventname;
                existingEvent.year = eventItem.year;
                existingEvent.start_date = eventItem.start_date;
                existingEvent.end_date = eventItem.end_date;

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, existingEvent);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // Event delete
        [HttpDelete]

        public HttpResponseMessage DeleteEvent(string name)
        {
            try
            {
                var eventItem = db.Events.FirstOrDefault(e => e.eventname == name);
                if (eventItem == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found");
                }

                db.Events.Remove(eventItem);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Event deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /*-----------------------------------------------------------------------------------*/
        //All schedules

        [HttpGet]
        public HttpResponseMessage GetSchedule()
        {

            try
            {
                var schedule = db.Schedules
                                         .Select(s => new
                                         {
                                             s.id,
                                             s.team1_id,
                                             s.team2_id,
                                             s.match_id,
                                             s.date,
                                             s.time

                                         })
                                         .OrderBy(b => b.id)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, schedule);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage InsertSchedule(Schedule schedule)
        {

            try
            {
                db.Schedules.Add(schedule);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Data Insert at" + schedule.id);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ModifySchedule(Schedule schedule)
        {

            try
            {
                var original = db.Schedules.Find(schedule.id);
                if (original == null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Schedule not found");
                }
                db.Entry(original).CurrentValues.SetValues(schedule);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Schedule Modified");



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public HttpResponseMessage DeleteSchedule(int id)
        {

            try
            {
                var original = db.Schedules.Find(id);
                if (original == null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Schedule not found");
                }
                db.Entry(original).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Schedule Deleted");



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage SearchSchedule(int matchid)
        {

            try
            {
                var schedule = db.Schedules.Where(b => b.match_id == matchid)
                                         .Select(s => new
                                         {
                                             s.id,
                                             s.team1_id,
                                             s.team2_id,
                                             s.match_id,
                                             s.date,
                                             s.time

                                         })
                                         .OrderBy(b => b.id)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, schedule);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        /*-----------------------------------------------------------------------------------*/
        //venue



        [HttpGet]
        public HttpResponseMessage AllVenues()
        {

            try
            {
                var venue = db.venues
                                         .Select(v => new
                                         {
                                             v.id,
                                             v.name,
                                             v.schedule_id


                                         })
                                         .OrderBy(b => b.id)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, venue);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertVenue(venue venue)
        {

            try
            {
                db.venues.Add(venue);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Data Inserted at " + venue.id);


            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ModifyVenue(venue venue)
        {

            try
            {
                var original = db.venues.Find(venue.id);
                if (original == null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Venue not found");
                }
                db.Entry(original).CurrentValues.SetValues(venue);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Venue Modified");



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteVenue(int id)
        {

            try
            {
                var original = db.venues.Find(id);
                if (original == null)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Venue not found");
                }
                db.Entry(original).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Venue Deleted");



            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage SearchVenue(String name)
        {

            try
            {
                var venue = db.venues.Where(b => b.name == name)
                                         .Select(s => new
                                         {
                                             s.id,
                                             s.name,
                                             s.schedule_id

                                         })
                                         .OrderBy(b => b.id)
                                         .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, venue);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }



    }
}