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
      (response:any) => {
        if(response){
          this.router.navigate(['room']);
          this.cookieService.set('IsLogged','eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTYzMjk4Njk4NCwiZXhwIjoxNjMyOTkwNTg0fQ.LLL6Z7jO_rFHd0lgga-x_9c9V--aERwdtEycutw-32U')
        }else{
          this.router.navigate(['login'])
        }
      }
    )
  }

}
