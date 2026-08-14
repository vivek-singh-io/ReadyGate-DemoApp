import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { of } from 'rxjs';
import { TicketSummary } from '../../core/models/ticket.models';
import { PermissionService } from '../../core/services/permission.service';
import { TicketApiService } from '../../core/services/ticket-api.service';
import { TicketListComponent } from './ticket-list.component';

const tickets: readonly TicketSummary[] = [
  {
    id: 17,
    reference: 'TKT-0017',
    subject: 'Cannot access the customer portal',
    customerName: 'Avery Jordan',
    status: 'Open',
    priority: 'High',
    flagged: true,
    updatedAt: '2026-08-14T08:30:00Z'
  }
];

describe('TicketListComponent', () => {
  let fixture: ComponentFixture<TicketListComponent>;
  let component: TicketListComponent;
  let permissions: PermissionService;
  let api: {
    getTickets: ReturnType<typeof vi.fn>;
    exportTickets: ReturnType<typeof vi.fn>;
  };

  beforeEach(async () => {
    api = {
      getTickets: vi.fn().mockReturnValue(of(tickets)),
      exportTickets: vi.fn().mockReturnValue(of(new Blob(['ticket']))),
    };

    await TestBed.configureTestingModule({
      imports: [TicketListComponent],
      providers: [
        provideRouter([]),
        PermissionService,
        { provide: TicketApiService, useValue: api }
      ]
    }).compileComponents();

    permissions = TestBed.inject(PermissionService);
    fixture = TestBed.createComponent(TicketListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('renders API tickets and exports only the selected IDs', () => {
    const click = vi.spyOn(HTMLAnchorElement.prototype, 'click').mockImplementation(() => undefined);
    component.toggleTicket(17, true);
    component.exportSelected();
    fixture.detectChanges();

    const element = fixture.nativeElement as HTMLElement;
    expect(element.textContent).toContain('TKT-0017');
    expect(element.textContent).toContain('Cannot access the customer portal');
    expect(api.exportTickets).toHaveBeenCalledWith([17]);
    click.mockRestore();
  });

  it('hides export controls when the user lacks export permission', () => {
    permissions.setPermissions(['view_tickets']);
    fixture.detectChanges();

    const element = fixture.nativeElement as HTMLElement;
    expect(element.querySelector('.primary-button')).toBeNull();
  });

  it('shows the shared empty state when no tickets match', () => {
    component.tickets.set([]);
    component.loading.set(false);
    fixture.detectChanges();

    expect((fixture.nativeElement as HTMLElement).textContent).toContain('No tickets match');
  });
});
