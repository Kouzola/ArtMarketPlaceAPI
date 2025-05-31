import { Injectable } from '@angular/core';
import { jwtDecode } from 'jwt-decode';
import { UserTokenInfo } from '../model/userTokenInfo.model';
import { HttpClient } from '@angular/common/http';
import { User } from '../model/user.model';
import { BehaviorSubject, catchError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private user = new BehaviorSubject<User | null>(null);
  public user$ = this.user.asObservable();

  private users = new BehaviorSubject<User[] | null>([]);
  public users$ = this.users.asObservable();

  constructor(private http: HttpClient) { }

  URL = 'https://localhost:7166/api/User'


  getUserbyId(id: number){
    const finalUrl = this.URL + `/${id}`;
    return this.http.get<User>(finalUrl).pipe(
      tap(u => this.user.next(u))
    );
  }

  getUserByUsername(userName: string){
    const finalUrl = this.URL + `/${userName}`;
    return this.http.get<User>(finalUrl).pipe(
      tap(u => this.user.next(u))
    );
  }

  getUserTokenInfo() : UserTokenInfo{
      const token = sessionStorage.getItem('jwt') ?? "";
      const decodedToken: any = jwtDecode(token);
      return {
        name: decodedToken['name'],
        id: decodedToken['id'],
        role: decodedToken['role'],
      };
  }

  updateUserInfo(id: number, user: User){
    const finalUrl = this.URL + `/${id}`;
    return this.http.put<User>(finalUrl,user).pipe(
      tap(u => this.user.next(u))
    );
  }

  getAllDeliveryGuy(){
    const finalUrl = this.URL + '/deliveryPartner';
    return this.http.get<User[]>(finalUrl).pipe(
      tap(u => this.users.next(u))
    )
  }

}
