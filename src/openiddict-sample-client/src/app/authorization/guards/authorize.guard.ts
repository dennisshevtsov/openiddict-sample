import { Injectable } from '@angular/core';

import { CanLoad } from '@angular/router';

import { User        } from 'oidc-client';
import { UserManager } from 'oidc-client';

@Injectable({
  providedIn: 'root',
})
export class AuthorizeGuard implements CanLoad {
  public constructor(private readonly um: UserManager) { }

  public canLoad(): Promise<boolean> {
    return this.um.getUser().then(user => this.isAuthorized(user));
  }

  private isAuthorized(user: User | null): boolean | Promise<boolean> {
    if (user) {
      return true;
    }

    return this.um.signinRedirect().then(() => false);
  }
}
