using AccountingNotebook.Data;
using AccountingNotebook.Services.Models;
using System;
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



        public async Task<Transaction> CreateTransactionAsync(CreateTransaction createTransaction)
        {
            if ((await CalculateResultingCreditAsync(createTransaction.Type, createTransaction.Amount)) < 0)
            {
                throw new ArgumentOutOfRangeException("Not enough credit to perform that transaction");
            }

            var entity = new Data.Entities.Transaction { Type = createTransaction.Type, Amount = createTransaction.Amount };
            var result = await _provider.CreateAsync(entity);

            return new Transaction { Amount = result.Amount, EffectiveDate = result.EffectiveDate, Id = result.Id, Type = result.Type };
        }

        private async Task<double> CalculateResultingCreditAsync(TransactionType type, double amount)
        {
            var currentBalance = await _provider.GetAvailableFundsAsync();
            return type == TransactionType.Credit ? currentBalance + amount : currentBalance - amount;
        }
    }
}
