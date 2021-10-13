import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaderResponse, HttpHeaders, HttpParams, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { BehaviorSubject,observable,Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

export class userObj {
  username: string
  password: string
}

@Injectable()



export class LoginService {

  constructor(private http: HttpClient, private cookieService: CookieService,private router: Router,private jwtHelper:JwtHelperService) { 
  }

  //Calls API to verify user based on username and password
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
      return throwError(err);
    }));
  }

  //Calls API to check if current token is authorized lately.
  isLoggedIn(token:string){
    let url = "http://localhost:48935/api/Login/IsLoggedIn?tokenString=" + token;
    return this.http.post(url,null).pipe(
      map(
        (response:any) => {
          return response['token'];
        }
      )
    )
  }
}
