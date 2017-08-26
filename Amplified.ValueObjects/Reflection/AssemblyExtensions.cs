using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Amplified.ValueObjects.Reflection
{
    public static class AssemblyExtensions
    {
        public static Assembly LoadReferences(this Assembly assembly)
        {
            var loadedAssemblies = new HashSet<string>();
            var assemblies = new Stack<AssemblyName>();

            foreach (var asm in assembly.GetReferencedAssemblies())
                assemblies.Push(asm);
            
            while (assemblies.Count > 0)
            {
                var asm = assemblies.Pop();
                if (loadedAssemblies.Contains(asm.FullName))
                    continue;

                var references = LoadAssembly(asm);
                foreach (var reference in references)
                    assemblies.Push(reference);
                
                loadedAssemblies.Add(asm.FullName);
            }

            return assembly;
        }

        private static IEnumerable<AssemblyName> LoadAssembly(AssemblyName assembly)
        {
            try
            {
                var loaded = Assembly.Load(assembly);
                return loaded.GetReferencedAssemblies();
            }
            catch (FileNotFoundException)
            {
                return new AssemblyName[0];
            }
            catch (FileLoadException)
            {
                return new AssemblyName[0];
            }
        }
    }
}