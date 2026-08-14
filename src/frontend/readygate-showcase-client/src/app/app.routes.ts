import { Routes } from '@angular/router';
import { viewTicketsGuard } from './core/guards/view-tickets.guard';

export const routes: Routes = [
  {
    path: 'forbidden',
    loadComponent: () =>
      import('./shared/forbidden/forbidden.component').then(
        (component) => component.ForbiddenComponent
      )
  },
  {
    path: 'tickets',
    canActivate: [viewTicketsGuard],
    loadComponent: () =>
      import('./features/tickets/ticket-list.component').then(
        (component) => component.TicketListComponent
      )
  },
  { path: '', pathMatch: 'full', redirectTo: 'tickets' },
  { path: '**', redirectTo: 'tickets' }
];
