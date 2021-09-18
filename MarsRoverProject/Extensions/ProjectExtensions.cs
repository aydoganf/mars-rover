using System;
using System.Linq;

namespace MarsRoverProject.Extensions
{
    public static class ProjectExtensions
    {
        public static bool IsIn<T>(this T value, params T[] list)
        {
            return list.Contains(value);
        }
    }
}
