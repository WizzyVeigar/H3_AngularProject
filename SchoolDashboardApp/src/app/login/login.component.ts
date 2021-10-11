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

  showloadingindiactor = true;
  constructor(private loginService: LoginService,private router : Router, private cookieService: CookieService) { 
    this.router.events.subscribe((routerEvent: Event) =>{
      if (routerEvent instanceof NavigationStart) {
        this.showloadingindiactor = true;
        console.log('WE ARE LOADING SOMETHING ' + this.showloadingindiactor)
      }

      if (routerEvent instanceof NavigationEnd) {
        this.showloadingindiactor = false;
        console.log('WE FINISHED LOADING SOMETHING! ' + this.showloadingindiactor)
      }
    })
  }

  username : string
  password : string

  ngOnInit(): void {
  }

  login() : void{
    this.loginService.verifyLogin(this.username,this.password).subscribe(
      res => {
        this.router.navigate(['room']);
        this.cookieService.set('IsLogged',res['token'] + '')
      }
    )
  }
}
