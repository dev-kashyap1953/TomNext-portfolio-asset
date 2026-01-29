export interface NewsItem {
  total: number;
  articles: Article[];
}

export interface Article {
  title: string;
  link: string;
  publishedAt: string;
  source: string;
  description: string;
  sourceUrl: string;
}