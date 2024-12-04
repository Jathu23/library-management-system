import { Component } from '@angular/core';
import { Color, LegendPosition, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'], // Fixed `styleUrl` to `styleUrls`
})
export class DashboardComponent {
  chartData = [
    { name: 'January', value: 5000 },
    { name: 'February', value: 7200 },
    { name: 'March', value: 6700 },
    { name: 'April', value: 8000 },
    { name: 'May', value: 9000 },
    { name: 'June', value: 8500 },
    { name: 'July', value: 10000 },
    { name: 'August', value: 9500 },
    { name: 'September', value: 7800 },
    { name: 'October', value: 9100 },
    { name: 'November', value: 8800 },
    { name: 'December', value: 9800 }
  ];
  

  view: [number, number] = [1200, 500];

  colorScheme: Color = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'],
    group: ScaleType.Ordinal,
    selectable: true,
    name: 'custom'
  };
   // Correctly using LegendPosition enum value
   legendPosition: LegendPosition = LegendPosition.Right;



   view2: [number, number] = [700, 400]; // Width and height of the chart

  cardData = [
    {
      "name": "Active Users",
      "value": 150
    },
    {
      "name": "Books Borrowed Today",
      "value": 45
    },
    {
      "name": "Books Available",
      "value": 1200
    }
  ];

  colorScheme2 = {
    domain: [ '#A10A28', '#C7B42C', '#AAAAAA'] // Customize the colors
  };

  view3: [number, number] = [700, 400]; // Width and height of the chart

  // Data for the Line Chart
  lineChartData = [
    {
      "name": "year 2023",
      "series": [
        { "name": "January", "value": 50 },
        { "name": "February", "value": 65 },
        { "name": "March", "value": 80 },
        { "name": "April", "value": 55 },
        { "name": "May", "value": 70 },
        { "name": "June", "value": 95 },
        { "name": "July", "value": 85 },
        { "name": "August", "value": 60 },
        { "name": "September", "value": 75 },
        { "name": "October", "value": 90 },
        { "name": "November", "value": 100 },
        { "name": "December", "value": 120 }
      ]
    },
    {
      "name": "year 2024",
      "series": [
          { "name": "January", "value": 0 },
          { "name": "February", "value": 0 },
          { "name": "March", "value": 0 },
          { "name": "April", "value": 0 },
          { "name": "May", "value": 70 },
          { "name": "June", "value": 95 },
          { "name": "July", "value": 0 },
          { "name": "August", "value": 0 },
          { "name": "September", "value": 0 },
          { "name": "October", "value": 0 },
          { "name": "November", "value": 0 },
          { "name": "December", "value": 0 }
      ]
  }
  ];

  colorScheme3 = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'] // Customize chart colors
  };

  // Options for animations and additional interactivity
  gradient = false;
  showLegend = false;
  showXAxis = true;
  showYAxis = true;
  showXAxisLabel = true;
  xAxisLabel = 'Month';
  showYAxisLabel = true;
  yAxisLabel = 'Books Borrowed';
  autoScale = true;
}
