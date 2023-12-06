import { Injectable } from '@angular/core';

import { CanActivate } from '@angular/router';
import { Router      } from '@angular/router';
import { UrlTree     } from '@angular/router';

import { UserManager } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})
export class SigninCallbackGuard implements CanActivate {
  public constructor(
    private readonly um    : UserManager,
    private readonly router: Router,
  ) { }

  public canActivate():  Promise<UrlTree> {
    return this.um.signinRedirectCallback()
                  .then(() => this.router.createUrlTree(['']));
  }
}
