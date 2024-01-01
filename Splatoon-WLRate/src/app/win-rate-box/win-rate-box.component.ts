import { Component } from '@angular/core';
import WinRateResponse from 'src/classes/WinRateResponse';
import { StatIncService } from '../services/statinc.service';

@Component({
  selector: 'app-win-rate-box',
  templateUrl: './win-rate-box.component.html',
  styleUrls: ['./win-rate-box.component.scss'],
})
export class WinRateBoxComponent {
  constructor(statinc: StatIncService) {
    this.StatIncService = statinc;
    this.fetchStats();
  }

  StatIncService: StatIncService;
  Request?: WinRateResponse;

  private fetchStats() {
    this.StatIncService.getWinRateData().subscribe({
      next: (response: WinRateResponse) => {
        if (response.IsValid) {
          this.Request = response;
        } else {
          this.Request = undefined;
        }
      },
      error: (error) => {
        this.Request = undefined;
      },
    });

    setTimeout(() => {
      this.fetchStats();
    }, 60000);
  }
}
