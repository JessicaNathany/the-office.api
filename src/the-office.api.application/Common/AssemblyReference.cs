using System.Reflection;

namespace the_office.api.application.Common;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}