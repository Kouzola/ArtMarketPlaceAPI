import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private showSucces = new BehaviorSubject<boolean>(false);
  private showError = new BehaviorSubject<boolean>(false);
  private message = new BehaviorSubject<string>("");
  showSucces$ = this.showSucces.asObservable();
  showError$ = this.showError.asObservable();
  message$ = this.message.asObservable();


  constructor() { }

  showSuccesToast(message: string){
    if(!this.showSucces.value){
      this.showSucces.next(true);
      this.message.next(message);
      setTimeout(() => this.closeToast(), 5000);
    }
  }

  showErrorToast(message: string){
    if(!this.showError.value){
      this.showError.next(true);
      this.message.next(message);
      setTimeout(() => this.closeToast(), 5000);
    }
  }

  closeToast() {
  this.showSucces.next(false);
  this.showError.next(false);
  this.message.next("");
  }
}
