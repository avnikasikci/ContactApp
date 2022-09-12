using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Core.Application.Core
{
    
    public class DependencyResolver
    {
        private static DependencyResolver _Current;

        public static DependencyResolver Current
        {
            get
            {
                return _Current;
            }
        }


        private readonly IDepencyLifetimeScope _RootLifetimeScope;

        public static void SetLifetimeScope(IDepencyLifetimeScope RootLifetimeScope)
        {
            _Current = new DependencyResolver(RootLifetimeScope);
        }

        public DependencyResolver(IDepencyLifetimeScope RootLifetimeScope)
        {
            _RootLifetimeScope = RootLifetimeScope;
        }

        public IDepencyLifetimeScope BeginLifetimeScope()
        {
            return _RootLifetimeScope.BeginLifetimeScope();
        }
        public IDepencyLifetimeScope BeginLifetimeScope(object Tag)
        {
            return _RootLifetimeScope.BeginLifetimeScope(Tag);
        }
    }

    public class DepencyLifetimeScope : IDepencyLifetimeScope
    {
        private readonly ILifetimeScope _LifetimeScope;

        public DepencyLifetimeScope(ILifetimeScope LifetimeScope)
        {
            _LifetimeScope = LifetimeScope;
        }

        public IDepencyLifetimeScope BeginLifetimeScope()
        {
            return new DepencyLifetimeScope(_LifetimeScope.BeginLifetimeScope());
        }
        public IDepencyLifetimeScope BeginLifetimeScope(object Tag)
        {
            return new DepencyLifetimeScope(_LifetimeScope.BeginLifetimeScope(Tag));
        }

        public object Resolve(Type type)
        {
            return _LifetimeScope.Resolve(type);
        }

        public T Resolve<T>()
        {
            return _LifetimeScope.Resolve<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            Type enumerableOfType = typeof(IEnumerable<>).MakeGenericType(type);
            return (object[])_LifetimeScope.ResolveService(new TypedService(enumerableOfType));
        }
        public IEnumerable<T> ResolveAll<T>()
        {
            return _LifetimeScope.Resolve<IEnumerable<T>>();
        }

        public void Dispose()
        {
            _LifetimeScope.Dispose();
        }
    }

    public interface IDepencyLifetimeScope : IDisposable
    {
        IDepencyLifetimeScope BeginLifetimeScope();
        IDepencyLifetimeScope BeginLifetimeScope(object Tag);
        object Resolve(Type type);
        T Resolve<T>();
        IEnumerable<object> ResolveAll(Type type);
        IEnumerable<T> ResolveAll<T>();
    }
}
