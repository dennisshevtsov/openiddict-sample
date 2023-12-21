import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { SignInAccountRequestDto } from '../dtos';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  public constructor(private readonly http: HttpClient) { }

  public signin(command: SignInAccountRequestDto) {
    const url = 'https://localhost:5004/connect/authorize';
    const body = new URLSearchParams({
      username: command.email,
      password: command.password,
      grant_type: 'authorization_code',
      client_id: 'openiddict-sample-api',
      code_challenge: command.code,
      code_challenge_method: 'S256',
      response_type: 'code',
      redirect_uri: 'http://localhost:5005/signin-callback'
    });
    const options = {
      headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
      },
    };

    return this.http.post<void>(url, body, options);
  }
}
