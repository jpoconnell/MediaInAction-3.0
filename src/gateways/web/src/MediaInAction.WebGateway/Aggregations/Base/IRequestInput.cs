using System.Collections.Generic;

namespace MediaInAction.WebGateway.Aggregations.Base;

public interface IRequestInput
{
    Dictionary<string, string> Endpoints { get; }
}