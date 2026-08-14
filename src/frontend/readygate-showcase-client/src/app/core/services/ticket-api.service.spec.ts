import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { TicketApiService } from './ticket-api.service';

describe('TicketApiService', () => {
  let service: TicketApiService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TicketApiService, provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(TicketApiService);
    http = TestBed.inject(HttpTestingController);
  });

  afterEach(() => http.verify());

  it('sends only selected ticket filters to the list API', () => {
    service.getTickets({ status: 'Open', priority: 'High' }).subscribe();

    const request = http.expectOne(
      (candidate) =>
        candidate.url === '/api/tickets' &&
        candidate.params.get('status') === 'Open' &&
        candidate.params.get('priority') === 'High'
    );
    expect(request.request.method).toBe('GET');
    request.flush([]);
  });

  it('posts selected IDs and requests a CSV blob', () => {
    service.exportTickets([17, 23]).subscribe();

    const request = http.expectOne('/api/tickets/export');
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual({ ticketIds: [17, 23] });
    expect(request.request.responseType).toBe('blob');
    request.flush(new Blob(['reference,subject']));
  });
});
