import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Assests } from '../utils/assests';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environments';

@Injectable({
  providedIn: 'root',
})
export class AssestsService {
   constructor(private httpClient: HttpClient){}

  getAllAssests(): Observable<Assests[]> {
    return this.httpClient.get<Assests[]>(environment.baseUrl + 'Assests/GetAllAssets');
  }
}
