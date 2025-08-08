using MediAgenda.DTOs.Reception;

namespace MediAgenda.Interface.Reception
{
    public interface IReceptionService
    {
        Task<IEnumerable<ReceptionDto>> GetAllAsync();
        Task<ReceptionDto> GetByIdAsync(Guid id);
    }
}
