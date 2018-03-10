using System;
using System.Collections.Generic;
using Moviebase.Core.Diagnostics;
using Moviebase.Entities;

namespace Moviebase.Core.MVP
{
    public class ValidationSupport
    {
        private Dictionary<Func<bool>, string> _conditionDictionary;
        private Action<string> _failAction;
        
        public ValidationSupport()
        {
            _conditionDictionary = new Dictionary<Func<bool>, string>();
        }
        
        public void SetCommonFailAction(Action<string> action)
        {
            _failAction = action;
        }

        public ValidationSupport IsTrue(Func<bool> condition, string failMessage)
        {
            _conditionDictionary.Add(condition, failMessage);
            return this;
        }

        public ValidationSupport EnsureInternetConnected()
        {
            return IsTrue(() => NetworkObserver.Instance.IsInternetConnected(), Strings.NoInternetMessage);
        }

        public bool Validate()
        {
            foreach (var condition in _conditionDictionary)
            {
                if (condition.Key.Invoke()) continue;
                if (condition.Value != null) _failAction.Invoke(condition.Value);
                return false;
            }
            return true;
        }

        public static bool QuickTrue(Func<bool> condition, string failLog)
        {
            var result = condition.Invoke();
            return result;
        }
    }
}
