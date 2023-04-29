using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IStatutsTransactionService
    {
        public Task<ActionResult<List<StatutsTransaction>>> GetStatutsTransactions();
        public Task<ActionResult<StatutsTransaction>> GetStatutsTransaction(int id);
        public Task<bool> UpdateStatutsTransaction(StatutsTransaction StatutsTransaction);
        public Task<bool> AddStatutsTransaction(StatutsTransaction StatutsTransaction);
        public Task<bool> DeleteStatutsTransaction(int id);
    }
}
