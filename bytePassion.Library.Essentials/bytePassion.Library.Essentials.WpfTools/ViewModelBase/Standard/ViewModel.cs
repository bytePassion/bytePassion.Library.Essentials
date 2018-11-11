using System.ComponentModel;
using bytePassion.Library.Essentials.Tools.FrameworkExtensions;

namespace bytePassion.Library.Essentials.WpfTools.ViewModelBase.Standard
{
	public abstract class ViewModel : DisposingObject, 
                                      IViewModel                                      
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
}