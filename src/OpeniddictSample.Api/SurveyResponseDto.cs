// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace OpeniddictSample.Api;

public sealed record class SurveyResponseDto(Guid SurveyId, string Name, string Description, DateTime CreatedOn);
