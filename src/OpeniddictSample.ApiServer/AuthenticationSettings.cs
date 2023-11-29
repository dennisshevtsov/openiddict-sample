// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace OpeniddictSample.ApiServer;

public sealed class AuthenticationSettings(string identityProviderUrl)
{
  public string IdentityProviderUrl { get; } = identityProviderUrl ?? throw new ArgumentNullException(nameof(identityProviderUrl));
}
