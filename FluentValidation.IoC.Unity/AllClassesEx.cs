using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;

namespace FluentValidation.IoC.Unity
{
    public static class AllClassesEx
    {
        private static readonly string NetFrameworkProductName = GetNetFrameworkProductName();
        private static readonly string UnityProductName = GetUnityProductName();
        private static readonly string[] assemblyExtensions = new[] { ".dll",".exe" };

#if NETCOREAPP1_0
        private static string GetBasePath()
        {
            return System.Windows.ApplicationModel.Package.Current.InstalledLocation;
        }
#else
        private static string GetSearchPath()
        {
            return (AppDomain.CurrentDomain.RelativeSearchPath == null)
                    ? AppDomain.CurrentDomain.BaseDirectory
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.RelativeSearchPath);
        }

        private static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public static IEnumerable<Type> FromAssembliesInSearchPath(bool includeSystemAssemblies = false, bool includeUnityAssemblies = false, bool skipOnError = true, bool recursive = true)
        {
            return FromAssembliesInPath(GetSearchPath(), includeSystemAssemblies, includeUnityAssemblies, skipOnError);
        }
#endif
        public static IEnumerable<Type> FromAssembliesInBasePath(bool includeSystemAssemblies = false, bool includeUnityAssemblies = false, bool skipOnError = true, bool recursive = true)
        {
            return FromAssembliesInPath(GetBasePath(), includeSystemAssemblies, includeUnityAssemblies, skipOnError, recursive);
        }
        
        public static IEnumerable<Type> FromAssembliesInPath(string path, bool includeSystemAssemblies = false, bool includeUnityAssemblies = false, bool skipOnError = true, bool recursive = true)
        {
            return FromCheckedAssemblies(GetAssembliesInPath(path, includeSystemAssemblies, includeUnityAssemblies, skipOnError, recursive), skipOnError);
        }

        private static IEnumerable<Assembly> GetAssembliesInPath(string path, bool includeSystemAssemblies, bool includeUnityAssemblies, bool skipOnError, bool recursive)
        {
            return GetAssemblyNames(path, skipOnError, recursive)
                    .Select(an => LoadAssembly(Path.GetFileNameWithoutExtension(an), skipOnError))
                    .Where(a => a != null && (includeSystemAssemblies || !IsSystemAssembly(a)) && (includeUnityAssemblies || !IsUnityAssembly(a)));
        }

        private static IEnumerable<string> GetAssemblyNames(string path, bool skipOnError, bool recursive)
        {
            try
            {
                var searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                return Directory.EnumerateFiles(path, "*", searchOption)
                    .Where(x => assemblyExtensions.Contains(Path.GetExtension(x)));
            }
            catch (Exception e)
            {
                if (!(skipOnError && (e is DirectoryNotFoundException || e is IOException || e is SecurityException || e is UnauthorizedAccessException)))
                {
                    throw;
                }

                return Enumerable.Empty<string>();
            }
        }

        private static Assembly LoadAssembly(string assemblyName, bool skipOnError)
        {
            try
            {
                return Assembly.Load(assemblyName);
            }
            catch (Exception e)
            {
                if (!(skipOnError && (e is FileNotFoundException || e is FileLoadException || e is BadImageFormatException)))
                {
                    throw;
                }

                return null;
            }
        }

        private static IEnumerable<Type> FromCheckedAssemblies(IEnumerable<Assembly> assemblies, bool skipOnError)
        {
            return assemblies
                    .SelectMany(a =>
                    {
                        IEnumerable<TypeInfo> types;

                        try
                        {
                            types = a.DefinedTypes;
                        }
                        catch (ReflectionTypeLoadException e)
                        {
                            if (!skipOnError)
                            {
                                throw;
                            }

                            types = e.Types.TakeWhile(t => t != null).Select(t => t.GetTypeInfo());
                        }

                        return types.Where(ti => ti.IsClass && !ti.IsAbstract && !ti.IsValueType && ti.IsVisible).Select(ti => ti.AsType());
                    });
        }

        private static bool IsSystemAssembly(Assembly a)
        {
            if (NetFrameworkProductName != null)
            {
                var productAttribute = a.GetCustomAttribute<AssemblyProductAttribute>();
                return productAttribute != null && string.Compare(NetFrameworkProductName, productAttribute.Product, StringComparison.Ordinal) == 0;
            }
            return false;
        }

        private static bool IsUnityAssembly(Assembly a)
        {
            if (UnityProductName == null) return false;
            var productAttribute = a.GetCustomAttribute<AssemblyProductAttribute>();
            return productAttribute != null && string.Compare(UnityProductName, productAttribute.Product, StringComparison.Ordinal) == 0;
        }

        private static string GetNetFrameworkProductName()
        {
            var productAttribute = typeof(object).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return productAttribute?.Product;
        }

        private static string GetUnityProductName()
        {
            var productAttribute = typeof(global::Unity.IUnityContainer).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return productAttribute?.Product;
        }
    }
}

