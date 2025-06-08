import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./pages/login/login.component";
import { ToastComponent } from "./shared/toast/toast.component";
import { ToastService } from './services/toast.service';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ToastComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnDestroy{
  
  
  title = 'ArtMarketPlace';
  toastService = inject(ToastService);
  showSucces$ = this.toastService.showSucces$;
  showError$ = this.toastService.showError$;
  message$ = this.toastService.message$;
  subscriptionSuccess: Subscription;
  subscriptionError: Subscription;
  subscriptionMessage: Subscription;
  success: boolean = false;
  error: boolean = false;
  message: string = "";

  constructor(){
    this.subscriptionSuccess = this.showSucces$.subscribe((x) => this.success = x);
    this.subscriptionError = this.showError$.subscribe((x) => this.error = x);
    this.subscriptionMessage = this.message$.subscribe((x) => this.message = x);
  }

  ngOnDestroy(): void {
    this.subscriptionError.unsubscribe();
    this.subscriptionSuccess.unsubscribe();
    this.subscriptionMessage.unsubscribe();
  }
  
}
