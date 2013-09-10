using System;

namespace GenerateText.Model
{
    public sealed class DataService : IDataService
    {

        void IDataService.GetData(Action<DataItem, Exception> callback)
        {
            // Use this to connect to the actual data service

            var item = new DataItem();

            item.LastPattern = Properties.Settings.Default.LastPattern;
            item.LastCount = Properties.Settings.Default.LastCount;
            item.LastQa = Properties.Settings.Default.LastQa;
            item.LastCountList = Properties.Settings.Default.LastCountList;

            callback(item, null);
        }

        void IDataService.Save(DataItem item)
        {
            Properties.Settings.Default.LastCount = item.LastCount;
            Properties.Settings.Default.LastCountList = item.LastCountList;
            Properties.Settings.Default.LastPattern = item.LastPattern;
            Properties.Settings.Default.LastQa = item.LastQa;
            Properties.Settings.Default.Save();
        }

    }
}