import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { LoginService } from '../login.service';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  private loggedIn = new BehaviorSubject<boolean>(false); // {1}
  private userName = new BehaviorSubject<string>('unknown'); // {2}

  constructor(public jwtHelper: JwtHelperService,private _cookieService: CookieService,private http: HttpClient,private loginService:LoginService,private router : Router) {
    this.jwtHelper = new JwtHelperService();
   }
   
   jsonObj : any

   get isLoggedIn() {
    return this.loggedIn.asObservable(); // {2}
  }

  get getuserName() {
    return this.userName.asObservable(); // {2}
  }


  //Gets token from cookie, and checks if it is expired, then refreshes token if it is not expired to keep session active
  //If token is expired, token will be deleted and user will get redirected to login
  public async isAuthenticated() : Promise<boolean> {
    const token = this._cookieService.get('IsLogged');
    if(token != ''){
      if(!this.jwtHelper.isTokenExpired(token)){
        await this.SetNewLoggedInTokenAsCookie(token)
        this.userName.next(this.jwtHelper.decodeToken(token).name)
        this.loggedIn.next(true)
        return true
      }else{
        this.userName.next(this.jwtHelper.decodeToken(token)['name'])
        this.loggedIn.next(false)
        return false
      }
    }
    this.loggedIn.next(false)
    return false
  }


  //Deletes IsLogged token from cookie, and redirects user to login page
  Logout(){
    console.log("i'm here!")
    var token = this._cookieService.delete('IsLogged')
    this.loggedIn.next(false);
    this.router.navigate(['login'])
  }

  //Calls loginservice to verify current token in cookie, if it's authorized it will be refreshed to keep session going.
  public async SetNewLoggedInTokenAsCookie(token : any) : Promise<void> {
    this.loginService.isLoggedIn(token).subscribe(res=> {
      this._cookieService.set('IsLogged',res)
      return res
    })
  }
}
