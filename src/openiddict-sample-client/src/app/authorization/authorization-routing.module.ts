import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { Routes       } from '@angular/router';

import { SigninCallbackGuard } from './guards';
import { SilentCallbackGuard } from './guards';

const routes: Routes = [
  {
    path: 'signin-callback',
    children: [],
    canActivate: [SigninCallbackGuard],
  },
  {
    path: 'silent-callback',
    children: [],
    canActivate: [SilentCallbackGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthorizationRoutingModule { }
