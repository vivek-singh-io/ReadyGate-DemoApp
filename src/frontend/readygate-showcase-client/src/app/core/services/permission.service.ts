import { Injectable, signal } from '@angular/core';

export type DemoPermission = 'view_tickets' | 'export_tickets';

@Injectable({ providedIn: 'root' })
export class PermissionService {
  private readonly granted = signal<ReadonlySet<DemoPermission>>(
    new Set<DemoPermission>(['view_tickets', 'export_tickets'])
  );

  has(permission: DemoPermission): boolean {
    return this.granted().has(permission);
  }

  setPermissions(permissions: readonly DemoPermission[]): void {
    this.granted.set(new Set(permissions));
  }
}
