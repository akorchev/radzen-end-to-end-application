import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { SecurityService } from './security.service';

@Component({
  selector: 'page-title',
  template: '{{ (route.data | async).title }}'
})
export class PageTitleComponent {
  constructor(private route: ActivatedRoute) {
  }
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  menuActive = true;
  mobileMenuActive = false;
  popupRoute: ActivatedRoute;
  profileActive = false;
  hamburgerMenuActive = false;

  constructor (public security: SecurityService,public router: Router, private route: ActivatedRoute, private location: Location) {
   router.events.subscribe(() => {
      this.popupRoute = this.route.children.find(route => route.outlet == 'popup');
    });
  }

  closePopup() {
    this.location.back();
  }

  onLogout(event) {
    event.preventDefault();

    this.security.logout();
  }

  onHamburgerMenuClick(event) {
    event.preventDefault();

    this.hamburgerMenuActive = !this.hamburgerMenuActive;
  }

  onProfileClick(event) {
    event.preventDefault();

    this.profileActive = !this.profileActive;
  }

  onToggleMenuClick(event) {
    event.preventDefault();

    if(this.isDesktop()) {
      this.menuActive = !this.menuActive;
    } else {
      this.mobileMenuActive = !this.mobileMenuActive;
    }
  }

  isDesktop() {
    return window.innerWidth > 1024;
  }
}
