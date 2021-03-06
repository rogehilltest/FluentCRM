﻿using System;
using System.Diagnostics;
using System.Linq;
using FakeXrmEasy;
using FluentCRM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace TestFluentCRM
{
    [TestClass]
    public class Test1_IUnknownEntity
    {
        private XrmFakedContext _context;
        private IOrganizationService _orgService;

        [TestInitialize]
        public void SetUp()
        {
            _context = TestUtilities.TestContext1();
            _orgService = _context.GetOrganizationService();
        }

        /// <summary>
        ///  Test IUnknownEntity Interface with Mock CRM Organization service.
        /// </summary>
        [TestMethod]
        public void TestService1()
        {
            var f = FluentAccount.Account(_orgService);

            var service2 = f.Service;

            Assert.AreSame(_orgService, service2);
        }

        [TestMethod]
        public void TestService2()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;

            var f = FluentAccount.Account();

            var service2 = f.Service;

            Assert.AreSame(_orgService, service2);
        }

        [TestMethod]
        public void TestService3()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;

            var account1 = _context.Data["account"].First().Value;
            var accountId = account1.Id;
            EntityWrapper ew = null;
            FluentAccount.Account(accountId).UseEntity( e =>  ew = e, "name").Execute();

            Assert.IsNotNull(ew);
            Assert.AreEqual( account1.Id, ew.Id);
        }

        [TestMethod]
        public void TestWhere1()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;

            int? count = 0;
            FluentAccount.Account().Where("name").Equals("Account1").Count(c=> count= c).Execute();

            Assert.IsTrue(count.HasValue);
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestWhere2()
        {
            int? count = 0;
            FluentAccount.Account(_orgService).Where("name").Equals("Account1").Count(c=> count= c).Execute();

            Assert.IsTrue(count.HasValue);
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void TestId1()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;
            var account1 = _context.Data["account"].First().Value;
            var accountId = account1.Id;
            var name = String.Empty;
            FluentAccount.Account(accountId).UseAttribute<string>( n => name = n,"name").Execute();

            Assert.AreEqual(account1["name"], name);
        }

        [TestMethod]
        public void TestId2()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;
            var account1 = _context.Data["account"].First().Value;
            var accountId = account1.Id;
            var name = String.Empty;
            FluentAccount.Account().Id(accountId).UseAttribute( (string n) => name = n,"name").Execute();

            Assert.AreEqual(account1["name"], name);
        }

        [TestMethod()]
        public void TestId3()
        {
            FluentCRM.FluentCRM.StaticService = _orgService;
            var account1 = _context.Data["account"].First().Value;
            var accountId = account1.Id;
            var name = String.Empty;
            FluentAccount.Account(accountId).UseAttribute( (string n) => name = n,"name", "name2").Execute();
            Assert.AreEqual(account1["name"], name);

            FluentAccount.Account(accountId).UseAttribute( (string n) => name = n,"name2", "name").Execute();
            Assert.AreEqual(account1["name2"], name);

            FluentAccount.Account(accountId).UseAttribute( (string n) => name = n,"name3", "name2", "name").Execute();
            Assert.AreEqual(account1["name3"], name);

            FluentAccount.Account(accountId).UseAttribute( (string n) => name = n,"name4", "name2", "name").Execute();
            Assert.AreEqual(account1["name2"], name);

            FluentAccount.Account(accountId).UseAttribute( (string n) => name = n,"name4", "name5", "name2").Execute();
            Assert.AreEqual(account1["name2"], name);
        }

        [TestMethod]
        public void TestTrace1()
        {
            var message = string.Empty;
            FluentAccount.Account(_orgService).Trace(m =>
            {
                message = m;
                Debug.WriteLine(m);
            }).Where("name").Equals("Account1").UseAttribute( (string a) => Debug.Write($"Name is {a}"), "name").Execute();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(message));
        }

        [TestMethod]
        public void TestTimer1()
        {
            var message = string.Empty;
            FluentAccount.Account(_orgService).Timer( m =>
            {
                message = m;
                Debug.WriteLine($@"Timer message: {m}");
            }).Where("name").Equals("Account1").UseAttribute( (string a) => Debug.Write($"Name is {a}"), "name").Execute();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(message));
        }

        [TestMethod]
        public void TestJoin1()
        {
            var message = string.Empty;
            FluentAccount.Account(_orgService).Trace(s => Debug.WriteLine(s))
                .Join<FluentContact>(c => c.Where("firstname").Equals("Sam")
                    .UseAttribute((string n) =>
                    {
                        Debug.WriteLine($"Contact name is {n}");
                        message = "Join succeeded";
                    }, "firstname"))
                    .UseAttribute((string a) => Debug.Write($"Account Name is {a}"), "name")
                .Count(c => Assert.AreEqual(1, c)).Execute();

            Assert.IsTrue(!string.IsNullOrWhiteSpace(message));
        }

    }
}
