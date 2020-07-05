/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TitiuloComponent } from './titiulo.component';

describe('TitiuloComponent', () => {
  let component: TitiuloComponent;
  let fixture: ComponentFixture<TitiuloComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TitiuloComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TitiuloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
