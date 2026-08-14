import { TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideRouter([])]
    }).compileComponents();
  });

  it('renders the shared application identity and environment', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const element = fixture.nativeElement as HTMLElement;

    expect(element.textContent).toContain('ReadyGate DemoApp');
    expect(element.textContent).toContain('Hackathon Demo');
    expect(element.querySelector('main#main-content')).not.toBeNull();
  });
});
