import { CommonModule, DOCUMENT } from '@angular/common';
import { ChangeDetectionStrategy, Component, DestroyRef, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { debounceTime, distinctUntilChanged, finalize, Subject } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import {
  TicketPriority,
  TicketQuery,
  TicketStatus,
  TicketSummary,
  ticketPriorities,
  ticketStatuses
} from '../../core/models/ticket.models';
import { PermissionService } from '../../core/services/permission.service';
import { TicketApiService } from '../../core/services/ticket-api.service';
import { EmptyStateComponent } from '../../shared/empty-state/empty-state.component';

interface FilterState {
  readonly status: TicketStatus | '';
  readonly priority: TicketPriority | '';
}

@Component({
  selector: 'app-ticket-list',
  imports: [CommonModule, FormsModule, EmptyStateComponent],
  templateUrl: './ticket-list.component.html',
  styleUrl: './ticket-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TicketListComponent {
  private readonly api = inject(TicketApiService);
  private readonly permissions = inject(PermissionService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly document = inject(DOCUMENT);
  private readonly destroyRef = inject(DestroyRef);
  private readonly filterChanges = new Subject<FilterState>();

  readonly statuses = ticketStatuses;
  readonly priorities = ticketPriorities;
  readonly tickets = signal<readonly TicketSummary[]>([]);
  readonly selectedIds = signal<ReadonlySet<number>>(new Set());
  readonly loading = signal(true);
  readonly exporting = signal(false);
  readonly error = signal<string | null>(null);
  readonly exportError = signal<string | null>(null);
  readonly canExport = computed(() => this.permissions.has('export_tickets'));
  readonly selectedCount = computed(() => this.selectedIds().size);
  readonly allVisibleSelected = computed(
    () => this.tickets().length > 0 && this.tickets().every((ticket) => this.selectedIds().has(ticket.id))
  );

  status: TicketStatus | '' = this.readStatus(this.route.snapshot.queryParamMap.get('status'));
  priority: TicketPriority | '' = this.readPriority(this.route.snapshot.queryParamMap.get('priority'));

  constructor() {
    this.filterChanges
      .pipe(
        debounceTime(250),
        distinctUntilChanged(
          (left, right) => left.status === right.status && left.priority === right.priority
        ),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((filters) => {
        void this.router.navigate([], {
          relativeTo: this.route,
          queryParams: {
            status: filters.status || null,
            priority: filters.priority || null
          },
          queryParamsHandling: 'merge',
          replaceUrl: true
        });
        this.loadTickets(filters);
      });

    this.loadTickets({ status: this.status, priority: this.priority });
  }

  onFilterChange(): void {
    this.filterChanges.next({ status: this.status, priority: this.priority });
  }

  retry(): void {
    this.loadTickets({ status: this.status, priority: this.priority });
  }

  toggleTicket(ticketId: number, checked: boolean): void {
    this.selectedIds.update((current) => {
      const next = new Set(current);
      checked ? next.add(ticketId) : next.delete(ticketId);
      return next;
    });
  }

  toggleAllVisible(checked: boolean): void {
    this.selectedIds.update((current) => {
      const next = new Set(current);
      for (const ticket of this.tickets()) {
        checked ? next.add(ticket.id) : next.delete(ticket.id);
      }
      return next;
    });
  }

  exportSelected(): void {
    if (!this.canExport() || this.selectedCount() === 0 || this.exporting()) return;

    this.exporting.set(true);
    this.exportError.set(null);
    this.api
      .exportTickets([...this.selectedIds()])
      .pipe(finalize(() => this.exporting.set(false)))
      .subscribe({
        next: (file) => this.download(file),
        error: () => this.exportError.set('The selected tickets could not be exported. Try again.')
      });
  }

  trackTicket(_: number, ticket: TicketSummary): number {
    return ticket.id;
  }

  private loadTickets(filters: FilterState): void {
    const query: TicketQuery = {
      ...(filters.status ? { status: filters.status } : {}),
      ...(filters.priority ? { priority: filters.priority } : {})
    };

    this.loading.set(true);
    this.error.set(null);
    this.api
      .getTickets(query)
      .pipe(finalize(() => this.loading.set(false)))
      .subscribe({
        next: (tickets) => {
          this.tickets.set(tickets);
          const visibleIds = new Set(tickets.map((ticket) => ticket.id));
          this.selectedIds.update((current) => new Set([...current].filter((id) => visibleIds.has(id))));
        },
        error: () => {
          this.tickets.set([]);
          this.error.set('Tickets are temporarily unavailable. Check the API and try again.');
        }
      });
  }

  private download(file: Blob): void {
    const url = URL.createObjectURL(file);
    const link = this.document.createElement('a');
    link.href = url;
    link.download = `flagged-tickets-${new Date().toISOString().slice(0, 10)}.csv`;
    link.click();
    URL.revokeObjectURL(url);
  }

  private readStatus(value: string | null): TicketStatus | '' {
    return ticketStatuses.includes(value as TicketStatus) ? (value as TicketStatus) : '';
  }

  private readPriority(value: string | null): TicketPriority | '' {
    return ticketPriorities.includes(value as TicketPriority) ? (value as TicketPriority) : '';
  }
}
