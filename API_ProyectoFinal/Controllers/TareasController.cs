using API_ProyectoFinal.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.IO;

namespace API_ProyectoFinal.Controllers
{
    [ApiController]
    [Route("tareas")]
    public class TareasController : ControllerBase
    {
        [HttpPost]
        [Route("agregar")]
        public IActionResult Agregar(Request request)
        {
            try
            {
                String FileName = "main.py";
                var carpeta = '"' + request.PathCarpeta + '"';
                var tema = '"' + request.Tema + '"';
                var categoria = '"' + request.Categoria + '"';
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = "python.exe";
                start.Arguments = string.Format("{0} {1} {2} {3}", FileName, carpeta, tema, categoria);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;

                Process process = new Process();
                process.StartInfo = start;
                process.Start();

                StreamReader reader = process.StandardOutput;
                string? result = reader.ReadLine();
                process.WaitForExit();
                process.Close();
                if (result == "True")
                {
                    return Ok("Tarea agregada");
                }
                else
                {
                    return BadRequest("No se pudo agregar la tarea");
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("No se pudo agregar la tarea, no se cumplio");
            }
        }
        [HttpPut]
        [Route("actualizar")]
        public IActionResult Actualizar(Tarea tarea)
        {
            try
            {
                var collection = Database.GetConnection();
                var filter = Builders<Tarea>.Filter.Eq("Id", tarea.Id);
                var update = Builders<Tarea>.Update.Set("nombre", tarea.Nombre).Set("carnet", tarea.Carnet).Set("resumen", tarea.Resumen).Set("nota", tarea.Nota);
                collection.UpdateOne(filter, update);
                return Ok("Tarea actualizada");
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo actualizar la tarea");
            }
        }
        [HttpDelete]
        [Route("eliminar")]
        public IActionResult Eliminar(List<Tarea> tareas)
        {
            try
            {
                Console.WriteLine(tareas);
                var collection = Database.GetConnection();
                var filter = Builders<Tarea>.Filter.In("Id", tareas);
                collection.DeleteMany(filter);
                return Ok("Tarea eliminada");
            }
            catch (Exception)
            {
                return BadRequest("No se pudo eliminar la tarea");
            }
        }
        [HttpGet]
        [Route("listar")]
        public IActionResult Listar()
        {
            try
            {
                var collection = Database.GetConnection();
                var filter = Builders<Tarea>.Filter.Empty;
                var result = collection.Find(filter).ToList();
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("No se pudo listar las tareas");
            }
        }
    }
}
