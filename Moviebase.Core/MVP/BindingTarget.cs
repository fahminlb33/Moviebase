using System.Windows.Forms;

namespace Moviebase.Core.MVP
{
    public class BindingTarget : IBindingTarget
    {
        public string PropertyName { get; set; }
        public IBindableComponent Control { get; set; }
    }
}