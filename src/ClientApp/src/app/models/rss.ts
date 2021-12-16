export class FeedViewModel
{
  constructor(
    public FeedValue?: Object, 
    public SortValue?: string, 
    public SyndicationItems?: Object[],
    public PageInfo?: Object
    ) { }
}