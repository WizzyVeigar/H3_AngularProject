import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

export class userObj {
  username: string
  password: string
}

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  constructor(private http: HttpClient) { 
  }
  verifyLogin(usernameInput : string, passwordInput : string){
    let url = "http://localhost:48935/api/Login";
    var tempobj = new userObj();
    tempobj.username = usernameInput
    tempobj.password = passwordInput

    return this.http.post(url,tempobj,{responseType:'text'});
  }
}
