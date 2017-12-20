using System.Windows.Forms;

namespace BlastMVP
{
    public interface IBindingTarget
    {
        IBindableComponent Control { get; set; }
        string PropertyName { get; set; }
    }
}