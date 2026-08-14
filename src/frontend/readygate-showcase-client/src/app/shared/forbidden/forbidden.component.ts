import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'app-forbidden',
  template: `
    <section class="forbidden" role="alert">
      <p class="eyebrow">Access restricted</p>
      <h1>Ticket access is not available</h1>
      <p>Ask an administrator for the <code>view_tickets</code> permission.</p>
    </section>
  `,
  styles: `
    .forbidden { max-width: 42rem; margin: 4rem auto; padding: 2rem; border: 1px solid #fecaca;
      border-radius: 1rem; background: #fff; text-align: center; }
    .eyebrow { color: #b91c1c; font-weight: 800; text-transform: uppercase; letter-spacing: .08em; }
    h1 { color: #0f2942; }
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ForbiddenComponent {}
