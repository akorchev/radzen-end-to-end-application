import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams } from '@angular/http';
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';

import { environment } from '../environments/environment';

@Injectable()
export class SecurityService {
  basePath = environment.securityUrl;

  constructor(private router: Router, private http: Http) {
  }

  isLoggedIn() {
    return tokenNotExpired();
  }

  canActivateRole(role: string, state: RouterStateSnapshot): boolean {
    if (this.canActivate(state)) {
      const currentRole = this.role;

      if (currentRole == role || currentRole == 'Administrator') {
        return true;
      } else {
        this.router.navigateByUrl('/unauthorized');
      }
    }

    return false;
  }

  isInRole(role: string): boolean {
    if (!this.isLoggedIn()) {
      return false;
    }

    const currentRole = this.role;

    return currentRole == role || currentRole == 'Administrator';
  }

  get profile() {
    if (!this.isLoggedIn()) {
      return null;
    }

    const jwt = new JwtHelper();

    return jwt.decodeToken(this.token);
  }

  get role() {
    return this.profile['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  }

  get name() {
    return this.profile['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
  }

  get token() {
    return localStorage.getItem('id_token');
  }

  logout() {
    localStorage.removeItem('id_token');

    location.reload();
  }

  canActivate(state: RouterStateSnapshot) {
    if (this.isLoggedIn()) {
      return true;
    } else {
      this.router.navigate([{ outlets: { popup: null } } ])
          .then(() => this.router.navigate(['/login'], { queryParams: { redirectUrl: state.url } }));

      return false;
    }
  }

  login(username: string, password: string) {
    return this.http.post(`${this.basePath}/login`, { username, password })
        .map((result: any) => {
          if (result.status == 200) {
            const { access_token } = result.json();

            localStorage.setItem('id_token', access_token);

            const { redirectUrl = '/' } = this.router.routerState.snapshot.root.queryParams;

            this.router.navigateByUrl(redirectUrl);
          }
        })
        .catch(response => {
          return Observable.throw(response.json());
        })
        .toPromise();
  }

  registerUser(email: string, password: string) {
    return this.http.post(`${this.basePath}/register`, { email, password })
      .map((result: any) => {
        if (result.status == 200) {
          const { access_token } = result.json();

          localStorage.setItem('id_token', access_token);

          this.router.navigateByUrl('/');
        }
      })
      .catch(response => {
        return Observable.throw(response.json());
      })
      .toPromise();
  }

  getUsers() {
    return this.get('users');
  }

  getRoles() {
    return this.get('roles');
  }

  private get(entity: string) {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.token}`);

    return this.http.request(`${this.basePath}/${entity}`, {
      method: 'GET',
      headers
    })
    .map(response => {
      if (response.status == 200) {
        return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  createRole(role: any) {
    const headers = new Headers();
    headers.append('Accept', 'application/json');
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', `Bearer ${this.token}`);

    return this.http.request(`${this.basePath}/roles`, {
      method: 'POST',
      body: JSON.stringify(role),
      headers
    })
    .map(response => {
      if (response.status == 201) {
        return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  deleteRole(id: string) {
    return this.delete('roles', id);
  }

  deleteUser(id: string) {
    return this.delete('users', id);
  }

  private delete(entity: string, id: string) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.token}`);

    return this.http.request(`${this.basePath}/${entity}/${id}`, {
      method: 'DELETE',
      headers
    })
    .map(response => {
      if (response.status == 204) {
        return {};
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }
}
