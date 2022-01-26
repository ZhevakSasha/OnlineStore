import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {PageEvent} from '@angular/material/paginator';
import { PageChangeService } from './page-change.service';

@Component({
  selector: 'app-paginator',
  templateUrl: './paginator.component.html',
  styleUrls: ['./paginator.component.css']
})
export class PaginatorComponent implements OnInit {
  @Input() length;
  pageSizeOptions: number[] = [5, 10, 25, 50];
  pageSize= 10;

  // MatPaginator Output
  pageEvent: PageEvent;

  @Output()
  paginatorEmit: EventEmitter<any> = new EventEmitter<any>();

  constructor(private readonly pageChangeService: PageChangeService) { }

  ngOnInit(): void {
    this.pageChangeService.changePageOptions(0, this.pageSize);
  }

  onPaginateChange(event) {
    this.pageChangeService.changePageOptions(event.pageIndex, event.pageSize);
    this.paginatorEmit.emit(); 
  }

}
