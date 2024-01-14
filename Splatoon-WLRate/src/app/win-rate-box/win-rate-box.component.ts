import { Component } from '@angular/core';
import WinRateResponse from 'src/classes/WinRateResponse';
import { StatIncService } from '../services/statinc.service';
import { getContrastingHex } from 'color-contrast-picker';

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
          if (response.SplatfestColor) {
            response.SplatfestColor =
              this.ConvertColor(response.SplatfestColor) ?? undefined;
          }

          this.setData(response, this.shouldFade(response));
        } else {
          this.setData(undefined, false);
        }
      },
      error: () => {
        this.setData(undefined, false);
      },
    });

    setTimeout(() => {
      this.fetchStats();
    }, 60000);
  }

  private setData(data?: WinRateResponse, withFade: boolean = false) {
    if (withFade) {
      this.Fade = true;
      setTimeout(() => {
        this.Request = data;
      }, 1000);
      setTimeout(() => {
        this.Fade = false;
      }, 2000);
    } else {
      this.Request = data;
    }
  }

  private shouldFade(data?: WinRateResponse) {
    if (this.Request == undefined) return false;
    return (
      this.Request != undefined &&
      data != undefined &&
      data.IsValid &&
      data.WonMatches != this.Request.WonMatches
    );
  }

  private ConvertColor(color: string): string | null {
    return getContrastingHex(`#${color.replace('#', '').slice(0, -2)}`, 1.5);
  }
}
