using System.Threading.Tasks;

namespace AccountingNotebook.Services
{
    public interface IFundsService
    {
        Task<double> GetCurrentFundsAsync();
    }
}
