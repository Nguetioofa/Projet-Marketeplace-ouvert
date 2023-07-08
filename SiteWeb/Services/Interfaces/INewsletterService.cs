using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface INewsletterService
    {
        public Task<ActionResult<List<NewsletterL>>> GetNewsletters();
        public Task<ActionResult<NewsletterL>> GetNewsletter(int id);
        public Task<bool> UpdateNewsletter(NewsletterL Newsletter);
        public Task<bool> AddNewsletter(NewsletterL Newsletter);
        public Task<bool> DeleteNewsletter(int id);
    }
}
