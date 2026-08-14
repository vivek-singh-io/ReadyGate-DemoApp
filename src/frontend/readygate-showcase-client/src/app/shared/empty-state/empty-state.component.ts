import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  selector: 'app-empty-state',
  template: `
    <section class="empty-state" role="status">
      <span class="empty-icon" aria-hidden="true">?</span>
      <h2>{{ title() }}</h2>
      <p>{{ message() }}</p>
    </section>
  `,
  styles: `
    .empty-state { padding: 3rem 1rem; text-align: center; color: #5f7182; }
    .empty-icon { display: grid; width: 2.75rem; height: 2.75rem; margin: 0 auto 0.75rem;
      place-items: center; border-radius: 50%; color: #0f766e; background: #ccfbf1; font-weight: 800; }
    h2 { margin: 0; color: #20384d; font-size: 1.125rem; }
    p { max-width: 34rem; margin: 0.5rem auto 0; }
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmptyStateComponent {
  readonly title = input.required<string>();
  readonly message = input.required<string>();
}
