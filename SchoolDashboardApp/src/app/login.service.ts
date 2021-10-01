import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject,Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';

export class userObj {
  username: string
  password: string
}

@Injectable()


export class LoginService {

  constructor(private http: HttpClient, private cookieService: CookieService) { 
  }
  verifyLogin(usernameInput : string, passwordInput : string){
    let url = "http://localhost:48935/api/Login";
    var tempobj = new userObj();
    tempobj.username = usernameInput
    tempobj.password = passwordInput

    return this.http.post(url,tempobj,{responseType:'json'}).pipe(
      map(
      (response:any) => {
        return response
      }
    ));
  }
}
