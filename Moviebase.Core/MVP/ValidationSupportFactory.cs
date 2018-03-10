using System;

namespace Moviebase.Core.MVP
{
    public class ValidationSupportFactory
    {
        private Action<string> _failAction;

        public ValidationSupportFactory(Action<string> action)
        {
            _failAction = action;
        }

        public ValidationSupport Create()
        {
            var support = new ValidationSupport();
            support.SetCommonFailAction(_failAction);
            return support;
        }
    }
}
