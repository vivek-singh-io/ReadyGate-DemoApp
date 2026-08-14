# ReadyGate DemoApp Fixture Manifest

**Repository:** `vivek-singh-io/ReadyGate-DemoApp`  
**Purpose:** External repository fixture inspected by ReadyGate during the hackathon  
**Data classification:** Fictional demonstration data only  
**Production guidance:** No. Intentional gaps documented here must not be copied into production.

> This manifest is a verification map, not proof. ReadyGate must inspect a pinned revision and cite
> exact source lines before confirming a repository claim. An expected path is not verified evidence
> until it exists on that revision.

## 1. Technology baseline

| Area | Declared value | Evidence | State |
| --- | --- | --- | --- |
| Frontend | Angular 22.1, TypeScript 6, npm | `src/frontend/readygate-showcase-client/package.json` | Verified in manifest |
| Backend | .NET 10 minimal API | `src/backend/ReadyGate.Showcase.Api/ReadyGate.Showcase.Api.csproj` | Verified locally |
| Data | SQLite with EF Core migrations | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Persistence` | Verified locally |
| Frontend tests | Vitest with Angular TestBed | `*.spec.ts` under the Angular app | 6/6 passed locally |
| Backend/database tests | xUnit | `tests/backend/ReadyGate.Showcase.Api.Tests` | 7/7 passed locally |
| QA exchange | Xray-compatible Cucumber/Gherkin | `tests/xray/*.feature` | Present on this branch |

## 2. Stable evidence map

The following are the contract paths for the completed demo fixture. The owner must compare this table
with the pinned revision before recording it as verified.

| Evidence family | Expected path | Important symbol or behavior | Current classification |
| --- | --- | --- | --- |
| Ticket list | `src/frontend/readygate-showcase-client/src/app/features/tickets/ticket-list.component.ts` | `TicketListComponent` | Verified locally |
| View permission | `src/frontend/readygate-showcase-client/src/app/core/guards/view-tickets.guard.ts` | `ViewTicketsGuard` | Verified locally |
| Empty state | `src/frontend/readygate-showcase-client/src/app/shared/empty-state/empty-state.component.ts` | `EmptyStateComponent` | Expected source fixture |
| Ticket API client | `src/frontend/readygate-showcase-client/src/app/core/services/ticket-api.service.ts` | filtered query and export calls | Verified locally |
| Ticket query API | `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketEndpoints.cs` | status/priority query | Verified locally; export section is never a precedent |
| Export API | `src/backend/ReadyGate.Showcase.Api/Endpoints/TicketEndpoints.cs` | `POST /api/tickets/export` | Intentional-gap fixture |
| Export service | `src/backend/ReadyGate.Showcase.Api/Services/CsvExportService.cs` | raw CSV mapping | Intentional-gap fixture; never a precedent |
| Query service | `src/backend/ReadyGate.Showcase.Api/Services/TicketQueryService.cs` | filtered ticket query | Verified locally |
| Authorization | `src/backend/ReadyGate.Showcase.Api/Authorization/DemoPermissionService.cs` | explicit demo permissions | Verified locally |
| Access requests | `src/backend/ReadyGate.Showcase.Api/Endpoints/AccessRequestEndpoints.cs` | read/list footprint only | Verified underspecified footprint |
| Seed data | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Seed/SeedData.cs` | deterministic fictional records | Verified locally |
| Backend tests | `tests/backend/ReadyGate.Showcase.Api.Tests` | endpoint/service/database examples | 7/7 passed locally |
| Database migrations | `src/backend/ReadyGate.Showcase.Api/Infrastructure/Persistence/Migrations` | reviewed EF migrations | Verified locally |
| Xray scenarios | `tests/xray` | Jira-key-tagged Gherkin scenarios | Present on this branch |

## 3. Canonical evidence scenarios

### SCRUM-7 - Block intent

**Ticket:** Approve or reject access requests from the dashboard

**Intended verdict:** Block

**Delivery rule:** No development branch.

The repository may expose an access-request read/list footprint so ReadyGate can distinguish an
underspecified change from a nonexistent feature. The ticket intentionally omits authorization
boundaries, concurrency and duplicate-action handling, rejection-reason behavior, and complete
notification behavior.

`SCRUM-7` remains the Block-intent calibration case even if a live model run produces a different
verdict. Record the actual verdict separately; do not rewrite the intent after seeing the result.

### SCRUM-8 - Conditional PII export

**Ticket:** Export selected flagged tickets to CSV

**Expected initial verdict:** Conditional

**Delivery rule:** No development branch while Conditional.

Required working baseline:

- `POST /api/tickets/export` accepts selected ticket IDs.
- Fictional seeded tickets are returned as CSV.
- Customer name, email, phone, and resolution notes are exported.
- The frontend can select tickets and request the export.

Deliberate gaps ReadyGate must identify:

| Finding | Expected repository evidence | Fixture state |
| --- | --- | --- |
| No server-side `export_tickets` check | Export endpoint/service has no enforced export policy | Intentionally insecure |
| Raw email and phone exported | CSV mapping writes fictional values without masking | Intentionally insecure |
| No export audit entry | No allowed/denied audit write exists | Intentionally incomplete |
| No 500-ticket limit | Request path has no maximum-selection guard | Intentionally incomplete |
| Empty selection undefined | No explicit empty-selection result exists | Intentionally incomplete |

After the Jira ticket explicitly scopes permission enforcement, masking, audit, validation, and tests,
ReadyGate may treat those missing components as new work. That does not make the insecure baseline a
safe precedent.

### SCRUM-9 - Pass filter

**Ticket:** Filter ticket list by status and priority

**Expected verdict:** Pass

**Delivery rule:** A human may prepare the Jira-key draft branch after the run completes.

ReadyGate should verify the ticket list, `view_tickets` behavior, shared empty state, status/priority
query parameters, 250 ms debounce, filter preservation, unauthorized-user behavior, and stable tests.

## 4. Xray exchange inventory

| Jira key | Feature file | Purpose |
| --- | --- | --- |
| `SCRUM-7` | `tests/xray/scrum-7-access-request-readiness.feature` | Block and no-branch governance |
| `SCRUM-8` | `tests/xray/scrum-8-pii-export-readiness.feature` | Conditional gap detection and remediation contract |
| `SCRUM-9` | `tests/xray/scrum-9-ticket-filter-readiness.feature` | Pass evidence and human-approved delivery |

These files are import-ready Gherkin definitions. They are not execution results, and their presence
does not claim that Xray import or automated execution has passed.

## 5. Style-reference policy

ReadyGate requires at least two safe same-layer references before generating code or tests for an
artifact family. Candidate paths and disqualifying rules are maintained in
`docs/reference-implementations.md`.

The following can never be approved precedents:

- the export mapping in `TicketEndpoints.cs` and `CsvExportService.cs` while the deliberate gaps remain;
- generated `.readygate/<JIRA-key>` artifacts;
- build output, dependency directories, secrets, or environment files;
- unrelated layers or files absent from the pinned revision.

If two safe references cannot be verified, ReadyGate must omit that artifact family and clearly report
the omission rather than inventing a house style.

## 6. Validation commands and evidence record

Run from a clean checkout after dependencies are restored:

```powershell
Set-Location src/backend
dotnet test ReadyGate.Showcase.slnx

Set-Location ../frontend/readygate-showcase-client
npm ci
npm test -- --watch=false
npm run build
```

No test or CI result is asserted by this document. Record verified evidence here only after execution:

| Check | Workflow/run | Commit | Result |
| --- | --- | --- | --- |
| .NET build/tests | Local validation | Working tree | 7/7 passed; Release build clean |
| Angular build/tests | Local validation | Working tree | 6/6 passed; production build passed |
| Secret/`.env` check | Pending | Pending | Not recorded |

## 7. Live ReadyGate evidence

The following identifiers are recorded for traceability and must be reconfirmed against the ReadyGate
ledger and provider UIs before the Round 2 recording.

| Verification | Recorded evidence | Interpretation |
| --- | --- | --- |
| SCRUM-7 run | `ad134974-cda8-4528-b76c-1cfeab761844` | Actual Conditional/83; differs from Block intent and needs calibration |
| SCRUM-8 run | `3de816e7-288b-4928-a28a-ffcc92883d0e` | Conditional/88 |
| SCRUM-9 run | `db19e675-50be-48a0-943e-e0120180116e` | Pass/80 |
| SCRUM-9 draft branch | `SCRUM-9-ticket-draft-filter-ticket-list-by-status-and-priority` | Human-approved branch preparation path |
| SCRUM-9 generated manifest | `.readygate/SCRUM-9/artifact-manifest.json` | Verify contents on the generated branch |

Jira comments were published for the three canonical runs through the confirmed ReadyGate action.
The generated SCRUM-9 branch initially contained only the specification and manifest because safe
same-layer references were not yet available. This documentation/fixture work exists to make artifact
eligibility explicit; it does not retroactively claim that code, SQL, or tests were generated.

## 8. Repository governance checklist

- [ ] Repository is public and the default branch is `main`.
- [ ] `GajapathiKS` has the agreed collaborator access.
- [ ] Pull requests and required approval protect `main` where account capabilities allow.
- [ ] Force pushes and deletion of `main` are blocked.
- [ ] Wiki is disabled and merged branches are deleted automatically.
- [ ] Subsequent branches and commits contain actual Jira keys.
- [ ] GitHub Actions are green on the pinned demo revision.
- [ ] No `.env`, token, credential, real customer data, or proprietary code exists in Git history.
- [ ] Runtime access is repository-restricted, stored outside source/chat, and revoked after submission.

## 9. Round 2 freeze record

Complete only after source fixtures, validation, and evidence review are finished.

```text
Repository: vivek-singh-io/ReadyGate-DemoApp
Default branch: main
Pinned commit SHA: pending
Fixture manifest reviewed by: pending
Reviewed at UTC: pending
Block-intent Jira key: SCRUM-7
Conditional Jira key: SCRUM-8
Pass Jira key: SCRUM-9
Reference PR: pending
CI run: pending
```
