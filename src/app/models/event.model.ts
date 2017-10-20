export class EventChangeMoney {
  constructor(
    public type: string,
    public description: string,
    public date: string,
    public category: number,
    public amount: number,
    public id?: number,
    public catName?: string
  ) {}
}
