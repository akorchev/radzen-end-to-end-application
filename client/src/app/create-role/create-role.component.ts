import { Component, Injector } from '@angular/core';
import { CreateRoleGenerated } from './create-role-generated.component';

@Component({
  selector: 'create-role',
  templateUrl: './create-role.component.html'
})
export class CreateRoleComponent extends CreateRoleGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
