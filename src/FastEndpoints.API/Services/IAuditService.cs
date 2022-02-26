namespace FastApiIntegration.API.Services;

public interface IAuditService
{
    Task RecordAudit(string action, string description);
}
