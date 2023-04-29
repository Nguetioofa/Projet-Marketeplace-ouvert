using Microsoft.AspNetCore.Mvc;

namespace ChangeToyServices.Interfaces
{
    public interface IEvaluationService
    {
        public Task<ActionResult<List<Evaluation>>> GetEvaluations();
        public Task<ActionResult<Evaluation>> GetEvaluation(int id);
        public Task<bool> UpdateEvaluation(Evaluation Evaluation);
        public Task<bool> AddEvaluation(Evaluation Evaluation);
        public Task<bool> DeleteEvaluation(int id);
    }
}
