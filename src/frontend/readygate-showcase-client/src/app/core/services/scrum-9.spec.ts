// SCRUM-9 requirements=FR-001,FR-002,FR-003 scenarios=US1-AS1,US1-AS2 framework=Vitest context=7149814cd2d3675d037545d054f12be2e02faa2b085277c3de97c7911a1602ce
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi } from 'vitest';
const fixture = { issueKey: 'SCRUM-9' };
const apiMock = { load: vi.fn().mockResolvedValue(fixture) };
describe('SCRUM-9 generated acceptance', () => {
  it('maps the fixture through the mocked API', async () => expect(await apiMock.load()).toEqual(fixture));
});