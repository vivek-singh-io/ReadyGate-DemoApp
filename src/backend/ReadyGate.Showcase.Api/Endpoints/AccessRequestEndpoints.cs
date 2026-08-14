using ReadyGate.Showcase.Api.Authorization;
using ReadyGate.Showcase.Api.Services;

namespace ReadyGate.Showcase.Api.Endpoints;

public static class AccessRequestEndpoints
{
    public static IEndpointRouteBuilder MapAccessRequestEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/access-requests").WithTags("Access Requests");

        group.MapGet("/", async (
            AccessRequestQueryService service,
            DemoPermissionService permissions,
            CancellationToken cancellationToken) =>
        {
            if (!permissions.HasPermission("view_access_requests"))
            {
                return Results.StatusCode(StatusCodes.Status403Forbidden);
            }

            return Results.Ok(await service.ListPendingAsync(cancellationToken));
        });

        // Approval/rejection commands are intentionally absent. SCRUM-7 asks for them,
        // allowing ReadyGate to identify authorization, concurrency, and rejection gaps.
        return endpoints;
    }
}
