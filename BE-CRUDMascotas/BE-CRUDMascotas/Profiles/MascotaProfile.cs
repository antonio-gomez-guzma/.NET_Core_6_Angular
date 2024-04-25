using AutoMapper;
using BE_CRUDMascotas.DTO;
using BE_CRUDMascotas.Models;

namespace BE_CRUDMascotas.Profiles
{
    public class MascotaProfile : Profile
    {
        public MascotaProfile() 
        {
            CreateMap<Mascota,MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }
    }
}
