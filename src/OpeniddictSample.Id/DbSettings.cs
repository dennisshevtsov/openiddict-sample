// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace OpeniddictSample.Id;

public sealed class DbSettings(string connectionString)
{
  public string ConnectionString { get; set; } = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
}
