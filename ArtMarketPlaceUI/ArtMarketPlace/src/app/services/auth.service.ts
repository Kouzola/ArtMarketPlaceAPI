import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  URL = 'https://localhost:7166/api/auth'

  login(userName: string, password: string): Observable<any> {
    return this.http.post(this.URL + '/Login',{UserName: userName, Password: password});
  }
}
