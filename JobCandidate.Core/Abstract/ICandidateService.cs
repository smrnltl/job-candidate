using JobCandidate.Core.Dtos;
using JobCandidate.Core.Entity;

namespace JobCandidate.Core.Abstract;

public interface ICandidateService
{
    Task<Candidate> AddOrUpdateCandidateAsync(CandidateDto candidateDto);
}
