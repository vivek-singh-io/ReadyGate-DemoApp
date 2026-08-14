using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Authorization;
using ReadyGate.Showcase.Api.Services;

namespace ReadyGate.Showcase.Api.Endpoints;

public static class TicketEndpoints
{
    public static IEndpointRouteBuilder MapTicketEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/tickets").WithTags("Tickets");

        group.MapGet("/", async (
            TicketStatus? status,
            TicketPriority? priority,
            TicketQueryService service,
            DemoPermissionService permissions,
            CancellationToken cancellationToken) =>
        {
            if (!permissions.HasPermission("view_tickets"))
            {
                return Results.StatusCode(StatusCodes.Status403Forbidden);
            }

            var result = await service.SearchAsync(status, priority, cancellationToken);
            return Results.Ok(result);
        });

        // HACKATHON FIXTURE ONLY: this endpoint intentionally does not call
        // DemoPermissionService. It exists so ReadyGate can identify the missing
        // server-side export authorization and other compliance controls.
        group.MapPost("/export", async (
            ExportTicketsRequest request,
            CsvExportService service,
            CancellationToken cancellationToken) =>
        {
            var content = await service.ExportAsync(request.TicketIds, cancellationToken);
            return Results.File(content, "text/csv", "flagged-tickets.csv");
        });

        return endpoints;
    }
}
