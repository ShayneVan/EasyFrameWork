﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.Practices.ServiceLocation;

namespace Easy.IOC.Autofac
{
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {
        readonly IComponentContext _container;

        public AutofacServiceLocator(IComponentContext container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
                return key != null ? _container.ResolveNamed(key, serviceType) : _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                var enumerableType = typeof (IEnumerable<>).MakeGenericType(serviceType);
                object instance = _container.Resolve(enumerableType);
                return ((IEnumerable) instance).Cast<object>();
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}