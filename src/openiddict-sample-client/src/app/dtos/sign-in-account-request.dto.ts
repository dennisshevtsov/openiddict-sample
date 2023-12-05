export class SignInAccountRequestDto {
  public constructor(
    public readonly email    : string,
    public readonly password : string,
  ) {}
}
