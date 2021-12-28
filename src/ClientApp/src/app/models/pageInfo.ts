export class PageInfo{
  constructor(
        public pageNumber?: number,
        public pageSize?: number,
        public totalItems?: number,
        public totalPages?: number) { }
}