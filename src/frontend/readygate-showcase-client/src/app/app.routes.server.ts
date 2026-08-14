import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: '**',
    // Ticket data is authenticated runtime state; do not invoke the API during build-time prerendering.
    renderMode: RenderMode.Client
  }
];
