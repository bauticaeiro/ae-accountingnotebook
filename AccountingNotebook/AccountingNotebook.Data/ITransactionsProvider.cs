using AccountingNotebook.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingNotebook.Data
{
    public interface ITransactionsProvider
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
    }
}
