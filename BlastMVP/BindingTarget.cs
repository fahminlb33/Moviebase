using System.Windows.Forms;

namespace BlastMVP
{
    public class BindingTarget : IBindingTarget
    {
        public string PropertyName { get; set; }
        public IBindableComponent Control { get; set; }
    }
}