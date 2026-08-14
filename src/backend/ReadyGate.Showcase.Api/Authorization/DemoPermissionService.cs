namespace ReadyGate.Showcase.Api.Authorization;

/// <summary>
/// A small, replaceable authorization seam used by the showcase API. Real systems should
/// derive permissions from a validated identity, not directly from a request header.
/// </summary>
public sealed class DemoPermissionService(IHttpContextAccessor httpContextAccessor)
{
    public bool HasPermission(string requiredPermission)
    {
        var rawPermissions = httpContextAccessor.HttpContext?.Request.Headers["X-Demo-Permissions"].ToString();
        if (string.IsNullOrWhiteSpace(rawPermissions))
        {
            return false;
        }

        return rawPermissions.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Contains(requiredPermission, StringComparer.OrdinalIgnoreCase);
    }
}
