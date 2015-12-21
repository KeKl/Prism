using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Prism.Perspex.Tests
{
    [TestFixture]
    public class ServiceLocatorExtensionsFixture
    {
        [Test]
        public void TryResolveShouldReturnNullIfNotFound()
        {
            IServiceLocator sl = new MockServiceLocator(() => null);

            object value = sl.TryResolve(typeof(ServiceLocatorExtensionsFixture));

            Assert.IsNull(value);
        }

        [Test]
        public void ShouldResolveFoundtypes()
        {
            IServiceLocator sl = new MockServiceLocator(() => new ServiceLocatorExtensionsFixture());

            object value = sl.TryResolve(typeof(ServiceLocatorExtensionsFixture));

            Assert.IsNotNull(value);
        }

        [Test]
        public void GenericTryResolveShouldReturn()
        {
            IServiceLocator sl = new MockServiceLocator(() => new ServiceLocatorExtensionsFixture());

            ServiceLocatorExtensionsFixture value = sl.TryResolve<ServiceLocatorExtensionsFixture>();

            Assert.IsNotNull(value);
        }

        internal class MockServiceLocator : ServiceLocatorImplBase
        {
            public Func<object> ResolveMethod;

            public MockServiceLocator(Func<object> resolveMethod)
            {
                ResolveMethod = resolveMethod;
            }

            protected override object DoGetInstance(Type serviceType, string key)
            {
                object instance = ResolveMethod();

                // If the instance was not found, throw exception.
                if (instance.GetType() != serviceType)
                {
                    throw new ActivationException("Type not found.");
                }
                else
                {
                    return instance;
                }
            }

            protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            {
                return null;
            }
        }
    }
}