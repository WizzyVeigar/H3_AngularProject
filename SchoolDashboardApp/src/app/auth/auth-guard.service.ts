import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router'
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(public auth: AuthService,public router: Router) { }

  //Calls isAuthenticated to check if current user (based on token) is authorized
  canActivate() : boolean {
    this.auth.isAuthenticated().then(value => {
      if(value){
        return true;
      }else {
        this.router.navigate(['login']);
        return false
      }
    })
    return true
  }
}
