using ReadyGate.Showcase.Api.Domain;

namespace ReadyGate.Showcase.Api.Endpoints;

public sealed record TicketListItem(
    int Id,
    string Reference,
    string Subject,
    TicketStatus Status,
    TicketPriority Priority,
    string CustomerName,
    DateTimeOffset UpdatedAtUtc);

public sealed record TicketListResponse(IReadOnlyList<TicketListItem> Items, int Total);

public sealed record ExportTicketsRequest(IReadOnlyCollection<int> TicketIds);
