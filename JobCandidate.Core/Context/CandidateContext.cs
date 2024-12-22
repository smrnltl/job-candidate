using JobCandidate.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace JobCandidate.Core.Context;

public class CandidateContext(DbContextOptions<CandidateContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
}
