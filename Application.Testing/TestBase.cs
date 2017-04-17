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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.Testing
{
    [TestClass]
    [DeploymentItem("sqlite3.dll")]
	[DeploymentItem("System.Data.SQLite.dll")]
    [DeploymentItem("Solar.Core.Windsor.xml")]
    public abstract class TestBase
    {
        public TestBase()
        {
            Infrastructure.Instance.Prepare();
        }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Infrastructure.Instance.ResetPersistenceStorage();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
        }
    }
}
