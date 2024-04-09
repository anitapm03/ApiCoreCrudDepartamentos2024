using ApiCoreCrudDepartamentos.Models;
using ApiCoreCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>>
            GetDepartamentos()
        {
            return await this.repo.GetDepartamentosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>>
            FindDepartamento(int id)
        {
            return await this.repo.FindDepartamentoAsync(id);
        }

        //LOS METODOS POR DEFECTO DE POST O PUT, RECIBEN UN OBJETO
        //SI QUISIERAMOS RECIBIR LOS DATOS POR PARAMETROS, 
        //DEBEMOS UTILIZAR Route
        //PODEMOS PERSONALIZAR LA RESPUESTA EN EL CASO QUE NO NOS 
        //GUSTE ALGO, PUDIENDO DEVOLVER NotFound, BadRequest
        [HttpPost]
        public async Task<ActionResult> PostDepartamento
            (Departamento departamento)
        {
            await this.repo.InsertDepartamentoAsync(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //PODEMOS PERSONALIZAR LA RESPUESTA
            if (await this.repo.FindDepartamentoAsync(id) == null)
            {
                //NO EXISTE EL DEPARTAMENTO PARA ELIMINARLO
                return NotFound();
            }
            else
            {
                await this.repo.DeleteDepartamentoAsync(id);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutDepartamento
            (Departamento departamento)
        {
            await this.repo.UpdateDepartamentoAsync(departamento.IdDepartamento
                , departamento.Nombre, departamento.Localidad);
            return Ok();
        }

        //PODEMOS TENER TODOS LOS METODOS POST/PUT/DELETE QUE DESEEMOS
        //DEBEMOS PERSONALIZARLOS CON Route
        [HttpPost]
        [Route("[action]/{id}/{nombre}/{localidad}")]
        public async Task<ActionResult> InsertParams
            (int id, string nombre, string localidad)
        {
            await this.repo.InsertDepartamentoAsync(id, nombre, localidad);
            return Ok();
        }

        //TAMBIEN PODEMOS COMBINAR RECIBIR OBJETOS CON ROUTES
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> UpdateParams
            (int id, Departamento departamento)
        {
            await this.repo.UpdateDepartamentoAsync(id,
                departamento.Nombre, departamento.Localidad);
            return Ok();
        }
    }
}
