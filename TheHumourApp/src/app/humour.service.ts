import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Humour } from './humour.model';
import { Observable, map, of } from 'rxjs';

export interface PagedResult
{
  current_page: number;
  limit: number;
  next_page: number;
  previous_page: number;
  results: { id: number; text: string}[];
  term: string;
  status: number;
  total_jokes: number;
  total_pages: number;
}

@Injectable({
  providedIn: 'root'
})
export class HumourService {

  constructor(private httpClient: HttpClient) { }

  // getHumourById(id: string) : Humour {

  // }

  // getRandomHumour() : Humour {

  // }

  search(page: number, limit: number, term: string): Observable<PagedResult> {
    // return of(ELEMENT_DATA);
    return this.httpClient.get<PagedResult>(`http://localhost:5056/api/humour/search?page=${page}&limit=${limit}&term=${term}`);
  }

}
