import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { LoginService } from '../login.service';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  constructor(public jwtHelper: JwtHelperService,private _cookieService: CookieService,private http: HttpClient,private loginService:LoginService) {
    this.jwtHelper = new JwtHelperService();
   }
   
   jsonObj : any

  public async isAuthenticated() : Promise<boolean> {
    const token = this._cookieService.get('IsLogged');
    if(token != ''){
      if(!this.jwtHelper.isTokenExpired(token)){
        return true
      }else{
        await this.SetNewLoggedInTokenAsCookie(token)
        return true
      }
    }
    return false
  }

  public async SetNewLoggedInTokenAsCookie(token : any) : Promise<void> {
    this.loginService.isLoggedIn(token).subscribe(res=> {
      this._cookieService.set('IsLogged',res)
      return res
    })
  }
}
