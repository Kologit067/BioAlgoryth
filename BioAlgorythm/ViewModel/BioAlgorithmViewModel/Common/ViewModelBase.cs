using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioAlgorythmViewModel.Common
{
    // Summary:
    //     A base class for the ViewModel classes in the MVVM pattern.
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {

        public bool ThrowOnInvalidPropertyName { get; set; }
        // Summary:
        //     Initializes a new instance of the ViewModelBase class.
        public ViewModelBase()
        {
            ThrowOnInvalidPropertyName = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected virtual void OnPropertyChanging(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangingEventHandler handler = this.PropertyChanging;
            if (handler != null)
            {
                var e = new PropertyChangingEventArgs(propertyName);
                handler(this, e);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
    }
}
