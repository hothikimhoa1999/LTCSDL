import { Component, Inject, GetTestability } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { strict } from 'assert';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];
  public products: any = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>('https://localhost:44369/' + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
      console.log(this.forecasts);
    }, error => console.error(error));

    // lấy thông tin và data từ trnag local host 44369 sang trang riêng angular với local host là 4200

    http.post('https://localhost:44369/' + 'api/Products/get-all', null).subscribe(result => {
      this.products = result;
      console.log(this.products);
    }, error => console.log(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

