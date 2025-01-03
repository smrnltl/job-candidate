using JobCandidate.Core.Context;
using JobCandidate.Core.Entity;
using JobCandidate.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace JobCandidate.Tests;

[TestFixture]
public class CandidateRepositoryTests
{
    private static CandidateContext InMemoryDbContext
    {
        get
        {
            var options = new DbContextOptionsBuilder<CandidateContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            return new CandidateContext(options);
        }
    }

    [Test]
    public async Task AddCandidate_ShouldAddNewCandidate()
    {
        // Arrange
        var dbContext = InMemoryDbContext;
        var repository = new CandidateRepository(dbContext);
        var candidate = new Candidate
        {
            FirstName = "Smaran",
            LastName = "Luitel",
            Email = "smaran@test.com",
            PhoneNumber = "1234567890",
            Comment = "Needs 2 months notice period"
        };

        // Act
        await repository.AddAsync(candidate);
        await repository.SaveAsync();

        // Assert
        var result = await repository.GetByEmailAsync("smaran@test.com");
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("Smaran"));
    }

    [Test]
    public async Task UpdateCandidate_ShouldModifyExistingCandidate()
    {
        // Arrange
        var dbContext = InMemoryDbContext;
        var repository = new CandidateRepository(dbContext);
        var candidate = new Candidate
        {
            FirstName = "Mission",
            LastName = "Luitel",
            Email = "mission@test.com",
            PhoneNumber = "9876543210",
            Comment = "Prefers working late"
        };

        await repository.AddAsync(candidate);
        await repository.SaveAsync();

        // Act
        var existingCandidate = await repository.GetByEmailAsync("mission@test.com");
        existingCandidate.Comment = "Available anytime";
        await repository.SaveAsync();

        // Assert
        var updatedCandidate = await repository.GetByEmailAsync("mission@test.com");
        Assert.That(updatedCandidate.Comment, Is.EqualTo("Available anytime"));
    }

    [Test]
    public async Task GetCandidateByEmail_ShouldReturnNull_WhenCandidateDoesNotExist()
    {
        // Arrange
        var dbContext = InMemoryDbContext;
        var repository = new CandidateRepository(dbContext);

        // Act
        var candidate = await repository.GetByEmailAsync("test@test.com");

        // Assert
        Assert.That(candidate, Is.Null);
    }
}