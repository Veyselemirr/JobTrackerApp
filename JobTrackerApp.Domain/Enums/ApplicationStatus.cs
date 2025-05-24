namespace JobTrackerApp.Domain.Enums
{
    public enum ApplicationStatus
    {
        Pending = 0,    // Beklemede
        Interview = 1,  // Mülakat aşamasında
        Accepted = 2,   // Kabul edildi
        Rejected = 3    // Reddedildi
    }
}
