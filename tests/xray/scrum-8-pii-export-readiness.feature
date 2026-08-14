@SCRUM-8 @readygate @readiness @conditional @pii
Feature: Gate a PII export until security controls are specified
  ReadyGate must expose repository discrepancies in the initial CSV export and must not
  allow delivery while the result remains Conditional.

  Background:
    Given the ReadyGate connection can read Jira project "SCRUM"
    And the target repository is "vivek-singh-io/ReadyGate-DemoApp"
    And the repository revision is pinned for the run

  Scenario Outline: Detect each deliberate export gap
    Given Jira issue "SCRUM-8" requests export of selected flagged tickets
    And the initial export fixture is present at the pinned revision
    When the configured readiness pipeline evaluates the ticket and repository evidence
    Then the run should report the discrepancy "<discrepancy>"

    Examples:
      | discrepancy |
      | missing server-side export_tickets authorization |
      | unmasked fictional email and phone data |
      | missing audit logging for allowed and denied attempts |
      | missing 500-ticket request limit |
      | undefined empty-selection behavior |

  Scenario: Keep delivery unavailable for a Conditional result
    Given the completed run for Jira issue "SCRUM-8" has verdict "Conditional"
    When a user views delivery actions
    Then branch preparation should not be available
    And the result should recommend ticket changes supported by evidence

  Scenario: Accept explicitly scoped remediation as new work
    Given Jira issue "SCRUM-8" explicitly requires server-side permission enforcement
    And it requires PII masking, audit logging, empty-selection validation, and a 500-ticket limit
    And it requires frontend, backend, database, and QA tests where applicable
    When ReadyGate reevaluates the revised ticket against the same repository revision
    Then absent implementation components may be classified as explicitly scoped new work
    But the insecure export fixture should not be selected as a coding-style precedent
