using System.Collections.Generic;

namespace ExplodingElves
{
    public static class ServiceLocator
    {
        private struct ServiceEntry
        {
            public object Provider;
            public object SecurityKey;
        }

        private static readonly Dictionary<int, ServiceEntry> Services = new();

        public static void SetService<T>(T provider, object securityKey = default) where T : class
        {
            int hashCode = typeof(T).GetHashCode();

            if (Services.TryGetValue(hashCode, out ServiceEntry entry) && entry.SecurityKey != null && entry.SecurityKey != securityKey)
            {
                return;
            }

            ServiceEntry newEntry = new()
            {
                Provider = provider,
                SecurityKey = securityKey,
            };

            Services[hashCode] = newEntry;
        }

        public static bool ServiceExists<T>() where T : class
        {
            return Services.ContainsKey(typeof(T).GetHashCode());
        }

        public static bool TryGetService<T>(out T service) where T : class
        {
            if (Services.TryGetValue(typeof(T).GetHashCode(), out ServiceEntry entry))
            {
                service = (T)entry.Provider;

                return true;
            }

            service = default;

            return false;
        }

        public static T GetService<T>() where T : class
        {
            return (T)Services[typeof(T).GetHashCode()].Provider;
        }
    }
}
