using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace YetAnotherDependencyInjector
{
    public static class Injector
    {
        private static List<KeyValuePair<Type, Type>> mappings
            = new List<KeyValuePair<Type, Type>>();

        private static Dictionary<string, Type> stringMappingDependencies = new Dictionary<string, Type>();
        private static int indexDependencies = 0;

        public static void Register<T, V>(params string[] name) where V : T
        {
            mappings.Add(new KeyValuePair<Type, Type>(typeof(T), typeof(V)));
            if (name.Length == 0 || name.Length > 1) return;
            stringMappingDependencies.Add(name.FirstOrDefault(), typeof(V));
        }

        public static T Get<T>(params string[] dependencies)
        {
            var type = typeof(T);
            return (T)Get(type, dependencies);
        }

        private static object Get(Type type, params string[] dependencies)
        {
            var target = ResolveType(type, dependencies);

            var constructor = target.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            List<object> resolvedParameters = new List<object>();

            foreach (var item in parameters)
            {
                resolvedParameters.Add(Get(item.ParameterType, dependencies));
            }

            return constructor.Invoke(resolvedParameters.ToArray());
        }

        private static Type ResolveType(Type type, string[] dependencies)
        {
            if (dependencies.Length > 0 && type.IsInterface)
            {
                var mappedType = TryGetMappedTypeByArrayNames(type, dependencies);
                indexDependencies++;
                return mappedType;
            }

            return mappings.FirstOrDefault(x => x.Key == type).Value ?? type;
        }

        private static Type TryGetMappedTypeByArrayNames(Type type, string[] dependencies)
        {
            Type mappedType;
            stringMappingDependencies.TryGetValue(dependencies[indexDependencies], out mappedType);

            if (mappedType == null)
                throw new ArgumentException(String.Format("No existen componentes registrados con el nombre {0} para la interfaz {1}", dependencies[indexDependencies], type.Name));
            if (!mappedType.GetTypeInfo().ImplementedInterfaces.Contains(type))
                throw new ArgumentException(String.Format("El componente con nombre {0} no implementa la interfaz {1}", dependencies[indexDependencies], type.Name));

            return mappedType;
        }

        public static void Clear()
        {
            mappings.Clear();
            stringMappingDependencies.Clear();
            indexDependencies = 0;
        }

    }
}
