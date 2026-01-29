import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { ScreenItem } from '../../models/screen-data.model';
import { ScreenDataService } from '../../services/screen-data.service';
import { NewsService } from '../../services/news.service';
import { CommonModule } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Charts } from '../../shared/components/charts/charts';
import { ChartConfig } from '../../shared/utils/types';
import { MatTabsModule } from '@angular/material/tabs';
import { Assests } from '../../shared/utils/assests';
import { AssestsService } from '../../shared/services/assests-service';
import { MatSelectModule } from '@angular/material/select';
import { TopIndicators } from '../../models/top-indicators.model';
import { NewsFeedByFiltered } from '../../models/news-feed-by-filtered';
import { Article, NewsItem } from '../../shared/utils/news-items';
import { ToastrModule, ToastrService } from 'ngx-toastr';

export interface NewsAnalytics {
  articlesLast7Days: number;
  highRelevanceCount: number;
  coveragePercentage: number;
  topSectors: number;
}

@Component({
  selector: 'app-screen-layout',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    Charts,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatBadgeModule,
    MatTooltipModule,
    MatCardModule,
    MatListModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatSlideToggleModule,
    MatTabsModule,
    MatSelectModule,
    ToastrModule
  ],
  templateUrl: './screen-layout.component.html',
  styleUrls: ['./screen-layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class ScreenLayoutComponent implements OnInit, AfterViewInit {
  items: ScreenItem[] = [];
  newsItems!: NewsItem;
  isNewsLoading: boolean = true;
  isNewsError: boolean = false;
  sortBy: 'recent' | 'relevant' = 'recent';
  newsFilters = {
    asset: '',
    geo: '',
    sector: '',
    unread: false
  };
  filteredNews: Article[] = [];

  // Top Indicators
  analytics: NewsAnalytics = {
    articlesLast7Days: 0,
    highRelevanceCount: 0,
    coveragePercentage: 0,
    topSectors: 0,
  };

  tableColumns: string[] = ['title', 'assets', 'relevance', 'sector', 'source', 'date'];
  selectedNewsItem: Article | null = null;
  isDetailDrawerOpen: boolean = false;

  articles: ScreenItem[] = [];
  selectedAsset: string = '';
  selectedGeo: string = '';
  selectedSector: string = '';
  allAssets: Assests[] = [];
  articlesOverTimeConfig: ChartConfig = { type: 'line', series: [] };
  articlesPerAssetConfig: ChartConfig = { type: 'bar', series: [] };
  articlesByCategoryConfig: ChartConfig = { type: 'pie', series: [] };

  newsFeedByFilter: NewsFeedByFiltered = { 
    assets: this.selectedAsset, geo: this.selectedGeo, sectors: this.selectedSector
   };
  dataSource = new MatTableDataSource<Article>();

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private screenDataService: ScreenDataService,
    private newsService: NewsService,
    private cdr: ChangeDetectorRef,
    private assetsService: AssestsService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.screenDataService.getItems().subscribe((data) => {
      this.articles = data;
      this.updateCharts();
    });

    this.getAllAssests();
    this.loadNews();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  loadNews(): void {
    this.isNewsLoading = true;
    this.newsService.getNewsFeed(this.newsFeedByFilter)?.subscribe({
      next: (news) => {
        this.newsItems = news;
        this.isNewsLoading = false;
        this.dataSource.data = news.articles;
        this.calculateAnalytics();
        this.getFilteredNews();
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.isNewsError = true;
        this.isNewsLoading = false;
        this.toastrService.error("Error news: ", err.message);
        this.cdr.detectChanges();
      }
    });
  }

  getFilteredNews(): Article[] {
    let filtered = this.newsItems?.articles ? [...this.newsItems.articles] : [];

    // Apply filters
    if (this.newsFilters.asset) {
      filtered = filtered.filter(article =>
        article.title.toLowerCase().includes(this.newsFilters.asset.toLowerCase()) ||
        article.description.toLowerCase().includes(this.newsFilters.asset.toLowerCase())
      );
    }

    if (this.newsFilters.geo) {
      filtered = filtered.filter(article =>
        article.title.toLowerCase().includes(this.newsFilters.geo.toLowerCase()) ||
        article.description.toLowerCase().includes(this.newsFilters.geo.toLowerCase())
      );
    }

    filtered.sort((a, b) => {
      if (this.sortBy === 'recent') {
        const dateA = new Date(a.publishedAt || 0).getTime();
        const dateB = new Date(b.publishedAt || 0).getTime();
        return dateB - dateA;
      } else {
        return b.title.length - a.title.length;
      }
    });

    this.filteredNews = filtered;
    return filtered;
  }

  onSortChange(sort: 'recent' | 'relevant'): void {
    this.sortBy = sort;
    this.getFilteredNews();
  }

  onNewsFilterChange(filterValue: string): void {
    const value = filterValue?.trim().toLowerCase() || '';
    this.dataSource.filter = value;
  }

  clearNewsFilters(): void {
    this.newsFeedByFilter = {
      assets: '',
      geo: '',
      sectors: ''
    }
    this.selectedAsset = '',
      this.selectedGeo = '';
    this.selectedSector = '';
    this.newsService.getNewsFeed(this.newsFeedByFilter)?.subscribe({
      next: (res: NewsItem) => {
        this.newsItems = res;
        this.filteredNews = res.articles;
        this.isNewsLoading = false;
        this.calculateAnalytics();
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        this.toastrService.error("Error clearNewsFilters: ", err.message);
        this.isNewsError = true;
        this.isNewsLoading = false;
      }
    })
  }

  calculateAnalytics(): void {
    this.newsService.getTopIndicators().subscribe({
      next: (res: TopIndicators) => {
        this.analytics.articlesLast7Days = res.total ?? 0;
        this.analytics.highRelevanceCount = res.highRelevanceCount ?? 0;
        this.analytics.coveragePercentage = res.coverageCount ?? 0;
        this.analytics.topSectors = res.topSecotrs;
        this.cdr.detectChanges();
      },
      error : (err: any) => {
        this.toastrService.error("Error calculateAnalytics: ", err.message);
      }
    })
  }

  openDetailDrawer(news: Article): void {
    this.selectedNewsItem = news;
    this.isDetailDrawerOpen = true;
  }

  closeDetailDrawer(): void {
    this.isDetailDrawerOpen = false;
    this.selectedNewsItem = null;
  }

  markAsRead(article: Article): void {
    console.log('Marking as read:', article.title);
  }

  pinNews(article: Article): void {
    console.log('Pinning news:', article.title);
  }

  getRelevanceLevel(news: Article): string {
    if (news.title.length > 60) return 'High';
    if (news.title.length > 30) return 'Medium';
    return 'Low';
  }

  getRelevanceColor(news: Article): string {
    const level = this.getRelevanceLevel(news);
    switch (level) {
      case 'High': return '#f44336';
      case 'Medium': return '#ff9800';
      case 'Low': return '#4caf50';
      default: return '#9e9e9e';
    }
  }

  onChangeFilters(): void {
    this.isNewsLoading = true;
    this.newsFeedByFilter = {
      assets: this.selectedAsset, geo: this.selectedGeo, sectors: this.selectedSector?.toString()
    }
    this.newsService.getNewsFeed(this.newsFeedByFilter).subscribe({
      next: (res: NewsItem) => {
        this.newsItems = res;
        this.filteredNews = res.articles;
        this.isNewsLoading = false;
        this.calculateAnalytics();
        this.cdr.detectChanges();
      },
      error: (err: any) => {
        this.isNewsError = true;
        this.isNewsLoading = false;
        this.toastrService.error("Error onChangeFilters: ", err.message);
        this.cdr.detectChanges();
      }
    });
  }

  extractAnchorText(html?: string): string {
    if (!html) return '';

    const tempDiv = document.createElement('div');
    tempDiv.innerHTML = html;

    const anchor = tempDiv.querySelector('a');
    return anchor?.textContent?.trim() || '';
  }

  private updateCharts(): void {
    const dates = Array.from(
      new Set(this.articles.map((a) => a.createdAt.toISOString().slice(0, 10))),
    ).sort();
    const countsOverTime = dates.map(
      (d) => this.articles.filter((a) => a.createdAt.toISOString().slice(0, 10) === d).length,
    );

    this.articlesOverTimeConfig = {
      type: 'line',
      title: 'Articles Over Time',
      xAxisCategories: dates,
      series: [{ name: 'Articles', data: countsOverTime, type: 'line', color: '#f7a556' }],
    };

    const titles = this.articles.map((a) => a.title);
    const countsPerAsset = titles.map((t) => this.articles.filter((a) => a.title === t).length);

    this.articlesPerAssetConfig = {
      type: 'bar',
      title: 'Articles per Asset',
      xAxisCategories: titles,
      series: [{ name: 'Articles', data: countsPerAsset, type: 'bar', color: '#f93a5a' }],
    };

    const categories = Array.from(new Set(this.articles.map((a) => a.category)));
    const categoryData = categories.map((cat) => ({
      name: cat,
      y: this.articles.filter((a) => a.category === cat).length,
    }));

    this.articlesByCategoryConfig = {
      type: 'pie',
      title: 'Articles by Category',
      series: [{ name: 'Articles', data: categoryData, type: 'pie' }],
    };
  }

  private getAllAssests(): void {
    this.assetsService.getAllAssests().subscribe({
      next: (res: Assests[]) => {
        this.allAssets = res;
      },
      error: (err: any) =>  {
        this.toastrService.error("Error getAllAssests: ", err.message);
      },
    });
  }
}
