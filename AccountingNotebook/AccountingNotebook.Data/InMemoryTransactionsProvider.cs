﻿using AccountingNotebook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingNotebook.Data
{
    public class InMemoryTransactionsProvider : ITransactionsProvider
    {
        private static readonly IList<Transaction> _transactions = 
            new List<Transaction> 
            { 
                new Transaction { Id = 1, Amount = 125.43, Type = TransactionType.Credit, EffectiveDate = new DateTime(2020, 4, 12)  },
                new Transaction { Id = 2, Amount = 80.27, Type = TransactionType.Debit, EffectiveDate = new DateTime(2020, 8, 4)  },
            };

        public InMemoryTransactionsProvider()
        {
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return _transactions;
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return _transactions.SingleOrDefault(t => t.Id == id);
        }
    }
}
