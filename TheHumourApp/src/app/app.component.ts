import { Component } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ViewChild } from '@angular/core';
import { Humour } from './humour.model';
import { HumourService } from './humour.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  term: string = '';
  displayedColumns: string[] = ['id', 'joke'];
  data: Humour[] = [];
  dataSource = new MatTableDataSource<Humour>([]);
  pageEvent!: PageEvent;

  constructor(private humourService: HumourService) {

  }
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit() {
    this.humourService.search(1, 5, this.term).subscribe(
      d => {
        this.data = d.results.map(x => new Humour(x.id, x.text)); 
        this.paginator.pageIndex = d.current_page;
        this.paginator.pageSize = d.limit;
        this.paginator.length = d.total_pages;
        this.dataSource = new MatTableDataSource<Humour>(this.data)
      }
    );
  }

  public getAllJokes() {
    this.humourService.search(1, this.paginator.pageSize, this.term).subscribe(
      d => {
        this.data = d.results.map(x => new Humour(x.id, x.text)); 
        this.paginator.pageIndex = d.current_page;
        this.paginator.pageSize = d.limit;
        this.paginator.length = d.total_pages;
        this.dataSource = new MatTableDataSource<Humour>(this.data)
      }
    );
  }
  public getJokes(event?: PageEvent) {
    this.humourService.search(this.paginator.pageIndex, this.paginator.pageSize, this.term).subscribe(
      d => {
        this.data = d.results.map(x => new Humour(x.id, x.text)); 
        this.dataSource = new MatTableDataSource<Humour>(this.data)
        this.paginator.pageIndex = d.current_page;
        this.paginator.length = d.total_pages;
        this.paginator.pageSize = d.limit;
        // this.paginator.lastPage = d.total_pages;
      }
    );
  }
}
