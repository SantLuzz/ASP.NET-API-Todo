using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController] //informa que to trabalhando com api e retorna somente json
    //[Route("home")] //define o prefixo de rota
    public class HomeController : ControllerBase
    {
        //Todo método dentro do controller é um action

        [HttpGet("/")] //atributo para definir que esse método é um get
        //[Route("/")] //define a rota para chegar nesse método
        public IActionResult Get(
            [FromServices]AppDbContext context) //criando uma instancia do db context direto do serviço
            =>  Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")] //atributo para definir que esse método é um get passando qual a rota
        public IActionResult GetById(
            [FromRoute]int id, //deifinindo que vem da rota
            [FromServices] AppDbContext context //criando uma instancia do db context direto do serviço
           )
        {
            var todo = context.Todos.FirstOrDefault(x=>x.Id == id);
            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost("/")]
        public IActionResult Post(
            [FromBody]TodoModel todo,
            [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();

            return Created($"/{todo.Id}",todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute]int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x=> x.Id == id);

            if (model == null) return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;
            
            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null) return NotFound();

            context.Todos.Remove(model);
            context.SaveChanges();

            return Ok(model);
        }
    }
}
