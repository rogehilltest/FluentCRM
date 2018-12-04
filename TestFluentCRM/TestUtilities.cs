﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace TestFluentCRM
{
    public class TestUtilities
    {
        public static XrmFakedContext TestContext1()
        {
            var account1= new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["name"] = "Account1",
                ["phone1"] = "123456",
                ["address1_country"] = "UK",
                ["statecode"] = 0
            };

            var account2= new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["name"] = "Account2",
                ["phone1"] = "654321",
                ["address1_country"] = "UK",
                ["statecode"] = 0
            };

            var account3= new Entity("account")
            {
                Id = Guid.NewGuid(),
                ["name"] = "Account3",
                ["phone1"] = "222333",
                ["address1_country"] = "US",
                ["statecode"] = 0
            };

            var contact1 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["firstname"] = "John",
                ["lastname"] = "Doe",
                ["parentcustomerid"] = account1.ToEntityReference(),
                ["telephone1"] = "12345677",
                ["telephone2"] = "23456789",
                ["phone"] = "776543212"
            };

            var contact2 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["firstname"] = "Sam",
                ["lastname"] = "Spade",
                ["parentcustomerid"] = account1.ToEntityReference(),
                ["telephone2"] = "3456789",
                ["phone"] = "76543212"
            };

            var contact3 = new Entity("contact")
            {
                Id = Guid.NewGuid(),
                ["firstname"] = "John",
                ["lastname"] = "Watson",
                ["parentcustomerid"] = account2.ToEntityReference(),
                ["telephone1"] = "34567789",
                ["phone"] = "796543212"
            };

            var context = new XrmFakedContext();
            context.Initialize(new List<Entity>{ account1, account2, account3, contact1, contact2, contact3 });

            return context;
        }
    }
}