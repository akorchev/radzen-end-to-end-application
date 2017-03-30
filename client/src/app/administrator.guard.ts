import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import { SecurityService } from './security.service';

@Injectable()
export class AdministratorGuard implements CanActivate {
  constructor(private security: SecurityService) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return this.security.canActivateRole('Administrator', state);
  }
}

