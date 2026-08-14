# Xray Cucumber exchange scenarios

These `.feature` files are import-ready, business-readable Cucumber scenarios for the three canonical
ReadyGate hackathon tickets.

- `SCRUM-7`: Block-intent access-request scenario.
- `SCRUM-8`: Conditional PII-export scenario.
- `SCRUM-9`: Pass ticket-filter scenario.

Importing a feature creates or updates test definitions according to the team's Xray workflow. It does
not prove execution. Attach run evidence only after the corresponding ReadyGate and application checks
actually complete.

The scenarios intentionally avoid credentials, real customer data, provider payloads, and environment
details. Keep Jira-key tags intact so imported tests remain traceable to the hackathon requirements.
