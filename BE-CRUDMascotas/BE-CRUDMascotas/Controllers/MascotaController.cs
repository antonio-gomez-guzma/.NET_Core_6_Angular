using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MascotaController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaMascotas = await _context.Mascotas.ToListAsync();
                return Ok(listaMascotas);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(id);

                if (mascota == null)
                {
                    return NotFound();
                }
                return Ok(mascota); 
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(id);

                if(mascota == null)
                {  return NotFound();}

                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();

                return NoContent();

            }catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Mascota mascota)
        {
            try
            {
                mascota.FechaCreacion = DateTime.Now;

                _context.Add(mascota);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = mascota.Id }, mascota);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Mascota mascota)
        {
            try
            {
                if(id != mascota.Id)
                {
                    return BadRequest();
                }

                var mascotaItem = await _context.Mascotas.FindAsync(id);

                if(mascotaItem == null)
                {
                    return NotFound();
                }

                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Color = mascota.Color;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Peso = mascota.Peso;

                _context.Update(mascota);
                await _context.SaveChangesAsync();

                return NotFound(); 

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
