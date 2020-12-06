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
        private readonly IFundsService _fundsService;

        public TransactionsService(ITransactionsProvider provider, IFundsService fundsService)
        {
            _provider = provider;
            _fundsService = fundsService;
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

        public async Task<(Transaction, string)> CreateTransactionAsync(CreateTransaction createTransaction)
        {
            var errors = string.Empty;
            if (createTransaction.Amount == 0)
            {
                return (null, "Please specify a value for the transaction.");
            }
            if (await CalculateResultingCreditAsync(createTransaction) < 0)
            {
                return (null, "Insufficient funds to perform transaction");
            }

            var entity = new Data.Entities.Transaction { Type = createTransaction.Type, Amount = createTransaction.Amount };
            var result = await _provider.CreateAsync(entity);

            return (new Transaction { Amount = result.Amount, EffectiveDate = result.EffectiveDate, Id = result.Id, Type = result.Type }, errors);
        }

        private async Task<double> CalculateResultingCreditAsync(CreateTransaction createTransaction)
        {
            var currentBalance = await _fundsService.GetCurrentFundsAsync();
            return createTransaction.Type == TransactionType.Credit ? currentBalance + createTransaction.Amount : currentBalance - createTransaction.Amount;
        }
    }
}
