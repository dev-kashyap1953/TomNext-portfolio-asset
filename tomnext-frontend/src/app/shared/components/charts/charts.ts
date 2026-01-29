import { Component, Input, SimpleChanges } from '@angular/core';
import { HighchartsChartComponent } from 'highcharts-angular';
import Highcharts from 'highcharts';
import { ChartConfig } from '../../utils/types';

@Component({
  selector: 'app-charts',
  imports: [HighchartsChartComponent],
  templateUrl: './charts.html',
  styleUrl: './charts.scss',
})
export class Charts {
@Input({ required: true }) config!: ChartConfig;

  options: Highcharts.Options = {};
  updateFlag = false;

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['config']) {
      this.buildOptions();
      this.updateFlag = true;
    }
  }

  private buildOptions(): void {
    const { type, title, xAxisCategories, series } = this.config;

    this.options = {
      chart: { type: this.config.type },
      title: this.config.title ? { text: this.config.title } : undefined,
      xAxis: this.config.type === 'pie' ? undefined : { categories: this.config.xAxisCategories },
      yAxis: this.config.type === 'pie' ? undefined : {},
      legend: {
        enabled: false,
      },
      series: this.config.series.map((s) => ({
        ...s,
        type: this.config.type,
      })) as Highcharts.SeriesOptionsType[],
      credits: { enabled: false },
    };
  }
}
