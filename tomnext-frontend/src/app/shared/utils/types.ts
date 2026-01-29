import type * as Highcharts from 'highcharts';

export type ChartType = 'line' | 'bar' | 'column' | 'pie';

export interface ChartSeries {
  name: string;
  type?: ChartType;
  data: number[] | Highcharts.PointOptionsObject[];
  color?: string;
}

export interface ChartConfig {
  type: ChartType;
  title?: string;
  xAxisCategories?: string[];
  series: ChartSeries[];
}
