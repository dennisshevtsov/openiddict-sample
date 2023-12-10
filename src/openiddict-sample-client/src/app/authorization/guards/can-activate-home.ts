import { inject } from '@angular/core';

import { ActivatedRouteSnapshot } from '@angular/router';
import { CanActivateFn          } from '@angular/router';
import { RouterStateSnapshot    } from '@angular/router';

import { User        } from 'oidc-client';
import { UserManager } from 'oidc-client';

export const canActivateHome: CanActivateFn = async (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const um: UserManager = inject(UserManager);

  const user: User | null = await um.getUser();
  if (user) {
    return true;
  }

  await um.signinRedirect();
  return false;
}
