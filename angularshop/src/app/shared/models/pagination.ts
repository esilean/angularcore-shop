import { Product } from "./product";

export interface Pagination {
    pageIndex: number;
    pageSize: number;
    totalPages: number;
    totalItemsPage: number;
    totalItems: number;
    data: Product[];
}
