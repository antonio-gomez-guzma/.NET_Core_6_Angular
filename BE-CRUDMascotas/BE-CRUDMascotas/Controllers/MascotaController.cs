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

    }
}
