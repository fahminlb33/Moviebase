using System.Windows.Forms;

namespace Moviebase.Core.MVP
{
    public interface IBindingTarget
    {
        IBindableComponent Control { get; set; }
        string PropertyName { get; set; }
    }
}