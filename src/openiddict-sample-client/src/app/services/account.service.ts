import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { SignInAccountRequestDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  public constructor(private readonly http: HttpClient) { }

  public signin(command: SignInAccountRequestDto) {
    const url = 'https://localhost:5004/connect/token';
    const body = new URLSearchParams({
      email   : command.email,
      password: command.password,
      grant_type: 'authorization_code',
      client_id: 'openiddict-sample-api',
      code: 'test'
    });
    const options = {
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
    };

    return this.http.post<void>(url, body, options);
  }
}
