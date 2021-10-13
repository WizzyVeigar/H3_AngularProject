import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedIn$: Observable<boolean>;
  userName$: string;

  constructor(private cookieService : CookieService,private router : Router,private authService: AuthService) { }

  //On initialization gets observeables from authservice for IsLoggedIn result and Username, and binds sets these to local variables
  //for HTML to bind to.
  ngOnInit(): void {
    this.isLoggedIn$ = this.authService.isLoggedIn;
    this.authService.getuserName.subscribe(res => {
      this.userName$ = res
    })
  }

  //Calls authservice to log user out
  onLogout(){
    this.authService.Logout();
  }

}
