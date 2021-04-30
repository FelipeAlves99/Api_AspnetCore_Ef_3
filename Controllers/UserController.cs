using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();
            return Ok(users);            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(user); 
        }        

        [HttpPost]
        public async Task<ActionResult<List<User>>> Post(
            [FromBody] User model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível adicionar o usuário" });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<List<User>>> Put(
            int id,
            [FromBody] User model,
            [FromServices] DataContext context)
        {
            if (model.Id != id)
                return NotFound(new { message = "Usuário não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Este registro já foi atualizado" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível alterar o usuário" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> Delete(
            int id,
            [FromServices] DataContext context
        )
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return BadRequest(new { message = "Usuário não encontrada" });

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return Ok(new { message = "Usuário removida com sucesso!" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível deletar esse usuário" });
            }
        }
    }
}
