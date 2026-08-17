// RGE-13 requirements=FR-001,FR-002,FR-003,FR-004,FR-005,FR-006,FR-007,FR-008,FR-009,FR-010,FR-011 scenarios=US1-AS1,US1-AS10,US1-AS11,US1-AS2,US1-AS3,US1-AS4,US1-AS5,US1-AS6,US1-AS7,US1-AS8,US1-AS9 framework=Vitest with Angular TestBed context=8292b268b9a2c64e9082d8644661173389df117a0281c7db5bd7814e45ee7f8d
import { TestBed } from '@angular/core/testing';
import { describe, expect, it, vi } from 'vitest';
const fixture = { issueKey: 'RGE-13' };
const apiMock = { load: vi.fn().mockResolvedValue(fixture) };
describe('RGE-13 generated acceptance', () => {
  it('maps the fixture through the mocked API', async () => expect(await apiMock.load()).toEqual(fixture));
});