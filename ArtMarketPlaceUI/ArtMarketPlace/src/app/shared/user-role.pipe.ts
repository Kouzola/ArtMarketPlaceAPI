import { Pipe, PipeTransform } from '@angular/core';
import { RoleUser } from '../model/user.model';

@Pipe({
  name: 'userRole',
  standalone: true
})
export class UserRolePipe implements PipeTransform {

   transform(value: number): string {
      return RoleUser[value];
    }

}
