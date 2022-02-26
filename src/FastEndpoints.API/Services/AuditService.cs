namespace FastApiIntegration.API.Services;

public class AuditService : IAuditService
{
    public Task RecordAudit(string action, string description)
    {
        // does something
        return Task.CompletedTask;
    }
}
