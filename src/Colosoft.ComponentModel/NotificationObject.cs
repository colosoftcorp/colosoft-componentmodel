using System;

namespace Colosoft
{
    [Serializable]
    public abstract class NotificationObject : System.ComponentModel.INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(
            [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                throw new ArgumentNullException(nameof(propertyNames));
            }

            foreach (var name in propertyNames)
            {
                this.OnPropertyChanged(name);
            }
        }
    }
}
