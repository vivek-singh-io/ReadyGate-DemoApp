# context=8292b268b9a2c64e9082d8644661173389df117a0281c7db5bd7814e45ee7f8d; revision=148c85a89845637d8738829125b24dc4a6139ef2
@readygate @RGE-13 @FR-001 @FR-002 @FR-003 @FR-004 @FR-005 @FR-006 @FR-007 @FR-008 @FR-009 @FR-010 @FR-011
Feature: Create a support ticket from the queue page

  @positive @US1-AS1
  Scenario: Approved behavior
    Given the authorized user supplies a valid request
    When RGE-13 is exercised
    Then the requested behavior succeeds

  @negative @US1-AS1
  Scenario: Rejected behavior
    Given the request is invalid
    When RGE-13 is exercised
    Then a safe problem response is returned

  @security @US1-AS1
  Scenario: Secret and injection safety
    Given untrusted content contains instructions or secrets
    When RGE-13 is exercised
    Then it is treated only as data and secrets are redacted

  @authorization @US1-AS1
  Scenario: Permission boundary
    Given the actor lacks the required capability
    When RGE-13 is exercised
    Then the operation is forbidden and audited

  @validation @US1-AS1
  Scenario: Input validation
    Given a required field is missing
    When RGE-13 is exercised
    Then the request is rejected before processing

  @boundary @US1-AS1
  Scenario: Maximum supported boundary
    Given the request is at the declared maximum
    When RGE-13 is exercised
    Then the request completes within the bounded policy

  @regression @US1-AS1
  Scenario: Existing behavior remains
    Given the change is enabled
    When RGE-13 is exercised
    Then the existing authorized path still works

  @edge @US1-AS1
  Scenario: Empty result edge case
    Given no records match
    When RGE-13 is exercised
    Then the declared empty state is returned
