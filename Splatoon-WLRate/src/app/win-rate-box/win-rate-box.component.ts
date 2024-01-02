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
    this.Fade = false;
    this.fetchStats();
  }

  StatIncService: StatIncService;
  Request?: WinRateResponse;
  Fade: Boolean;

  private fetchStats() {
    this.StatIncService.getWinRateData().subscribe({
      next: (response: WinRateResponse) => {
        if (response.IsValid) {
          //The following if check determines if the number change fade animation should be played
          if (
            this.Request == undefined ||
            this.Request.WinPercentage == response.WinPercentage
          ) {
            this.Request = response;
          } else {
            this.Fade = true;
            setTimeout(() => {
              this.Request = response;
            }, 1000);
            setTimeout(() => {
              this.Fade = false;
            }, 2000);
          }
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
