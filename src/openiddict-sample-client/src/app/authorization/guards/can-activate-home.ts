import { inject } from '@angular/core';

import { ActivatedRouteSnapshot } from '@angular/router';
import { CanActivateFn          } from '@angular/router';
import { RouterStateSnapshot    } from '@angular/router';

import { User        } from 'oidc-client';
import { UserManager } from 'oidc-client';

export const canActivateHome: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const um: UserManager = inject(UserManager);

  return um.getUser().then((user: User | null) => {
    if (user) {
      return true;
    }

    return um.signinRedirect().then(() => false);
  });
}
