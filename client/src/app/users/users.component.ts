import { Component, Injector } from '@angular/core';
import { UsersGenerated } from './users-generated.component';

@Component({
  selector: 'users',
  templateUrl: './users.component.html'
})
export class UsersComponent extends UsersGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
