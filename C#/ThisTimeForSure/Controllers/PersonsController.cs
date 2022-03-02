using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using PersonAccess;
using System.Net.Http;

namespace ThisTimeForSure.Controllers
{
    public class PersonsController : ApiController
    {
        // return everyone in database
        public IEnumerable<Person> Get()
        {
            using (peopleEntities Peeps = new peopleEntities())
            {
                return Peeps.Persons.ToList();
            }
        }
        //return specific person in database
        public HttpResponseMessage Get(int id)
        {
            using (peopleEntities Peeps = new peopleEntities())
            {
                var Pers = Peeps.Persons.FirstOrDefault(e => e.Id == id);

                if (Pers != null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, Pers);
                }
                else
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "Person " + id + " Not Found");
                }
            }
        }
        //post people to database
        public HttpResponseMessage Post([FromBody] Person person)
        {
            using(peopleEntities Peeps = new peopleEntities())
            {
                try
                {
                    Peeps.Persons.Add(person);
                    Peeps.SaveChanges();

                    var message = Request.CreateResponse(System.Net.HttpStatusCode.Created, person);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + person.Id.ToString());
                    return message;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            using(peopleEntities Peeps = new peopleEntities())
            {
                Person p = (Person)Peeps.Persons.Find(id);
                if (p == null)
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "No Person with this ID was found");
                }
                else
                {
                    Peeps.Persons.Remove(p);
                    Peeps.SaveChanges();
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK);
                }
            }
        }
        public HttpResponseMessage Put(int id, [FromBody]Person person)
        {
            using(peopleEntities Peeps = new peopleEntities())
            {
                Person Peep = (Person)Peeps.Persons.Find(id);
                if (Peep != null)
                {
                    Peep.Name = person.Name;
                    Peep.Age = person.Age;
                    Peeps.SaveChanges();
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, Peep);
                }
                else
                {
                    return Request.CreateResponse(System.Net.HttpStatusCode.NotFound, "Person not found to update");
                }
            }
        }
    }
}