import { Component, OnInit } from '@angular/core';
import {userObj,LoginService} from '../login.service'
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

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

  ngOnInit(): void {
  }

  login() : void{
    this.loginService.verifyLogin(this.username,this.password).subscribe(
      res => {
        console.log(res)
        if(res != '0'){
          this.router.navigate(['room']);
          this.cookieService.set('IsLogged',res['token'] + '')
        }else{
          this.router.navigate(['login'])
        }
      }
    )
  }
}
