import { Component, Injector } from '@angular/core';
import { RegisterUserGenerated } from './register-user-generated.component';

@Component({
  selector: 'register-user',
  templateUrl: './register-user.component.html'
})
export class RegisterUserComponent extends RegisterUserGenerated {
  constructor(injector: Injector) {
    super(injector);
  }
}
