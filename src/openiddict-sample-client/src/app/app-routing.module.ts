import { NgModule     } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { SigninComponent } from './components';

const routes: Routes = [
  {
    path     : 'signin',
    component: SigninComponent,
  },
  {
    path      : '',
    pathMatch : 'full',
    redirectTo: 'signin',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }