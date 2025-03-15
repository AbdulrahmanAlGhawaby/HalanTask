import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];
  title = 'halantask.client';
  selectedTab = 0;
  constructor(private router: Router) {}

  ngOnInit() {
    // Set active tab based on the current route
    if (window.location.pathname.includes('/tickets/create')) {
      this.selectedTab = 1; // Set "Create Ticket" as active
    } else {
      this.selectedTab = 0; // Set "View Tickets" as active
    }
  }

  onTabChange(event: any) {
    if (event.index === 0) {
      this.router.navigate(['/tickets']);
    } else if (event.index === 1) {
      this.router.navigate(['/tickets/create']);
    }
  }
}
