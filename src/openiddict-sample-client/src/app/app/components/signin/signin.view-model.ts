import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { SignInAccountRequestDto } from '../../dtos';
import { AccountService          } from '../../services';

export interface SigninProps {
  email   : string;
  password: string;
}

@Injectable()
export class SigninViewModel implements SigninProps {
  private emailValue    : undefined | string;
  private passwordValue : undefined | string;
  private xsrfTokenValue: undefined | string;

  public constructor(
    private readonly service: AccountService) {}

  public get email(): string {
    return this.emailValue ?? '';
  }

  public set email(value: string) {
    this.emailValue = value;
  }

  public get password(): string {
    return this.passwordValue ?? '';
  }

  public set password(value: string) {
    this.passwordValue = value;
  }

  public get xsrfToken(): string {
    return this.xsrfTokenValue ?? '';
  }

  public set xsrfToken(value: string) {
    this.xsrfTokenValue = value;
  }

  public signin(): Observable<void> {
    const requestDto = new SignInAccountRequestDto(
      this.email, this.password);

    return this.service.signin(requestDto, this.xsrfToken);
  }
}
