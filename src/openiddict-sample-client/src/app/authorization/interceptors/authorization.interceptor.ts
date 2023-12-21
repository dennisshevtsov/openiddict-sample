import { HttpEvent       } from '@angular/common/http';
import { HttpHandler     } from '@angular/common/http';
import { HttpHeaders     } from '@angular/common/http';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest     } from '@angular/common/http';

import { Injectable } from '@angular/core';

import { User        } from 'oidc-client';
import { UserManager } from 'oidc-client';

import { from       } from 'rxjs';
import { Observable } from 'rxjs';
import { switchMap  } from 'rxjs';

@Injectable()
export class AuthorizationInterceptor implements HttpInterceptor {
  public constructor(private readonly um : UserManager) { }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const addBearerToken = (user: User | null) => {
      if (user) {
        return next.handle(req.clone({
          headers: new HttpHeaders({
            'Content-Type' : 'application/json',
            'Authorization': `Bearer ${user?.access_token}`,
          }),
        }));
      }

      return next.handle(req);
    };

    return from(this.um.getUser()).pipe(switchMap(addBearerToken));
  }
}
