import { Component, input } from '@angular/core';
import {NgbToast} from '@ng-bootstrap/ng-bootstrap'

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [NgbToast],
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent {
  success = input<boolean>();
  message = input<string>();
}
