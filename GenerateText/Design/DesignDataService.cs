using System;
using GenerateText.Model;

namespace GenerateText.Design
{
    public sealed class DesignDataService : IDataService
    {
        void IDataService.GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem();
            item.LastPattern = "Patrice";
            item.LastCount = 12;
            item.LastQa = true;
            item.LastCountList = "5 10 15 20";
            callback(item, null);
                       
        }

        void IDataService.Save(DataItem item)
        {
            
        }

    }
}