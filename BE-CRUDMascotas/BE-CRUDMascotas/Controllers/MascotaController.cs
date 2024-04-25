using BE_CRUDMascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BE_CRUDMascotas.DTO;
using BE_CRUDMascotas.Repository;

namespace BE_CRUDMascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _mascotaRepository;

        public MascotaController(IMascotaRepository mascotaRepository, IMapper mapper)
        {          
            _mapper = mapper;
           _mascotaRepository = mascotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listaMascotas = await _mascotaRepository.GetListMascotas(); // PATRON REPOSITORIO

                var listaMascotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listaMascotas); // MAPEAR DTO

                return Ok(listaMascotasDTO); // DEVOLVIENDO DATOS
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
                var mascota = _mascotaRepository.GetMascotaById(id); //PATON REPOSITORIO

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
                var mascota = await _mascotaRepository.GetMascotaById(id);

                if(mascota == null)
                {  return NotFound();}

               await _mascotaRepository.DeleteMascota(mascota);

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

                mascota = await _mascotaRepository.addMascota(mascota);

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

                var mascotaItem = await _mascotaRepository.GetMascotaById(id);

                if(mascotaItem == null)
                {
                    return NotFound();
                }

               await _mascotaRepository.UpdateMascota(mascota);

                return NoContent(); 

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
