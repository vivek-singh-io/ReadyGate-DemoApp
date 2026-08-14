using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

namespace ReadyGate.Showcase.Api.Services;

/// <summary>
/// HACKATHON FIXTURE ONLY: this deliberately exports raw PII and omits authorization,
/// audit logging, masking, and a maximum-ticket limit. ReadyGate is expected to detect
/// those gaps. Do not copy this implementation into production code.
/// </summary>
public sealed class CsvExportService(ShowcaseDbContext dbContext)
{
    public async Task<byte[]> ExportAsync(IReadOnlyCollection<int> ticketIds, CancellationToken cancellationToken)
    {
        var tickets = await dbContext.Tickets
            .AsNoTracking()
            .Include(ticket => ticket.Customer)
            .Where(ticket => ticketIds.Contains(ticket.Id))
            .OrderBy(ticket => ticket.Reference)
            .ToListAsync(cancellationToken);

        var csv = new StringBuilder("Reference,CustomerName,Email,Phone,ResolutionNotes\r\n");
        foreach (var ticket in tickets)
        {
            csv.AppendLine(string.Join(',',
                Escape(ticket.Reference),
                Escape(ticket.Customer.Name),
                Escape(ticket.Customer.Email),
                Escape(ticket.Customer.Phone),
                Escape(ticket.ResolutionNotes)));
        }

        return Encoding.UTF8.GetBytes(csv.ToString());
    }

    private static string Escape(string value)
    {
        var neutralized = value.Length > 0 && "=+-@".Contains(value[0], StringComparison.Ordinal)
            ? $"'{value}"
            : value;

        return $"\"{neutralized.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
    }
}
