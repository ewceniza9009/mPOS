using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace mPOSv2.Utilities
{
    public class Util<T>
    {
        public static List<T> ConvertToList(ObservableCollection<mPOS.POCO.TrnSales> obj)
        {
            var serializedObject = JsonConvert.SerializeObject(obj);
            var convertedOutput = JsonConvert.DeserializeObject<List<T>>(serializedObject);

            return convertedOutput;
        }
    }
}
