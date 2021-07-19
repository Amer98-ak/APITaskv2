using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using APITaskv2;
using Models;

namespace APITaskv2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ToDoesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ToDoes 
        public IQueryable<ToDo> GetToDo()
        {
            return db.ToDo;
        }

        // GET: api/ToDoes/5
        [ResponseType(typeof(ToDo))]
        public async Task<IHttpActionResult> GetToDo(int id)
        {
            ToDo toDo = await db.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        // PUT: api/ToDoes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutToDo(int id, ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.Id)
            {
                return BadRequest();
            }

            db.Entry(toDo).State = EntityState.Modified;

            try
            {
                toDo.ModifiedOn = DateTime.Now;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ToDoes
        [ResponseType(typeof(ToDo))]
        public async Task<IHttpActionResult> PostToDo(ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            toDo.CreatedOn = toDo.ModifiedOn = DateTime.Now;
            db.ToDo.Add(toDo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/ToDoes/5
        [ResponseType(typeof(ToDo))]
        public async Task<IHttpActionResult> DeleteToDo(int id)
        {
            ToDo toDo = await db.ToDo.FindAsync(id);
            if (toDo == null)
            {
                return NotFound();
            }

            db.ToDo.Remove(toDo);
            await db.SaveChangesAsync();

            return Ok(toDo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoExists(int id)
        {
            return db.ToDo.Count(e => e.Id == id) > 0;
        }
    }
}