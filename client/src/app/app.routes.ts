import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule, ActivatedRoute } from '@angular/router';

import { PageTitleComponent } from './app.component';
import { ProductsComponent } from './products/products.component';
import { AddProductComponent } from './add-product/add-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
import { OrdersComponent } from './orders/orders.component';
import { AddOrderComponent } from './add-order/add-order.component';
import { EditOrderComponent } from './edit-order/edit-order.component';
import { AddOrderDetailComponent } from './add-order-detail/add-order-detail.component';
import { LoginComponent } from './login/login.component';
import { UsersComponent } from './users/users.component';
import { RolesComponent } from './roles/roles.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { CreateRoleComponent } from './create-role/create-role.component';

import { UnauthorizedComponent } from './unauthorized.component';
import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
import { AdministratorGuard } from './administrator.guard';

export const routes: Routes = [
  {
    path: 'products',
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: ProductsComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Products'
        }
      }
    ]
  },
  {
    path: 'products',
    canActivate: [AuthGuard],
    component: ProductsComponent,
    outlet: 'popup',
    data: {
      title: 'Products'
    }
  },
  {
    path: 'add-product',
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: AddProductComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Product'
        }
      }
    ]
  },
  {
    path: 'add-product',
    canActivate: [AuthGuard],
    component: AddProductComponent,
    outlet: 'popup',
    data: {
      title: 'Add Product'
    }
  },
  {
    path: 'edit-product',
    children: [
      {
        path: ':Id',
        canActivate: [AuthGuard],
        component: EditProductComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Product'
        }
      }
    ]
  },
  {
    path: 'edit-product/:Id',
    canActivate: [AuthGuard],
    component: EditProductComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Product'
    }
  },
  {
    path: 'orders',
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: OrdersComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Orders'
        }
      }
    ]
  },
  {
    path: 'orders',
    canActivate: [AuthGuard],
    component: OrdersComponent,
    outlet: 'popup',
    data: {
      title: 'Orders'
    }
  },
  {
    path: 'add-order',
    children: [
      {
        path: '',
        canActivate: [AuthGuard],
        component: AddOrderComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Order'
        }
      }
    ]
  },
  {
    path: 'add-order',
    canActivate: [AuthGuard],
    component: AddOrderComponent,
    outlet: 'popup',
    data: {
      title: 'Add Order'
    }
  },
  {
    path: 'edit-order',
    children: [
      {
        path: ':Id',
        canActivate: [AuthGuard],
        component: EditOrderComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Edit Order'
        }
      }
    ]
  },
  {
    path: 'edit-order/:Id',
    canActivate: [AuthGuard],
    component: EditOrderComponent,
    outlet: 'popup',
    data: {
      title: 'Edit Order'
    }
  },
  {
    path: 'add-order-detail',
    children: [
      {
        path: ':OrderId',
        canActivate: [AuthGuard],
        component: AddOrderDetailComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Add Order Detail'
        }
      }
    ]
  },
  {
    path: 'add-order-detail/:OrderId',
    canActivate: [AuthGuard],
    component: AddOrderDetailComponent,
    outlet: 'popup',
    data: {
      title: 'Add Order Detail'
    }
  },
  {
    path: 'login',
    children: [
      {
        path: '',
        component: LoginComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Login'
        }
      }
    ]
  },
  {
    path: 'login',
    component: LoginComponent,
    outlet: 'popup',
    data: {
      title: 'Login'
    }
  },
  {
    path: 'users',
    children: [
      {
        path: '',
        canActivate: [AdministratorGuard],
        component: UsersComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Users'
        }
      }
    ]
  },
  {
    path: 'users',
    canActivate: [AdministratorGuard],
    component: UsersComponent,
    outlet: 'popup',
    data: {
      title: 'Users'
    }
  },
  {
    path: 'roles',
    children: [
      {
        path: '',
        canActivate: [AdministratorGuard],
        component: RolesComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Roles'
        }
      }
    ]
  },
  {
    path: 'roles',
    canActivate: [AdministratorGuard],
    component: RolesComponent,
    outlet: 'popup',
    data: {
      title: 'Roles'
    }
  },
  {
    path: 'register-user',
    children: [
      {
        path: '',
        component: RegisterUserComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Register User'
        }
      }
    ]
  },
  {
    path: 'register-user',
    component: RegisterUserComponent,
    outlet: 'popup',
    data: {
      title: 'Register User'
    }
  },
  {
    path: 'create-role',
    children: [
      {
        path: '',
        canActivate: [AdministratorGuard],
        component: CreateRoleComponent
      },
      {
        path: '',
        component: PageTitleComponent,
        outlet: 'title',
        data: {
          title: 'Create Role'
        }
      }
    ]
  },
  {
    path: 'create-role',
    canActivate: [AdministratorGuard],
    component: CreateRoleComponent,
    outlet: 'popup',
    data: {
      title: 'Create Role'
    }
  },
  {
    path: 'unauthorized',
    children: [
      { path: '', component: UnauthorizedComponent } ,
      { path: '', component: PageTitleComponent, outlet: 'title', data: { title: 'Unauthorized' } },
    ]
  },
  { path: '', redirectTo: '/products', pathMatch: 'full' }
];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
