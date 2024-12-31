using JobCandidate.Core.Abstract;
using JobCandidate.Core.Dtos;
using JobCandidate.Core.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidate.Infrastructure.Serice;

public class CandidateService(ICandidateRepository repository, IMemoryCache cache) : ICandidateService
{
    public async Task<Candidate> AddOrUpdateCandidateAsync(CandidateDto candidateDto)
    {
        if (string.IsNullOrWhiteSpace(candidateDto.Email))
        {
            throw new ArgumentException("Email is required.");
        }

        // Check cache first
        if (!cache.TryGetValue(candidateDto.Email, out Candidate candidate))
        {
            candidate = await repository.GetByEmailAsync(candidateDto.Email);
            if (candidate != null)
            {
                cache.Set(candidateDto.Email, candidate);
            }
        }

        if (candidate != null)
        {
            // Update existing candidate
            candidate.FirstName = candidateDto.FirstName ?? candidate.FirstName;
            candidate.LastName = candidateDto.LastName ?? candidate.LastName;
            candidate.PhoneNumber = candidateDto.PhoneNumber ?? candidate.PhoneNumber;
            candidate.CallTimeInterval = candidateDto.CallTimeInterval ?? candidate.CallTimeInterval;
            candidate.LinkedIn = candidateDto.LinkedIn ?? candidate.LinkedIn;
            candidate.GitHub = candidateDto.GitHub ?? candidate.GitHub;
            candidate.Comment = candidateDto.Comment ?? candidate.Comment;
        }
        else
        {
            // Create new candidate
            candidate = new Candidate
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                Email = candidateDto.Email,
                PhoneNumber = candidateDto.PhoneNumber,
                CallTimeInterval = candidateDto.CallTimeInterval,
                LinkedIn = candidateDto.LinkedIn,
                GitHub = candidateDto.GitHub,
                Comment = candidateDto.Comment
            };
            await repository.AddAsync(candidate);
        }

        await repository.SaveAsync();

        return candidate;
    }
}
