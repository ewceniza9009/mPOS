using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace mPOSv2.Services
{
    public class PropertyChangeTracker
    {
        public List<PropertyChangedInfo> ChangedProperties;
        private INotifyPropertyChanged _vmInstance;
        private string PropertyFilterName;

        public PropertyChangeTracker(INotifyPropertyChanged vmInstance, string propNameToTrack = "")
        {
            PropertyFilterName = propNameToTrack;
            ChangedProperties = new List<PropertyChangedInfo>();
            _vmInstance = vmInstance;
            _vmInstance.PropertyChanged += Tracker;
        }

        private void Tracker(object sender, PropertyChangedEventArgs e)
        {
            var prop = sender.GetType().GetProperty(e.PropertyName);
            var value = prop?.GetValue(sender);
            var changedProp = new PropertyChangedInfo()
            {
                PropertyName = prop?.Name,
                Value = value
            };

            if (PropertyFilterName != string.Empty)
            {
                if (PropertyFilterName == prop?.Name)
                {
                    ChangedProperties.Add(changedProp);
                }
            }
            else
            {
                ChangedProperties.Add(changedProp);
            }
        }
    }

    public class PropertyChangedInfo
    {
        public string PropertyName { get; set; }
        public object Value { get; set; }
    }
}
