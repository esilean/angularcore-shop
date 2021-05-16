import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

import { Pagination } from '../shared/models/pagination';
import { ProductBrand } from '../shared/models/product-brand';
import { ProductType } from '../shared/models/product-type';
import { ShopParams } from '../shared/models/shop-params';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private baseUrl: string = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http
      .get<Pagination>(this.baseUrl + 'products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getBrands() {
    return this.http.get<ProductBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<ProductType[]>(this.baseUrl + 'products/types');
  }
}
