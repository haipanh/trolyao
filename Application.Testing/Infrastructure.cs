/* 
 * Copyright (c) 2013 by TMC - TMA Microsoft Center
 * Floor 8, TMA Building - Quang Trung Software City
 * District 12, Ho Chi Minh City, Vietnam
 * Email: tmc@tma.com.vn
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Solar.Infrastructure.RepositoryConfiguration;

namespace Application.Testing
{
    internal class Infrastructure
    {
        #region Singleton Representation

        static Infrastructure()
        {
            Instance = new Infrastructure();
        }

        public static Infrastructure Instance { get; private set; }

        private Infrastructure()
        {
            var pathToAssembly = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var pathToWindsorConfigurationFile = Path.Combine(Path.GetDirectoryName(pathToAssembly),
                                                              "Solar.Core.Windsor.xml");

            this.container = new WindsorContainer(new XmlInterpreter(pathToWindsorConfigurationFile));
        }

        #endregion

        private readonly IWindsorContainer container;

        public void Prepare()
        {
            this.container.GetService<RepositoryFactoryBuilderBase>().BuildRepositoryFactory();
        }

        public void ResetPersistenceStorage()
        {
            this.container.GetService<RepositoryFactoryBuilderBase>().ExportSchema();
        }
    }
}
