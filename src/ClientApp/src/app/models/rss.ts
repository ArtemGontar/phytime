export class Feed
{
  constructor(
    public feedValue?: Object, 
    public sortValue?: string, 
    public syndicationItems?: Object[],
    public pageInfo?: Object
    ) { }
}