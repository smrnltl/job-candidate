using JobCandidate.Core.Abstract;
using JobCandidate.Core.Context;
using JobCandidate.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobCandidate.Infrastructure.Repository;

public class CandidateRepository(CandidateContext context) : ICandidateRepository
{
    public async Task<Candidate> GetByEmailAsync(string email)
    {
        return await context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task AddAsync(Candidate candidate)
    {
        await context.Candidates.AddAsync(candidate);
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
