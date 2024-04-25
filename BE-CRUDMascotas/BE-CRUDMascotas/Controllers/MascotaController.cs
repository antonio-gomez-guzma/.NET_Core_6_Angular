using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BE_CRUDMascotas.DTO;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MascotaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaMascotas = await _context.Mascotas.ToListAsync();

                var listaMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listaMascotas);
                return Ok(listaMascotasDTO);
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

                var mascotaDto = _mapper.Map<MascotaDTO>(mascota);

                return Ok(mascotaDto); 
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
        public async Task<IActionResult> Post(MascotaDTO mascotadto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotadto);

                mascota.FechaCreacion = DateTime.Now;

                _context.Add(mascota);
                await _context.SaveChangesAsync();

                var mascotaItemDto = _mapper.Map<MascotaDTO>(mascota);

                return CreatedAtAction("Get", new { id = mascotaItemDto.Id }, mascotaItemDto);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MascotaDTO mascotadto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotadto);
                if (id != mascota.Id)
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

                await _context.SaveChangesAsync();

                return NoContent(); 

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
