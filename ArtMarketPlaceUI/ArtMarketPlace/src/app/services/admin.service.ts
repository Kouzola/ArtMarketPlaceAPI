import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { User } from '../model/user.model';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  private user = new BehaviorSubject<User | null>(null);
  public user$ = this.user.asObservable();

  private users = new BehaviorSubject<User[]>([]);
  public users$ = this.users.asObservable();
  
  constructor(private http: HttpClient) { }

  UserURL = 'https://localhost:7166/api/User/admin/users';

  //User related method
  getUserbyId(id: number){
      const finalUrl = this.UserURL + `/${id}`;
      return this.http.get<User>(finalUrl).pipe(
        tap(u => this.user.next(u))
      );
  }

  getAllUsers(){
    return this.http.get<User[]>(this.UserURL).pipe(
      tap(u => this.users.next(u))
    );
  }

  updateUserInfo(id: number, user: User){
    const finalUrl = this.UserURL + `/${id}`;
    return this.http.put<User>(finalUrl,user).pipe(
      tap(u => this.user.next(u))
    );
  }

  deleteUserById(id: number){
    const finalUrl = this.UserURL + `/${id}`;
    return this.http.delete(finalUrl);
  }

}
