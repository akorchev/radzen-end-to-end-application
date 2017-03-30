import { Component, Injector } from '@angular/core';
import { RolesGenerated } from './roles-generated.component';

@Component({
  selector: 'roles',
  templateUrl: './roles.component.html'
})
export class RolesComponent extends RolesGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
