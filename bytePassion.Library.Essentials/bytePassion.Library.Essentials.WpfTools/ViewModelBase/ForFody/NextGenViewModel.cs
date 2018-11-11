using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using bytePassion.Library.Essentials.Tools.FrameworkExtensions;
using bytePassion.Library.Essentials.WpfTools.ViewModelBase.Standard;

namespace bytePassion.Library.Essentials.WpfTools.ViewModelBase.ForFody
{
    public abstract class NextGenViewModel : DisposingObject, IViewModel
    {
        protected void NotifyPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        protected void ChangeAndNofityPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}