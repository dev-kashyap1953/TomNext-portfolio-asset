import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from '../../environments/environments';
import { TopIndicators } from '../models/top-indicators.model';
import { NewsFeedByFiltered } from '../models/news-feed-by-filtered';
import { NewsItem } from '../shared/utils/news-items';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private http: HttpClient) { }

  httpoptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      'Accept': 'application/json'
    })
  }

  getNewsFeed(newsFeedByFiltered?: NewsFeedByFiltered): Observable<NewsItem> {
    return this.http.post<NewsItem>(environment.baseUrl + 'News/GetNewsFromRss', newsFeedByFiltered, this.httpoptions);
  }

  getTopIndicators(): Observable<TopIndicators> {
    return this.http.get<TopIndicators>(environment.baseUrl + 'News/GetTopIndicators');
  }
}