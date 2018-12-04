﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace FluentCRM.Interfaces
{
    /// <summary>
    /// Interface to use where we are looking to create a new entity.
    /// </summary>
    public interface ICreateEntity
    {
        IOrganizationService Service { get; }

        ICreateEntity Id(Action<EntityReference> returnNewId);
        ICreateEntity Create(Dictionary<string, Object> attributes);
        ICreateEntity CreateOptionSets(Dictionary<string, string> attributes);

        void Execute( Action preExecute, Action<int,int> postExecute );
    }
}