import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RoomInfoComponent } from './room-info/room-info.component';
import { AuthGuardService as AuthGuard } from './auth/auth-guard.service';

const routes: Routes = [
  {path: '', component: AppComponent, children: [
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
    }
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
