import { Injectable } from '@angular/core';
import { Http, Headers, URLSearchParams, QueryEncoder } from '@angular/http';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment';
import { PlusQueryEncoder } from './plus-query-encoder';

import { SecurityService } from './security.service';
import * as models from './test.model';

@Injectable()
export class TestService {
  basePath = environment.test;

  constructor(private http: Http, private auth: SecurityService) {
  }

  getOrderDetails(filter?: string, top?: number, skip?: number, orderby?: string, count?: boolean) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    const search = new URLSearchParams('', new PlusQueryEncoder());

    if (filter) {
      search.set('$filter', filter);
    }

    if (top != null) {
      search.set('$top', top.toString());
    }

    if (skip != null) {
      search.set('$skip', skip.toString());
    }

    if (orderby) {
      search.set('$orderby', orderby);
    }

    if (count != null) {
      search.set('$count', count.toString());
    }

    return this.http.request(`${this.basePath}/OrderDetails`, {
      method: 'get',
      search,
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  createOrderDetail(orderDetail?: models.OrderDetail) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/OrderDetails`, {
      method: 'post',
      headers,
      body: JSON.stringify(orderDetail)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return orderDetail;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  deleteOrderDetail(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/OrderDetails(${id})`, {
      method: 'delete',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return {};
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getOrderDetailById(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/OrderDetails(${id})`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  updateOrderDetail(id?: number, orderDetail?: models.OrderDetail) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/OrderDetails(${id})`, {
      method: 'patch',
      headers,
      body: JSON.stringify(orderDetail)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return orderDetail;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getOrders(filter?: string, top?: number, skip?: number, orderby?: string, count?: boolean) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    const search = new URLSearchParams('', new PlusQueryEncoder());

    if (filter) {
      search.set('$filter', filter);
    }

    if (top != null) {
      search.set('$top', top.toString());
    }

    if (skip != null) {
      search.set('$skip', skip.toString());
    }

    if (orderby) {
      search.set('$orderby', orderby);
    }

    if (count != null) {
      search.set('$count', count.toString());
    }

    return this.http.request(`${this.basePath}/Orders`, {
      method: 'get',
      search,
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  createOrder(order?: models.Order) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/Orders`, {
      method: 'post',
      headers,
      body: JSON.stringify(order)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return order;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  deleteOrder(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Orders(${id})`, {
      method: 'delete',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return {};
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getOrderById(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Orders(${id})`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  updateOrder(id?: number, order?: models.Order) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/Orders(${id})`, {
      method: 'patch',
      headers,
      body: JSON.stringify(order)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return order;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getProducts(filter?: string, top?: number, skip?: number, orderby?: string, count?: boolean) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    const search = new URLSearchParams('', new PlusQueryEncoder());

    if (filter) {
      search.set('$filter', filter);
    }

    if (top != null) {
      search.set('$top', top.toString());
    }

    if (skip != null) {
      search.set('$skip', skip.toString());
    }

    if (orderby) {
      search.set('$orderby', orderby);
    }

    if (count != null) {
      search.set('$count', count.toString());
    }

    return this.http.request(`${this.basePath}/Products`, {
      method: 'get',
      search,
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  createProduct(product?: models.Product) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/Products`, {
      method: 'post',
      headers,
      body: JSON.stringify(product)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return product;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  deleteProduct(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'delete',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return {};
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  getProductById(id?: number) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'get',
      headers
    })
    .map(response => {
      switch (response.status) {
        case 200:
          return response.json();
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }

  updateProduct(id?: number, product?: models.Product) {
    const headers = new Headers();

    headers.append('Accept', 'application/json');
    headers.append('Authorization', `Bearer ${this.auth.token}`);
    headers.append('Content-Type', 'application/json');

    return this.http.request(`${this.basePath}/Products(${id})`, {
      method: 'patch',
      headers,
      body: JSON.stringify(product)
    })
    .map(response => {
      switch (response.status) {
        case 204:
          return product;
      }
    })
    .catch(response => {
      return Observable.throw(response.json());
    })
    .toPromise();
  }
}
