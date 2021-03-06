/*
  This file is automatically generated. Any changes will be overwritten.
  Modify create-role.component.ts instead.
*/
import { Injector, OnInit, OnDestroy } from '@angular/core';
import { NavigationEnd, Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Subscription } from 'rxjs';

import { SecurityService } from '../security.service';

/*
  Component properties set from design-time.
*/
const { components } = require('../../../../meta/pages/create-role.json');

export class CreateRoleGenerated implements OnInit, OnDestroy {
  components = components;
  // Array of messages displayed by the notification component.
  messages = [];

  router: Router;

  route: ActivatedRoute;

  _location: Location;

  subscription: Subscription;

  security: SecurityService;

  parameters: any;

  Security: any;

  ngOnInit() {
    this.subscription = this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd && this instanceof <any>this.route.component) {
        this.parameters = this.route.snapshot.params;

      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }


  form0Submit(event: any) {
    this.security.createRole(event)
    .then(result => {
      this.router.navigate([{ outlets: { popup: null } }]).then(() => this.router.navigate(['roles']));
    }, result => {

    });
  }

  constructor(injector: Injector) {
    this.router = injector.get(Router);

    this._location = injector.get(Location);

    this.route = injector.get(ActivatedRoute);

    this.security = injector.get(SecurityService);
  }
}
