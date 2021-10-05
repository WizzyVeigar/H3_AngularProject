import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject,observable,Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

export class userObj {
  username: string
  password: string
}

@Injectable()



export class LoginService {

  constructor(private http: HttpClient, private cookieService: CookieService,private router: Router) { 
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
    ),
    catchError((err: any)=>{
      this.router.navigate['login']
      return throwError(err);
    }));
  }
}
