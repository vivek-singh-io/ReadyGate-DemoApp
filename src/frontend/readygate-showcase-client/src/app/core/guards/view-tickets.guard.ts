import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { PermissionService } from '../services/permission.service';

export const viewTicketsGuard: CanActivateFn = () => {
  const permissions = inject(PermissionService);
  return permissions.has('view_tickets') ? true : inject(Router).parseUrl('/forbidden');
};
