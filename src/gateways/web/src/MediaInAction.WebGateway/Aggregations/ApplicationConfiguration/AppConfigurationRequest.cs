using System.Collections.Generic;
using MediaInAction.WebGateway.Aggregations.Base;

namespace MediaInAction.WebGateway.Aggregations.ApplicationConfiguration;

public class AppConfigurationRequest : IRequestInput
{
    public Dictionary<string, string> Endpoints { get; } = new();
}