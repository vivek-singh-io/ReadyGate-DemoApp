# ReadyGate DemoApp Reference Implementations

This document tells ReadyGate and human reviewers which repository files may establish local coding
style. It prevents generated artifacts from copying an intentional hackathon vulnerability or guessing
patterns that the repository does not contain.

## Approval rule

An artifact family is eligible for generation only when ReadyGate can verify at least two safe,
non-generated, same-layer references on the pinned revision. Each reference must demonstrate the
relevant structure, naming, dependency injection, asynchronous behavior, error handling, logging,
authorization, data-access, fixture, mocking, or assertion convention.

The paths below are **candidate contracts** until they exist and are reviewed on the pinned revision.
Documentation alone is never proof that a reference exists.

## Candidate reference inventory

| Artifact family | Candidate reference 1 | Candidate reference 2 | Required evidence before approval |
| --- | --- | --- | --- |
| Angular feature/component | `src/frontend/readygate-showcase-client/src/app/features/tickets/ticket-list.component.ts` | `src/frontend/readygate-showcase-client/src/app/shared/empty-state/empty-state.component.ts` | Standalone component structure, typed state, DI and error/empty behavior |
| Angular service/guard | `src/frontend/readygate-showcase-client/src/app/core/services/ticket-api.service.ts` | `src/frontend/readygate-showcase-client/src/app/core/guards/view-tickets.guard.ts` | Typed HTTP calls, permission flow, no embedded credentials |
| Angular unit test | `src/frontend/readygate-showcase-client/src/app/features/tickets/ticket-list.component.spec.ts` | `src/frontend/readygate-showcase-client/src/app/core/services/ticket-api.service.spec.ts` | Angular TestBed, typed stubs, fixtures and Vitest assertions |
| .NET endpoint | `src/backend/ReadyGate.Showcase.Api/Endpoints/AccessRequestEndpoints.cs` | `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketEndpoints.cs` query mapping only | Thin mapping, permission checks and cancellation |
| .NET service | `src/backend/ReadyGate.Showcase.Api/Services/TicketQueryService.cs` | `src/backend/ReadyGate.Showcase.Api/Services/AccessRequestQueryService.cs` | Constructor DI, async flow and data access |
| .NET test | `tests/backend/ReadyGate.Showcase.Api.Tests/TicketEndpointTests.cs` | `tests/backend/ReadyGate.Showcase.Api.Tests/TicketQueryServiceTests.cs` | Web/application fixtures, fictional data and xUnit assertions |
| EF/database | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Persistence/Migrations/202608140001_InitialShowcaseSchema.cs` | `tests/backend/ReadyGate.Showcase.Api.Tests/TicketDatabaseTests.cs` | EF naming, isolated SQLite and relationship assertions |
| Xray Cucumber | `tests/xray/scrum-8-pii-export-readiness.feature` | `tests/xray/scrum-9-ticket-filter-readiness.feature` | Jira tags, business-readable Given/When/Then, no provider secrets |

## Explicitly excluded references

Do not use these files or categories as style precedents:

- the export mapping in `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketEndpoints.cs` while it omits enforced
  `export_tickets` authorization;
- `src/backend/ReadyGate.Showcase.Api/Services/CsvExportService.cs` while it exports raw PII and
  omits audit/limit handling;
- any file generated under `.readygate/<JIRA-key>`;
- `bin`, `obj`, `node_modules`, `dist`, generated files, or vendored dependencies;
- environment files, credentials, logs, screenshots, and recorded provider payloads;
- a path that is not present on the exact revision inspected by ReadyGate.

## Reference Jira ticket support

The UI may accept an optional reference Jira ticket for a similar completed implementation. That ticket
is supporting context only. It must not override the current ticket, repository profile, architecture,
security controls, or observed repository conventions.

For the hackathon, no dedicated merged reference Jira implementation is recorded yet. The team must
provide all of the following before enabling it as an approved precedent:

- Jira key and summary;
- merged branch, pull request URL, and merge commit SHA;
- exact authorization, transformation/masking, audit, and validation paths;
- exact frontend/backend/database test paths; and
- confirmation that all data is fictional and current validation is green.

## Human review checklist

- [ ] Candidate file exists on the pinned revision.
- [ ] Candidate is in the same layer and artifact family as the requested output.
- [ ] Candidate is not generated or part of an intentional insecure fixture.
- [ ] Dependencies and architecture agree with `.readygate/readygate.yaml`.
- [ ] Two independent safe references are available.
- [ ] Required security and authorization behavior is visible in the references or explicit ticket scope.
- [ ] Validation commands are safe for the isolated generated workspace.

When any item fails, ReadyGate must mark the affected artifact family ineligible and tell the user what
evidence is missing. It must not silently produce fewer outputs than the user confirmed.
