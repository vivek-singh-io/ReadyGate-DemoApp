# RGE-13: Create a support ticket from the queue page

As support agent with create_tickets permission, I want raise a support ticket from the queue page, so that a customer call becomes a tracked ticket without leaving the product.

- Spec Kit source: `specs/rge-13/spec.md`
- Spec hash: `48465f685488a0ab8082a1584365c0a3b7e7b76360a8f14eff9eb720fc59fb4b`
- Repository revision: `148c85a89845637d8738829125b24dc4a6139ef2`
- Generation context: `8292b268b9a2c64e9082d8644661173389df117a0281c7db5bd7814e45ee7f8d`

## Requirements

- `FR-001` — Show a Raise a ticket action on the flagged-tickets page only for an agent holding create_tickets.
- `FR-002` — Require subject, customer, priority and description; assignee is optional.
- `FR-003` — Resolve the customer to an existing customer record and reject a name that does not match.
- `FR-004` — Reject a subject longer than 120 characters and a description longer than 4000 characters.
- `FR-005` — Default priority to Medium and fix the initial status to Open.
- `FR-006` — Return every validation failure in one HTTP 400 response rather than the first one found.
- `FR-007` — Preserve everything the agent typed when a submission is rejected.
- `FR-008` — Move keyboard focus to the first field in error after a rejected submission.
- `FR-009` — Persist the created ticket with the authenticated agent as its creator.
- `FR-010` — Return to the flagged-tickets queue with the newly created ticket selected.
- `FR-011` — Reject a create request from an agent without create_tickets with HTTP 403 and persist nothing.

## Out of scope

- File attachments on a support ticket.
- Ticket templates and saved drafts.
- Editing a ticket after it has been created.
- Notifying the customer that a ticket was raised.

## Acceptance scenarios

- `US1-AS1` — Show a Raise a ticket action on the flagged-tickets page only for an agent holding create_tickets.
- `US1-AS10` — Require subject, customer, priority and description; assignee is optional.
- `US1-AS11` — Resolve the customer to an existing customer record and reject a name that does not match.
- `US1-AS2` — Reject a subject longer than 120 characters and a description longer than 4000 characters.
- `US1-AS3` — Default priority to Medium and fix the initial status to Open.
- `US1-AS4` — Return every validation failure in one HTTP 400 response rather than the first one found.
- `US1-AS5` — Preserve everything the agent typed when a submission is rejected.
- `US1-AS6` — Move keyboard focus to the first field in error after a rejected submission.
- `US1-AS7` — Persist the created ticket with the authenticated agent as its creator.
- `US1-AS8` — Return to the flagged-tickets queue with the newly created ticket selected.
- `US1-AS9` — Reject a create request from an agent without create_tickets with HTTP 403 and persist nothing.
