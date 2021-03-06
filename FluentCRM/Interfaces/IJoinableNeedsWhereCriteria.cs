﻿using Microsoft.Xrm.Sdk.Query;

namespace FluentCRM
{
    /// <summary>
    /// Interface used where FluentCRM requires that some kind of criteria be specified following a Where() clause.
    /// </summary>
    public interface IJoinableNeedsWhereCriteria
    {
        /// <summary>
        /// Add criteria that the Where-attribute equals the given value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="value">Select entity records where the given attribute equals this value.</param>
        /// <typeparam name="T">The type of the value being compared to.</typeparam>
        IJoinableEntitySet Equals<T>(T value);

        /// <summary>
        /// Add criteria that the Where-attribute does not equal the given value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="value">Select entity records where the given attribute does not equal this value.</param>
        /// <typeparam name="T">The type of the value being compared to.</typeparam>
        IJoinableEntitySet NotEqual<T>(T value);

        /// <summary>
        /// Add criteria that the Where-attribute is not null
        /// </summary>
        /// <returns>FluentCRM object</returns>
        IJoinableEntitySet IsNotNull { get; }

        /// <summary>
        /// Add criteria that the Where-attribute is null
        /// </summary>
        /// <returns>FluentCRM object</returns>
        IJoinableEntitySet IsNull { get; }

        /// <summary>
        /// Add criteria that the Where-attribute is in the given set of values
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="inVals">Select entity records where the given attribute value is in the given set of values</param>
        IJoinableEntitySet In<T>(params T[] inVals);

        /// <summary>
        /// Add criteria that the Where-attribute is greater than the given value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="value">Select entity records where the given attribute is greater than this value.</param>
        /// <typeparam name="T">The type of the value being compared to.</typeparam>
        IJoinableEntitySet GreaterThan<T>(T value);

        /// <summary>
        /// Add criteria that the Where-attribute is less than the given value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="value">Select entity records where the given attribute is less than this value.</param>
        /// <typeparam name="T">The type of the value being compared to.</typeparam>
        IJoinableEntitySet LessThan<T>(T value);

        /// <summary>
        /// Add criteria that the Where-attribute begins with the given value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="s">Select entity records where the given attribute starts with this value.</param>
        IJoinableEntitySet BeginsWith(string s);

        /// <summary>
        /// Add criteria that the Where-attribute matches the given condition and value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="op">ConditionalOperator from the CRM SDK.</param>
        IJoinableEntitySet Condition(ConditionOperator op );   

        /// <summary>
        /// Add criteria that the Where-attribute matches the given condition and value
        /// </summary>
        /// <returns>FluentCRM object</returns>
        /// <param name="op">ConditionalOperator from the CRM SDK.</param>
        /// <param name="value">Select entity records where the given attribute and conition matches this value.</param>
        /// <typeparam name="T">The type of the value being compared to.</typeparam>
        IJoinableEntitySet Condition<T>(ConditionOperator op, T value);    }
}