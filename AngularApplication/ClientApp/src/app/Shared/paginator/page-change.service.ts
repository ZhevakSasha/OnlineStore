import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PageChangeService {

    public pageNumber$ = new Subject<number>();
	public pageSize$ = new Subject<number>();

	public changePageOptions(pageNumber: number, pageSize: number) {
   		this.pageNumber$.next(pageNumber); 
		this.pageSize$.next(pageSize); 
	    }

}