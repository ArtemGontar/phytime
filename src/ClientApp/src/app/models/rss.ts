export class Feed
{
  constructor(
    public feedValue?: Object, 
    public sortValue?: string, 
    public syndicationItems?: SyndicationItem[],
    public pageInfo?: PageInfo
    ) { }
}

export class PageInfo
{
  constructor(
    public pageNumber?: number, 
    public pageSize?: number, 
    public totalItems?: number,
    public totalPages?: number
    ) { }
}

export class SyndicationItem
{
  constructor(
    public id?: string,
    public summary?: string, 
    public title?: string, 
    public publishDate?: string
    ) { }
}