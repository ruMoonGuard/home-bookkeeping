export class EventChangeMoney {
  constructor(
    public type: string,
    public description: string,
    public creationDate: string,
    public categoryId: number,
    public amount: number,
    public id?: number,
    public catName?: string
  ) {}
}
