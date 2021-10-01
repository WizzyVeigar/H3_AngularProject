import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  constructor(public jwtHelper: JwtHelperService,private _cookieService: CookieService) {
    this.jwtHelper = new JwtHelperService();
   }

  public isAuthenticated() : boolean {
    const token = this._cookieService.get('IsLogged');
    console.log(token);

    return !this.jwtHelper.isTokenExpired(token);
  }
}
