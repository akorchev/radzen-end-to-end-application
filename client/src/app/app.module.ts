import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RadzenModule } from '@radzen/angular';

import { AppRoutes } from './app.routes';
import { AppComponent, PageTitleComponent } from './app.component';
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

import { TestService } from './test.service';
import { SecurityService } from './security.service';
import { AuthGuard } from './auth.guard';
import { UnauthorizedComponent } from './unauthorized.component';
import { AdministratorGuard } from './administrator.guard';

@NgModule({
  declarations: [
    ProductsComponent,
    AddProductComponent,
    EditProductComponent,
    OrdersComponent,
    AddOrderComponent,
    EditOrderComponent,
    AddOrderDetailComponent,
    LoginComponent,
    UsersComponent,
    RolesComponent,
    RegisterUserComponent,
    CreateRoleComponent,
    UnauthorizedComponent,
    AppComponent,
    PageTitleComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RadzenModule,
    AppRoutes
  ],
  providers: [
    AdministratorGuard,
    SecurityService,
    AuthGuard,
    TestService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
