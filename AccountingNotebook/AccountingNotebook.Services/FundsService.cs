using AccountingNotebook.Data;
using System.Threading.Tasks;

namespace AccountingNotebook.Services
{
    public class FundsService : IFundsService
    {
        private readonly ITransactionsProvider _provider;

        public FundsService(ITransactionsProvider provider)
        {
            _provider = provider;
        }

        public async Task<double> GetCurrentFundsAsync()
        {
            return await _provider.GetAvailableFundsAsync();
        }
    }
}
