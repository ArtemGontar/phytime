export class Recommendation{
  constructor(
    public id?: string,
    public title?: string,
    public summary?: string,
    public publishDate?: string,
    public author?: string,
    public tags?: string[],
    public level?: string,
    public link?: string
  ){}
}