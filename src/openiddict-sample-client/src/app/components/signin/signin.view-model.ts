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
  private codeValue     : undefined | string;

  public constructor(
    private readonly service: AccountService) {}

  public get email(): string {
    if (this.emailValue) {
      return this.emailValue;
    }

    throw 'No email provided.';
  }

  public set email(value: string) {
    this.emailValue = value;
  }

  public get password(): string {
    if (this.passwordValue) {
      return this.passwordValue;
    }

    throw 'No password provided.';
  }

  public set password(value: string) {
    this.passwordValue = value;
  }

  public get code(): string {
    if (this.codeValue) {
      return this.codeValue;
    }

    throw 'No password provided.';
  }

  public set code(value: string) {
    this.codeValue = value;
  }

  public signin(): Observable<void> {
    const requestDto = new SignInAccountRequestDto(
      this.email, this.password, this.code);

    return this.service.signin(requestDto);
  }
}
