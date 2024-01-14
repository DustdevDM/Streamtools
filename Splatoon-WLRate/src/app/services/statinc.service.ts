import { Injectable } from '@angular/core';
import { Observable, config } from 'rxjs';
import WinRateResponse from 'src/classes/WinRateResponse';
import axios, { Axios } from 'axios';

@Injectable({
  providedIn: 'root',
})
export class StatIncService {
  constructor() {}

  public getWinRateData(): Observable<WinRateResponse> {
    return new Observable((observer) => {
      axios
        .get('https://streamtools.dustdev.de/api/Splatoon/WLStats')
        .then((res) => {
          if (res.status == 204) {
            observer.next(new WinRateResponse(false));
          } else {
            observer.next(
              new WinRateResponse(
                true,
                res.data.winPercentage,
                res.data.wonMatches,
                res.data.splatfestColor
              )
            );
          }
          observer.complete();
        })
        .catch((err) => {
          observer.error(err);
          observer.complete();
        });
    });
  }
}
