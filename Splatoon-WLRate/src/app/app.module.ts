import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { WinRateBoxComponent } from './win-rate-box/win-rate-box.component';

const routes: Routes = [
  { path: 'splatoon/wl', component: WinRateBoxComponent },
];

@NgModule({
  declarations: [AppComponent, WinRateBoxComponent],
  imports: [BrowserModule, RouterModule.forRoot(routes)],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
