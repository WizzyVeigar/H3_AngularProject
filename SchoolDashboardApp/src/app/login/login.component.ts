import { Component, OnInit } from '@angular/core';
import {userObj,LoginService} from '../login.service'
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import {Event,NavigationStart,NavigationEnd} from '@angular/router'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService,private router : Router, private cookieService: CookieService) { 
  }

  username : string
  password : string

  notLogged : boolean

  ngOnInit(): void {
  }

  //Calls loginservice verifyLogin function to verify login, and redirects user if authorized
  //If error it will set notLogged to false.
  login() : void{
    this.loginService.verifyLogin(this.username,this.password).subscribe(
      res => {
        this.router.navigate(['room']);
        this.cookieService.set('IsLogged',res['token'] + '')
      },
      err => {
        this.notLogged = true
      }
    )
  }
}
