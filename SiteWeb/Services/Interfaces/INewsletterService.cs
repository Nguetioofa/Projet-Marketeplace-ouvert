using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface INewsletterService
    {
        public Task<ActionResult<List<Newsletter>>> GetNewsletters();
        public Task<ActionResult<Newsletter>> GetNewsletter(int id);
        public Task<bool> UpdateNewsletter(Newsletter Newsletter);
        public Task<bool> AddNewsletter(Newsletter Newsletter);
        public Task<bool> DeleteNewsletter(int id);
    }
}
