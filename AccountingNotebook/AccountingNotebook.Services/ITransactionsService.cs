using AccountingNotebook.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingNotebook.Services
{
    public interface ITransactionsService
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task<Transaction> CreateTransactionAsync(CreateTransaction createTransaction);
    }
}
