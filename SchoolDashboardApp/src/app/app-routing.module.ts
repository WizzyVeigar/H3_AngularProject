import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RoomInfoComponent } from './room-info/room-info.component';
import { AuthGuardService as AuthGuard } from './auth/auth-guard.service';
import { AllRoomsComponent } from './all-rooms/all-rooms.component';


//Routes with protection
//The routes with canActivate will trigger canActivate function in AuthGuard
const routes: Routes = [
  {path: '',
  component:RoomInfoComponent,
  canActivate:[AuthGuard]
},
{path: 'login',
  component:LoginComponent
},
{
  path: 'room',
  component: RoomInfoComponent,
  canActivate:[AuthGuard]
},
{
  path: 'allrooms',
  component: AllRoomsComponent,
  canActivate:[AuthGuard]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
