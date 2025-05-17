import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {jwtDecode} from 'jwt-decode';
import { User } from '../model/user.model';
import { userRegister } from '../model/userRegister.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  URL = 'https://localhost:7166/api/auth'

  login(userName: string, password: string): Observable<any> {
    return this.http.post(this.URL + '/Login',{UserName: userName, Password: password});
  }

  register(userRegisterInfo: userRegister): Observable<any> {
    return this.http.post(this.URL + '/Register',userRegisterInfo);
  }

  getActualUserInfo() : User{
    const token = sessionStorage.getItem('jwt') ?? "";
    const decodedToken: any = jwtDecode(token);
    return {
      id: decodedToken['id'],
      role: decodedToken['role'],
    };
  }
}
