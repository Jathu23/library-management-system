using library_management_system.DTOs.Chart;
using library_management_system.Repositories;

namespace library_management_system.Services
{
    public class ChartService
    {
        public readonly ChartRepository _chartRepository;

        public ChartService(ChartRepository repository)
        {
            this._chartRepository = repository;
        }
        public List<ChartData> GetBorrowingTrends()
        {
            return _chartRepository.GetBorrowingTrends();
        }
    }
}
