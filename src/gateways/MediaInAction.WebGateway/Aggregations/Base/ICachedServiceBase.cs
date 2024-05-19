﻿using System.Collections.Generic;

namespace MediaInAction.WebGateway.Aggregations.Base;

public interface ICachedServiceBase<TValue>
{
    void Add(string serviceName, TValue data);
    IDictionary<string, TValue> GetManyAsync(IEnumerable<string> serviceNames);
}