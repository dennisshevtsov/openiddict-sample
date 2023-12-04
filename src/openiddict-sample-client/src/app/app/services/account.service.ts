import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { SignInAccountRequestDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  public constructor(private readonly http: HttpClient) { }

  public signin(command: SignInAccountRequestDto, xsrfToken: string) {
    const url = 'api/account/signin';
    const body = JSON.stringify(command);
    const options = {
      headers: {
        'Content-Type': 'application/json',
        'X-XSRF-TOKEN': xsrfToken,
      },
    };

    return this.http.post<void>(url, body, options);
  }
}
