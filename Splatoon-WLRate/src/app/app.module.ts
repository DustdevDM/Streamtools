import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { WinRateBoxComponent } from './win-rate-box/win-rate-box.component';

@NgModule({
  declarations: [
    AppComponent,
    WinRateBoxComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
