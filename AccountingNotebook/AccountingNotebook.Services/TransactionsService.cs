using AccountingNotebook.Data;
using AccountingNotebook.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingNotebook.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ITransactionsProvider _provider;

        public TransactionsService(ITransactionsProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var result = await _provider.GetAllAsync();
            return result.Select(t => new Transaction { Amount = t.Amount, EffectiveDate = t.EffectiveDate, Id = t.Id, Type = t.Type });
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            var result = await _provider.GetByIdAsync(id);
            return result == null ? null : new Transaction { Amount = result.Amount, EffectiveDate = result.EffectiveDate, Id = result.Id, Type = result.Type };
        }
    }
}
