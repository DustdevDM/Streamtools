import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WinRateBoxComponent } from './win-rate-box.component';

describe('WinRateBoxComponent', () => {
  let component: WinRateBoxComponent;
  let fixture: ComponentFixture<WinRateBoxComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WinRateBoxComponent]
    });
    fixture = TestBed.createComponent(WinRateBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
