using JobCandidate.Core.Dtos;
using JobCandidate.Core.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace JobCandidate.Tests;

[TestFixture]
public class CandidateApiIntegrationTests
{
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }

    [TearDown]
    public void Teardown()
    {
        _client.Dispose();
    }

    [Test]
    public async Task CreateCandidateApi_ShouldAddNewCandidate()
    {
        // Arrange
        var candidateDto = new CandidateDto
        {
            FirstName = "Smaran",
            LastName = "Luitel",
            Email = "smaran@test.com",
            PhoneNumber = "1234567890",
            Comment = "Needs 2 months notice period"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/candidate", candidateDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var createdCandidate = await response.Content.ReadFromJsonAsync<Candidate>();
        Assert.That(createdCandidate, Is.Not.Null);
        Assert.That(createdCandidate.FirstName, Is.EqualTo("Smaran"));
    }

    [Test]
    public async Task CreateCandidateApi_ShouldReturnBadRequest_WhenEmailIsMissing()
    {
        // Arrange
        var candidateDto = new CandidateDto
        {
            FirstName = "John",
            LastName = "Smith",
            PhoneNumber = "1234567890",
            Comment = "Excited to join"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/candidate", candidateDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task UpdateCandidateApi_ShouldModifyExistingCandidate()
    {
        // Arrange
        var candidateDto = new CandidateDto
        {
            FirstName = "Mission",
            LastName = "Luitel",
            Email = "mission@test.com",
            PhoneNumber = "9876543210",
            Comment = "Available for new opportunities"
        };

        // Create candidate first
        await _client.PostAsJsonAsync("/api/candidate", candidateDto);

        // Act - Update comment
        candidateDto.Comment = "Looking for remote roles";
        var response = await _client.PostAsJsonAsync("/api/candidate", candidateDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var updatedCandidate = await response.Content.ReadFromJsonAsync<Candidate>();
        Assert.That(updatedCandidate, Is.Not.Null);
        Assert.That(updatedCandidate.Comment, Is.EqualTo("Looking for remote roles"));
    }
}