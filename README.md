# ReadyGate Demo Application

ReadyGate DemoApp is a fictional ticket-management repository used as the external inspection and
generation target for the ReadyGate hackathon. It demonstrates how ReadyGate combines a Jira ticket,
a pinned repository revision, repository-local engineering guidance, and human-controlled delivery.

> [!WARNING]
> This repository is a hackathon fixture, not production software. The CSV-export path deliberately
> contains security and compliance gaps so ReadyGate can discover them. Never copy those gaps into a
> production implementation.

## Canonical Jira scenarios

| Jira key | Scenario | Intended outcome | Delivery rule |
| --- | --- | --- | --- |
| `SCRUM-7` | Approve or reject access requests | Block | No development branch |
| `SCRUM-8` | Export selected flagged tickets to CSV | Conditional | No branch until the ticket is remediated and passes |
| `SCRUM-9` | Filter tickets by status and priority | Pass | A human may prepare the Jira-key draft branch |

`SCRUM-7` is the **Block intent** fixture. A model result that differs from that intent is calibration
evidence, not permission to relabel the fixture. The exact expected evidence and intentional gaps are
recorded in [docs/fixture-manifest.md](docs/fixture-manifest.md).

## Verified repository baseline

- Angular 22.1 standalone frontend with TypeScript 6 and npm.
- .NET 10 minimal API.
- Repository guidance in `.readygate/readygate.yaml` and `.readygate/READYGATE.md`.
- Xray-importable Cucumber scenarios under `tests/xray`.

SQLite/EF Core, business features, and their automated tests are verified only when their declared
paths exist on the pinned revision. The fixture manifest intentionally distinguishes existing evidence
from expected paths.

## Intended project structure

```text
.readygate/                              ReadyGate profile and repository guidance
docs/                                    Fixture inventory and reference guidance
src/backend/ReadyGate.Showcase.Api/      .NET minimal API
src/frontend/readygate-showcase-client/  Angular application
tests/backend/                           Backend and isolated-SQLite tests
tests/xray/                              Xray Cucumber exchange scenarios
```

## Intentional hackathon-only gaps

The initial `SCRUM-8` export fixture is expected to:

- omit server-side `export_tickets` authorization;
- export unmasked fictional email addresses and phone numbers;
- omit export audit logging;
- omit the 500-ticket request limit; and
- leave empty-selection behavior undefined.

These omissions must remain visible and documented until the demo explicitly shows ticket remediation.
They are not approved style precedents. Safe references and exclusion rules are documented in
[docs/reference-implementations.md](docs/reference-implementations.md).

## Local validation commands

Run these commands from a clean checkout after dependencies are restored. Their presence here does not
claim that CI or the test suites have passed.

```powershell
Set-Location src/backend
dotnet test ReadyGate.Showcase.slnx

Set-Location ../frontend/readygate-showcase-client
npm ci
npm test -- --watch=false
npm run build
```

## Importing the Xray scenarios

The files under `tests/xray` are standard Gherkin and contain Jira-key tags. Import them through Xray's
Cucumber feature import workflow. They are exchange artifacts: execution evidence must be attached by
the team or automation after the scenarios actually run.

## Security and governance

- Use fictional customers and tickets only.
- Never commit `.env` files, Jira/GitHub/AWS credentials, tokens, or real customer data.
- Never commit directly to protected `main`.
- Developer branches use `<JIRA-key>-<short-description>`.
- ReadyGate draft branches use `<JIRA-key>-ticket-draft-<short-description>` and require human approval.
- ReadyGate never executes generated SQL or migrations and never creates a branch for Block or Conditional.

See [.readygate/READYGATE.md](.readygate/READYGATE.md) for repository-specific generation rules.
