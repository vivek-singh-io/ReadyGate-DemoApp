@SCRUM-9 @readygate @readiness @pass @filters
Feature: Verify and deliver the ticket-list filter change
  ReadyGate must confirm the ticket and repository evidence for status and priority filters
  before a human can prepare a development branch.

  Background:
    Given the ReadyGate connection can read Jira project "SCRUM"
    And the target repository is "vivek-singh-io/ReadyGate-DemoApp"
    And the repository revision is pinned for the run

  Scenario: Verify the ticket-list filter evidence
    Given Jira issue "SCRUM-9" requires status and priority filters
    When the configured readiness pipeline evaluates the ticket and repository evidence
    Then it should cite the ticket-list component
    And it should cite the existing view-tickets permission behavior
    And it should cite the shared empty-state component
    And it should cite status and priority API query handling
    And it should cite a 250 millisecond debounce convention
    And it should cite representative frontend and backend tests

  Scenario: Preserve filter behavior in the user interface
    Given a support agent has "view_tickets" permission
    When the agent filters tickets by status and priority
    Then the results should update without a full-page reload
    And the selected filters should survive ticket detail navigation
    And the empty-state component should appear when no tickets match

  Scenario: Prepare a branch only after confirmation
    Given the completed run for Jira issue "SCRUM-9" has verdict "Pass"
    And the user has reviewed the generated artifact manifest
    When the user confirms branch preparation
    Then ReadyGate should prepare branch "SCRUM-9-ticket-draft-filter-ticket-list-by-status-and-priority"
    And the generated artifacts should be placed under ".readygate/SCRUM-9"

  Scenario: Return the existing branch for a duplicate request
    Given the SCRUM-9 draft branch has already been prepared for the run
    When the user confirms branch preparation again
    Then ReadyGate should return the existing branch
    And it should not create a second branch
