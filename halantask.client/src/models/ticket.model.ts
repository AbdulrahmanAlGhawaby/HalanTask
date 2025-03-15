export interface Ticket {
  id: number;
  phoneNumber: string;
  governorate: string;
  city: string;
  district: string;
  createdAt: Date;
  isHandled: boolean;
}

export interface PaginatedList<T> {
  items: T[];
  pageIndex: number;
  totalPages: number;
  totalCount: number;
}
