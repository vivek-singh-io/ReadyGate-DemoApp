# ReadyGate DemoApp Repository Guidance

This repository is a fictional ticket-management application used as the external inspection target
for the ReadyGate hackathon demonstration. It is not the ReadyGate product repository.

## Architecture

- Angular 22 standalone frontend under `src/frontend/readygate-showcase-client`.
- .NET 9 minimal API under `src/backend/ReadyGate.Showcase.Api`.
- Thin HTTP endpoints delegate to injected services.
- Authorization policies remain separate from business services.
- SQLite and EF Core migrations provide the intended persistence convention.
- Tests use Vitest/Angular TestBed for the frontend and xUnit for the backend/database.
- All customers, tickets, emails, and phone numbers are fictional.

The repository must contain at least two safe, representative same-layer examples for every
code/test family that ReadyGate is expected to generate. Generated or intentionally incomplete files
must not be selected as coding-style precedents.

## Hackathon fixtures

The repository intentionally contains three evidence scenarios:

1. **Block:** an access-request footprint exists, while its Jira ticket omits authorization,
   concurrency, rejection, and notification detail.
2. **Conditional:** CSV export works but intentionally lacks server-side `export_tickets`
   enforcement, PII masking, audit logging, a 500-ticket limit, and defined empty-selection behavior.
3. **Pass:** ticket-list, view-permission, empty-state, status/priority filtering, API filtering, and
   debounce patterns exist and can be verified from stable source paths.

These gaps are test fixtures, not production recommendations. Keep the build green and document the
exact files and symbols in `docs/fixture-manifest.md`.

## Generation rules

ReadyGate-generated work must:

- derive from approved Spec Kit requirements and acceptance scenarios;
- use the exact repository revision and validated `readygate.yaml` profile;
- follow verified same-layer structure, naming, dependency injection, async, error handling,
  logging, authorization, data-access, fixture, mocking, and assertion conventions;
- use existing dependencies unless an approved plan explicitly permits a new one;
- generate database work only when required and when SQLite/EF conventions are verified;
- include rollback or forward-fix metadata and database tests for database changes;
- never execute generated SQL, migrations, seeds, or rollback scripts;
- record requirement, Jira, repository, reference-file, generator, and validation provenance;
- target a human-approved Jira-key branch and never protected `main`.

If the profile, architecture guidance, dependencies, and observed code disagree, stop the affected
artifact family and ask the repository owner to resolve the conflict. Do not guess.

## Branches and credentials

- Developer branch: `<JIRA-key>-<short-description>`.
- ReadyGate draft branch after Pass and human approval:
  `<JIRA-key>-ticket-draft-<short-description>`.
- Commit message: `<JIRA-key> <imperative summary>`.
- Never commit directly to `main`.
- Never store GitHub/Jira tokens, `.env` values, real customer data, or credentials in this
  repository, its history, screenshots, logs, prompts, or generated artifacts.

