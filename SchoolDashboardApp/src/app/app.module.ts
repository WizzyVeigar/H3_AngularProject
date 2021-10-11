import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RoomInfoComponent } from './room-info/room-info.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatGridListModule } from '@angular/material/grid-list'; 
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { LoginService } from './login.service';
import { FormsModule } from '@angular/forms';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldControl } from '@angular/material/form-field';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { APP_INITIALIZER } from '@angular/core';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';
import { AllRoomsComponent } from './all-rooms/all-rooms.component';
import {MatIconModule} from '@angular/material/icon'

export function getToken(): string {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    RoomInfoComponent,
    LoginComponent,
    AllRoomsComponent
  ],
  imports: [
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatCardModule,
    BrowserModule,
    MatButtonModule,
    MatIconModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    RouterModule,
    MatGridListModule,
    MatTableModule,
    FormsModule,
    JwtModule.forRoot({
      config:{
        tokenGetter: getToken
      }
    })
  ],
  providers: [
    DatePipe,
    LoginService,
    CookieService,
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
