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
        .get('http://localhost:5047/Splatoon/WLStats')
        .then((res) => {
          if (res.status == 204) {
            observer.next(new WinRateResponse(false));
          } else {
            observer.next(
              new WinRateResponse(
                true,
                res.data.winPercentage,
                res.data.wonMatches
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
