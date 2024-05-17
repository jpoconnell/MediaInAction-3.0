using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MediaInAction.FileService.FileMethodNs;

public class FileMethod_Tests : FileServiceApplicationTestBase
{
    [Fact]
    public void EnsureAllFileMethodsAreRegistered()
    {
        var existingFileMethodTypes = GetExistingFileMethotTypes();

        var fileMethods = GetRequiredService<IEnumerable<IFileMethod>>();

        fileMethods.Count().ShouldBe(existingFileMethodTypes.Length);
    }

    [Fact]
    public void EnsureFileTypeIsUnique()
    {
        var fileMethods = GetRequiredService<IEnumerable<IFileMethod>>().ToList();

        var distinctedFileMethods = fileMethods.DistinctBy(p => p.Name).ToList();

        fileMethods.Count.ShouldBe(distinctedFileMethods.Count);
    }

    [Fact]
    public void EnsureAllFileMethodsHaveFileTypeImplementation()
    {
        foreach (var fileMethod in GetRequiredService<IEnumerable<IFileMethod>>())
        {
            fileMethod.Name.ShouldNotBeNullOrEmpty();
        }
    }

    private static Type[] GetExistingFileMethotTypes()
    {
        return typeof(IFileMethod)
            .Assembly
            .GetTypes()
            .Where(t => typeof(IFileMethod).IsAssignableFrom(t) && !t.IsAbstract)
            .ToArray();
    }
}
