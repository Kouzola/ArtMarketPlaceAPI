import { Pipe, PipeTransform } from '@angular/core';
import { Status } from '../model/order.model';

@Pipe({
  name: 'status',
  standalone: true
})
export class StatusPipe implements PipeTransform {

  transform(value: number): unknown {
    return Status[value];
  }

}
