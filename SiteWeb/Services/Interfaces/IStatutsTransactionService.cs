using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IStatutsTransactionService
    {
        public Task<ActionResult<List<StatutsTransactionL>>> GetStatutsTransactions();
        public Task<ActionResult<StatutsTransactionL>> GetStatutsTransaction(int id);
        public Task<bool> UpdateStatutsTransaction(StatutsTransactionL StatutsTransaction);
        public Task<bool> AddStatutsTransaction(StatutsTransactionL StatutsTransaction);
        public Task<bool> DeleteStatutsTransaction(int id);
    }
}
