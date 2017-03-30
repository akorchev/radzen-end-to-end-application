export interface Order {
  Id: number;
  OrderDate: string;
  UserName: string;
}

export interface OrderDetail {
  Id: number;
  OrderId: number;
  ProductId: number;
  Quantity: number;
}

export interface OrderDetailResponse {
  value: Array<OrderDetail>;
}

export interface OrderResponse {
  value: Array<Order>;
}

export interface Product {
  Id: number;
  ProductName: string;
  ProductPrice: number;
}

export interface ProductResponse {
  value: Array<Product>;
}
