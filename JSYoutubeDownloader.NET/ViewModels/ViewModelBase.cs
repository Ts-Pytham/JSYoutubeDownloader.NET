using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JSYoutubeDownloader.NET.ViewModels
{
    internal abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}
