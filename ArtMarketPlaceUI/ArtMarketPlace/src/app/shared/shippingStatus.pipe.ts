import { Pipe, PipeTransform } from '@angular/core';
import { ShipmentStatus } from '../model/shipment.model';

@Pipe({
  name: 'shippingStatus',
  standalone: true
})
export class ShippinStatusPipe implements PipeTransform {

  transform(value: number): unknown {
    return ShipmentStatus[value];
  }

}