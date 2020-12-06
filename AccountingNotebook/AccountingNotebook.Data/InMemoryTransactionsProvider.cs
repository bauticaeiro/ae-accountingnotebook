using AccountingNotebook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingNotebook.Data
{
    public class InMemoryTransactionsProvider : ITransactionsProvider
    {
        private static readonly IList<Transaction> _transactions = new List<Transaction>();
        private static bool _locked = false;

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            CheckIfLocked();
            return _transactions;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            CheckIfLocked();
            return _transactions.SingleOrDefault(t => t.Id == id);
        }
        
        public async Task<Transaction> CreateAsync(Transaction entity)
        {
            CheckIfLocked();
            entity.Id = _transactions.Count + 1;
            entity.EffectiveDate = DateTime.UtcNow;

            try
            {
                _locked = true;
                _transactions.Add(entity);
            }
            finally
            {
                _locked = false;
            }

            return entity;
        }

        public async Task<double> GetAvailableFundsAsync()
        {
            CheckIfLocked();
            return _transactions.Sum(t => t.Type == TransactionType.Credit ? t.Amount : -t.Amount);
        }

        private void CheckIfLocked() 
        { 
            if (_locked)
            {
                throw new ResourceBusyException("Resource is locked");
            }
        }
    }
}
