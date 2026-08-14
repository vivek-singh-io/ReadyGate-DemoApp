import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TicketExportRequest, TicketQuery, TicketSummary } from '../models/ticket.models';

@Injectable({ providedIn: 'root' })
export class TicketApiService {
  private readonly http = inject(HttpClient);
  private readonly endpoint = '/api/tickets';

  getTickets(query: TicketQuery): Observable<readonly TicketSummary[]> {
    let params = new HttpParams();
    if (query.status) params = params.set('status', query.status);
    if (query.priority) params = params.set('priority', query.priority);

    return this.http.get<readonly TicketSummary[]>(this.endpoint, { params });
  }

  exportTickets(ticketIds: readonly number[]): Observable<Blob> {
    const request: TicketExportRequest = { ticketIds };
    return this.http.post(`${this.endpoint}/export`, request, { responseType: 'blob' });
  }
}
