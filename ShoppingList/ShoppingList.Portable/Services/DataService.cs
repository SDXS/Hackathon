namespace ShoppingList.Portable.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using System.Xml;

    using PCLStorage;

    using ShoppingList.Portable.Models;

    public class DataService
    {
        private static readonly DataContractSerializer dataContractSerializer;

        private const string fileName = "data.sl";

        static DataService()
        {
            dataContractSerializer = new DataContractSerializer(typeof(DataContainer));
        }

        public async Task SaveData(List<Entry> entries)
        {
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                using (var xmlWriter = XmlWriter.Create(stream))
                {
                    dataContractSerializer.WriteObject(xmlWriter, new DataContainer { Entries = entries });
                }
            }
        }

        public async Task<List<Entry>> LoadData()
        {
            if ((await FileSystem.Current.LocalStorage.CheckExistsAsync(fileName)) == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
                using (var stream = await file.OpenAsync(FileAccess.Read))
                {
                    using (var xmlReader = XmlReader.Create(stream))
                    {
                        var dataContainer = dataContractSerializer.ReadObject(xmlReader) as DataContainer;
                        if (dataContainer != null)
                        {
                            return dataContainer.Entries;
                        }
                    }
                }
            }

            return null;
        }
    }
}
