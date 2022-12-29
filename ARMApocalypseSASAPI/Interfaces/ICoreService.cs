using ARMApocalypseSASAPI.Dtos;

namespace ARMApocalypseSASAPI.Interfaces
{
    public interface ICoreService
    {
        Task<GenericResponse<IEnumerable<ItemResponse>>> GetAllItems();
        Task<GenericResponse<SurvivorResponse>> RegisterSurvivor(RegisterSurvivorRequest request);
        Task<GenericResponse<SurvivorResponse>> UpdateSurvivorLocation(UpdateSurvivorLocationRequest request);
        Task<GenericResponse<SurvivorInfectionReportResponse>> ReportSurvivorInfectionStatus(UpdateSurvivorInfectionStatusRequest request);



    }
}
