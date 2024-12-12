import { Component } from '@angular/core';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrl: './pie-chart.component.css'
})
export class PieChartComponent {

  view: [number, number] = [400, 300];
  colorScheme:any = { domain: ['#42A5F5', '#66BB6A', '#EF5350'] };

  donutData = [
    { name: 'Sale', value: 70 },
    { name: 'Distribute', value: 20 },
    { name: 'Return', value: 10 }
  ];
}
