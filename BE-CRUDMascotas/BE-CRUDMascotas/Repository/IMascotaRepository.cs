using BE_CRUDMascotas.Models;

namespace BE_CRUDMascotas.Repository
{
    public interface IMascotaRepository 
    {

        Task<List<Mascota>>GetListMascotas();

        Task<Mascota> GetMascotaById(int id);

        Task DeleteMascota(Mascota mascota);

        Task<Mascota> addMascota(Mascota mascota);

        Task UpdateMascota(Mascota mascota);
    }
}
