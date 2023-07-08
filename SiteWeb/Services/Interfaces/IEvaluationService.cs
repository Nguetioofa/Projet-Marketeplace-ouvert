using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Services.Interfaces
{
    public interface IEvaluationService
    {
        public Task<ActionResult<List<EvaluationL>>> GetEvaluations();
        public Task<ActionResult<EvaluationL>> GetEvaluation(int id);
        public Task<bool> UpdateEvaluation(EvaluationL Evaluation);
        public Task<bool> AddEvaluation(EvaluationL Evaluation);
        public Task<bool> DeleteEvaluation(int id);
    }
}
