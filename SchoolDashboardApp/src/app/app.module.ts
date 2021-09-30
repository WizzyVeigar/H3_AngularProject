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
import { MatFormFieldControl } from '@angular/material/form-field';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AppComponent,
    RoomInfoComponent,
    LoginComponent
  ],
  imports: [
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatCardModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    RouterModule,
    MatGridListModule,
    MatTableModule,
    FormsModule
  ],
  providers: [DatePipe,LoginService],
  bootstrap: [AppComponent]
})
export class AppModule { }
