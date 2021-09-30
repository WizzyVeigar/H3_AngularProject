import { Component, OnInit } from '@angular/core';
import {userObj,LoginService} from '../login.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  username : string
  password : string

  ngOnInit(): void {
  }

  login() : void{
    var temp
    this.loginService.verifyLogin(this.username,this.password).subscribe(data=>{
    })
  }

}
