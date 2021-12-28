import { PageInfo } from "./pageInfo";

export class Feed
{
  constructor(
    public feedValue?: Object, 
    public sortValue?: string, 
    public syndicationItems?: SyndicationItem[],
    public pageInfo?: PageInfo
    ) { }
}

export class SyndicationItem{
  constructor(
    public title?: string,
    public summary?: string,
    public publishDate?: string,
    public links?: string
  ){}
}