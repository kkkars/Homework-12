using DepsWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    public class AccountServiceInMemory : IAccountService
    {
        // TODO: can chance on concurent dictionary and get rid of semaphores, login will be a key

        private List<Account> _accounts = new List<Account>();
        private readonly SemaphoreSlim _semaphore1 = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _semaphore2 = new SemaphoreSlim(1, 1);

        public async Task<bool> Add(Account newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount));
            }

            await _semaphore1.WaitAsync();
            try
            {
                var isExist = await Find(newAccount);

                if (isExist)
                {
                    return false;
                }

                _accounts.Add(newAccount);
                return true;
            }
            finally
            {
                _semaphore1.Release();
            }
        }

        public async Task<bool> Find(Account newAccount)
        {
            await _semaphore2.WaitAsync();
            try
            {
                return _accounts.Any(account => account.Login == newAccount.Login);
            }
            finally
            {
                _semaphore2.Release();
            }
        }
    }
}
