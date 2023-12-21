import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { HomeComponent   } from './components';
import { SigninComponent } from './components';
import { canActivateHome } from './authorization';

const routes: Routes = [
  {
    path     : 'signin',
    component: SigninComponent,
  },
  {
    path       : 'home',
    canActivate: [canActivateHome],
    component  : HomeComponent,
  },
  {
    path      : '',
    pathMatch : 'full',
    redirectTo: 'home',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }