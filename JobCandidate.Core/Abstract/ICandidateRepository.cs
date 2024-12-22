using JobCandidate.Core.Entity;

namespace JobCandidate.Core.Abstract;

public interface ICandidateRepository
{
    Task<Candidate> GetByEmailAsync(string email);
    Task AddAsync(Candidate candidate);
    Task SaveAsync();
}
