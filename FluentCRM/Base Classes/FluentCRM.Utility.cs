﻿using System;
using FluentCRM.Interfaces;
using Microsoft.Xrm.Sdk.Query;

namespace FluentCRM.Base_Classes
{
    public abstract partial class FluentCRM
    {
        #region "Ordering and Counting"

        ICanExecute ICanExecute.Distinct()
        {
            _distinct = true;
            return this;
        }

        IEntitySet IEntitySet.Distinct()
        {
            return (IEntitySet) ((ICanExecute) this).Distinct();
        }

        private ICanExecute Order(string attribute, OrderType orderingType)
        {
            _orders.Add(new OrderExpression
            {
                AttributeName = attribute,
                OrderType = orderingType
            });
            return this;
        }

        ICanExecute ICanExecute.OrderByDesc(string attribute)
        {
            return Order(attribute, OrderType.Descending);
        }

        public IAnotherWhere And
        {
            get
            {
                Trace("And...");
                return this;
            }
        }

        ICanExecute ICanExecute.OrderByAsc(string attribute)
        {
            return Order(attribute, OrderType.Ascending);
        }

        IEntitySet IEntitySet.OrderByAsc(string attribute)
        {
            return (IEntitySet) Order(attribute, OrderType.Ascending);
        }

        IEntitySet IEntitySet.OrderByDesc(string attribute)
        {
            return (IEntitySet) Order(attribute, OrderType.Descending);
        }

        ICanExecute ICanExecute.Exists(Action<bool> action)
        {
            _actionList.Add(new Tuple<string[], Func<EntityWrapper, string, bool?>>(
                new string[] {"createdon"},
                (entity, c) => true));
            _postExecuteActions.Add(() =>
            {
                var exists = (_entities?.Count > 0);
                Trace($"Called exists(): {exists}");

                action?.Invoke(exists);
            });
            return this;
        }

        ICanExecute ICanExecute.Exists(Action whenTrue, Action whenFalse)
        {
            return ((ICanExecute) this).Exists(c =>
            {
                if (c)
                {
                    whenTrue?.Invoke();
                }
                else
                {
                    whenFalse?.Invoke();
                }
            });
        }

        ICanExecute IEntitySet.Count(Action<int?> action)
        {
            _postExecuteActions.Add(() =>
                {
                    Trace($"Count={_processedEntityCount}");
                    action(_processedEntityCount);
                }
            );
            return this;
        }

        ICanExecute ICanExecute.Count(Action<int?> action)
        {
            return ((IEntitySet) this).Count(action);
        }

        ICanExecute IEntitySet.Exists(Action<bool> action)
        {
            return ((ICanExecute) this).Exists(action);
        }

        ICanExecute IEntitySet.Exists(Action whenTrue, Action whenFalse)
        {
            return ((ICanExecute) this).Exists(whenTrue, whenFalse);
        }

        #endregion
    }
}