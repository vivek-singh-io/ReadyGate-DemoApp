# ReadyGate DemoApp Architecture

The DemoApp is a deliberately small Angular 22 and .NET 10 reference application. Angular standalone
features call a typed HTTP client; thin Minimal API endpoint modules delegate query behavior to
injected services; services use an EF Core SQLite context containing fictional seeded data.

Safe generation precedents are the ticket query, access-request query, permission, persistence, and
their tests. `CsvExportService` and the export mapping inside `TicketEndpoints` are hackathon fixtures
with intentional authorization, masking, audit, and selection-limit gaps. They must never be used as
approved style or security precedents.

Database changes use reviewed EF Core migrations. ReadyGate may draft migrations and rollback/forward-
fix notes on a Jira-key branch, but it must never execute them, merge them, or deploy them.
