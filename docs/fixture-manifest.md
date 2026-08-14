# ReadyGate DemoApp Fixture Manifest

**Repository:** `vivek-singh-io/ReadyGate-DemoApp`  
**Purpose:** External repository fixture inspected by ReadyGate during the hackathon  
**Data classification:** Fictional demonstration data only  
**Production guidance:** No. Intentional gaps documented here must not be copied into production.

> This file is a verification map, not proof. ReadyGate must inspect the pinned repository revision
> and cite the real source files and symbols before confirming any claim.

## 1. Verified technology baseline

| Area                   | Expected value                   | Owner verification              |
| ---------------------- | -------------------------------- | ------------------------------- |
| Frontend               | Angular 22.1, TypeScript 6, npm  | TODO                            |
| Backend                | .NET 9 minimal API               | TODO                            |
| Data                   | SQLite with EF Core migrations   | TODO after persistence is added |
| Frontend tests         | Vitest with Angular TestBed      | TODO                            |
| Backend/database tests | xUnit                            | TODO                            |
| QA exchange            | Xray-compatible Cucumber/Gherkin | TODO                            |

## 2. Stable evidence paths

Vivek must replace `TODO` with the final path and important symbol after implementation. Once frozen
for the Round 2 demo, these paths must remain stable.

| Evidence family     | Intended path                                                               | Important symbol            | Status |
| ------------------- | --------------------------------------------------------------------------- | --------------------------- | ------ |
| Ticket list         | `src/frontend/readygate-showcase-client/src/app/tickets/ticket-list/`       | `TicketListComponent`       | TODO   |
| View permission     | `src/frontend/readygate-showcase-client/src/app/core/permissions/`          | `ViewTicketsGuard`          | TODO   |
| Empty state         | `src/frontend/readygate-showcase-client/src/app/shared/empty-state/`        | `EmptyStateComponent`       | TODO   |
| Ticket query API    | `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketQueryEndpoints.cs`      | status/priority query       | TODO   |
| Export API          | `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketExportEndpoints.cs`     | `POST /api/tickets/export`  | TODO   |
| Export service      | `src/backend/ReadyGate.Showcase.Api/Services/TicketExportService.cs`        | CSV generation              | TODO   |
| Query service       | `src/backend/ReadyGate.Showcase.Api/Services/TicketQueryService.cs`         | filtered ticket query       | TODO   |
| Authorization       | `src/backend/ReadyGate.Showcase.Api/Authorization/`                         | permission policies         | TODO   |
| Access requests     | `src/backend/ReadyGate.Showcase.Api/AccessRequests/`                        | query/list footprint        | TODO   |
| Seed data           | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Seed/`                   | fictional tickets/customers | TODO   |
| Frontend tests      | `src/frontend/readygate-showcase-client/src/app/**/*.spec.ts`               | representative tests        | TODO   |
| Backend tests       | `tests/backend/`                                                            | endpoint/service tests      | TODO   |
| Database migrations | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Persistence/Migrations/` | EF migrations               | TODO   |

## 3. Fictional seed data

Document the final deterministic seed IDs here. Do not use real names or contact details.

| Record type     | IDs/count | Purpose                              | Status |
| --------------- | --------- | ------------------------------------ | ------ |
| Customers       | TODO      | CSV masking and export evidence      | TODO   |
| Tickets         | TODO      | status/priority filtering and export | TODO   |
| Access requests | TODO      | Block-scenario repository footprint  | TODO   |

## 4. Block scenario fixture

**Jira key:** `TODO-after-Pratibha-creates-ticket`  
**Expected verdict:** Block  
**Ticket:** Approve or reject access requests from the dashboard

Repository evidence that must exist:

- An access-request model or service contract.
- A query endpoint or list footprint.
- Enough real code for ReadyGate to distinguish an underspecified change from a nonexistent area.

Intentionally absent from the ticket/fixture:

- Authorization boundaries.
- Concurrency and duplicate-action behavior.
- Rejection-reason behavior.
- Complete notification behavior.

ReadyGate must create no development branch for this scenario.

## 5. Conditional PII-export fixture

**Jira key:** `TODO-after-Pratibha-creates-ticket`  
**Expected initial verdict:** Conditional  
**Ticket:** Export selected flagged tickets to CSV

Required working baseline:

- `POST /api/tickets/export` accepts selected ticket IDs.
- The service retrieves fictional seeded tickets.
- CSV output contains customer name, email, phone, and resolution notes.
- The frontend can select tickets and request the export.
- The application still builds and its baseline tests pass.

Intentional gaps ReadyGate must discover:

| Expected finding                      | Evidence expectation                                | Status |
| ------------------------------------- | --------------------------------------------------- | ------ |
| No server-side `export_tickets` check | Export endpoint/service contains no enforced policy | TODO   |
| Raw email and phone exported          | CSV mapping writes unmasked fictional values        | TODO   |
| No export audit entry                 | No success/denied audit write exists                | TODO   |
| No 500-ticket limit                   | Request path has no maximum-selection validation    | TODO   |
| Empty selection undefined             | No explicit validation/result is implemented        | TODO   |

These are deliberate hackathon fixtures. Do not label the export implementation production-safe.
The first Conditional run must not expose branch creation.

After Pratibha revises the Jira ticket with permission, masking, audit, validation, and test
requirements, ReadyGate should treat the missing components as explicitly scoped new work and may
produce Pass when all evidence and policy requirements are satisfied.

## 6. Pass filter fixture

**Jira key:** `TODO-after-Pratibha-creates-ticket`  
**Expected verdict:** Pass  
**Ticket:** Filter ticket list by status and priority

ReadyGate must be able to verify:

- `TicketListComponent` or the final equivalent.
- `ViewTicketsGuard` or the final equivalent permission symbol.
- `EmptyStateComponent` or the final equivalent.
- Ticket-query API parameters for status and priority.
- A 250 ms debounce convention or comparable established pattern.
- Existing unauthorized-user behavior.
- Stable frontend and backend test examples.

## 7. Approved style-reference inventory

Provide at least two non-generated, non-fixture-gap examples for every family that ReadyGate will
generate. All paths must refer to the pinned demo revision.

| Family/layer               | Reference path 1 | Reference path 2 | Why representative                          | Status |
| -------------------------- | ---------------- | ---------------- | ------------------------------------------- | ------ |
| Angular feature component  | TODO             | TODO             | Structure, naming, DI, async/error style    | TODO   |
| Angular unit test          | TODO             | TODO             | TestBed, mocks, fixtures, assertions        | TODO   |
| .NET endpoint              | TODO             | TODO             | Routing, validation, result mapping         | TODO   |
| .NET service               | TODO             | TODO             | DI, async, errors, logging, data access     | TODO   |
| .NET unit/integration test | TODO             | TODO             | Fixtures, fakes, assertions                 | TODO   |
| EF migration/database test | TODO             | TODO             | Naming, transaction and rollback convention | TODO   |

ReadyGate must reject generated files, intentional insecure gaps, build outputs, unrelated layers,
or stale revisions as style precedents.

## 8. Reference implementation

Create this after Pratibha provides a dedicated reference Jira key.

| Field                                 | Value                                     |
| ------------------------------------- | ----------------------------------------- |
| Reference Jira key                    | TODO                                      |
| Summary                               | Authorized JSON export with audit logging |
| Branch                                | TODOâ€”must include the Jira key            |
| Pull request URL                      | TODO                                      |
| Merge commit SHA                      | TODO                                      |
| Permission evidence paths             | TODO                                      |
| Transformation/masking evidence paths | TODO                                      |
| Audit evidence paths                  | TODO                                      |
| Validation evidence paths             | TODO                                      |
| Frontend/backend test paths           | TODO                                      |

The reference must be merged, use fictional data, and remain compatible with current architecture.
ReadyGate may reuse its patterns but must never let it override the current Jira requirements.

## 9. Expected validation commands

Run from a clean checkout after dependencies are restored:

```powershell
Set-Location src/backend
dotnet test ReadyGate.Showcase.slnx

Set-Location ../frontend/readygate-showcase-client
npm ci
npm test -- --watch=false
npm run build
```

Record the GitHub Actions workflow URL, run ID, commit SHA, and test totals below:

| Check               | Workflow/run | Commit | Result |
| ------------------- | ------------ | ------ | ------ |
| .NET build/tests    | TODO         | TODO   | TODO   |
| Angular build/tests | TODO         | TODO   | TODO   |
| Secret/`.env` check | TODO         | TODO   | TODO   |

## 10. Jira and ReadyGate integration evidence

| Verification                                                   | Evidence                 | Status |
| -------------------------------------------------------------- | ------------------------ | ------ |
| Jira recognizes issue-key branch/commit                        | TODO URL                 | TODO   |
| ReadyGate reads pinned repository tree                         | TODO run ID              | TODO   |
| Block scenario creates no branch                               | TODO run ID              | TODO   |
| Conditional run finds all five intentional gaps                | TODO run ID              | TODO   |
| Pass enables human-approved branch preparation                 | TODO run/branch URL      | TODO   |
| Duplicate branch request returns existing branch               | TODO run/action ID       | TODO   |
| Generated manifest contains Spec Kit and repository provenance | TODO path                | TODO   |
| Generated SQL/migration is not executed                        | TODO validation evidence | TODO   |

## 11. Repository governance and security checklist

- [ ] Repository is public and default branch is `main`.
- [ ] `GajapathiKS` has the agreed collaborator access.
- [ ] Pull request and one approval are required for `main`.
- [ ] Force pushes and deletion of `main` are blocked.
- [ ] Wiki is disabled.
- [ ] Merged branches are deleted automatically.
- [ ] Subsequent branches and commits contain actual Jira keys.
- [ ] GitHub Actions are green on the pinned demo revision.
- [ ] No `.env`, token, credential, real customer data, or proprietary code exists in Git history.
- [ ] Fine-grained runtime access is restricted to this repository and configured outside source/chat.
- [ ] Runtime access expires and is revoked after the submission window.

## 12. Freeze record for Round 2

Complete this only when all fixture work is ready for recording.

```text
Repository: vivek-singh-io/ReadyGate-DemoApp
Default branch: main
Pinned commit SHA: TODO
Fixture manifest reviewed by: TODO
Reviewed at UTC: TODO
Block Jira key: TODO
Conditional Jira key: TODO
Pass Jira key: TODO
Reference Jira key: TODO
Reference PR: TODO
CI run: TODO
```

