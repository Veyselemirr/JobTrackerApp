public class JobApplicationUpdateDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public DateTime AppliedDate { get; set; }
    public int Status { get; set; }
    public string? Notes { get; set; }
    public int UserId { get; set; }
    public string? Location { get; set; }
    public string? WorkModel { get; set; }
}
