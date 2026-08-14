@SCRUM-7 @readygate @readiness @block-intent
Feature: Gate an underspecified access-request approval change
  ReadyGate must prevent delivery when the ticket does not define the controls needed
  to safely approve or reject an access request.

  Background:
    Given the ReadyGate connection can read Jira project "SCRUM"
    And the target repository is "vivek-singh-io/ReadyGate-DemoApp"
    And the repository revision is pinned for the run

  Scenario: Identify the missing approval controls
    Given Jira issue "SCRUM-7" requests dashboard approval and rejection
    When the configured readiness pipeline evaluates the ticket and repository evidence
    Then the findings should identify a missing authorization boundary
    And the findings should identify undefined concurrent or duplicate actions
    And the findings should identify undefined rejection behavior
    And the findings should identify incomplete notification behavior

  Scenario: Prevent development delivery for the Block-intent fixture
    Given the completed run for Jira issue "SCRUM-7" has verdict "Block"
    When a user views delivery actions
    Then branch preparation should not be available
    And no development branch should be created

  Scenario: Record calibration without rewriting fixture intent
    Given Jira issue "SCRUM-7" is registered as the canonical Block-intent fixture
    And a live model returns a verdict other than "Block"
    When the team reviews the run
    Then the actual verdict and score should remain visible in the ledger
    And the expected Block intent should remain documented for calibration
    And the run should not be presented as a successful Block demonstration
