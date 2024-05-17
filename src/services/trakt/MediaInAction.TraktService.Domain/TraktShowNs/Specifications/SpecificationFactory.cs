using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs.Specifications;

public static class SpecificationFactory
{
    public static ISpecification<TraktShow> Create(string filter)
    {
        
        if (filter.StartsWith("s:"))
        {
            var status = int.Parse(filter.Split(':')[1]);
            return new StatusSpecification(status);
        }

        return null;
    }
}