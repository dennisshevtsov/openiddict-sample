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
    const body = JSON.stringify(command);
    const options = {
      headers: {
        'Content-Type': 'application/json',
      },
    };

    return this.http.post<void>(url, body, options);
  }
}
