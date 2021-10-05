import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})


export class AuthService {

  constructor(public jwtHelper: JwtHelperService,private _cookieService: CookieService,private http: HttpClient) {
    this.jwtHelper = new JwtHelperService();
   }

  public isAuthenticated() : boolean {
    const token = this._cookieService.get('IsLogged');
    var test = this.jwtHelper.decodeToken(token)
    if(!this.jwtHelper.isTokenExpired(token)){
      return true
    }else{
      let url = "http://localhost:48935/api/Login/IsLoggedIn?tokenString=" + token;
      this.http.post(url,null).pipe(map(
        (response:any) => {
          console.log(response)
          this._cookieService.set('IsLogged',response['token'])
          return this.jwtHelper.isTokenExpired(token)
        }
      ))
    }


    return !this.jwtHelper.isTokenExpired(token);
  }
}
