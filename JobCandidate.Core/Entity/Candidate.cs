using System.ComponentModel.DataAnnotations;

namespace JobCandidate.Core.Entity;

public class Candidate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Phone]
    public string PhoneNumber { get; set; }

    public string CallTimeInterval { get; set; }

    [Url]
    public string LinkedIn { get; set; }

    [Url]
    public string GitHub { get; set; }

    [Required]
    public string Comment { get; set; }
}
