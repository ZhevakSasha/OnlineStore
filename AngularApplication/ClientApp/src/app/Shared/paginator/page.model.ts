export class PageModel {
    constructor(
      public TotalCount: number,
      public PageSize: number,
      public CurrentPage: number,
      public TotalPages: number,
      public HesNext: boolean,
      public HesPrevious: boolean) {
    }
  }