namespace ShoppingList.Portable.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    using PCLStorage;

    using ShoppingList.Portable.Models;

    public class DataService
    {
        private static readonly DataContractSerializer DataContractSerializer;

        private const string FileName = "data.xml";

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);

        static DataService()
        {
            DataContractSerializer = new DataContractSerializer(typeof(DataContainer));
        }

        public event Action ListChanged;

        public async Task<List<Entry>> GetAsync()
        {
            await this.semaphore.WaitAsync();
            try
            {
                return await this.LoadDataAsync();
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        public async Task AddAsync(Entry model)
        {
            await this.semaphore.WaitAsync();
            try
            {
                var list = await this.LoadDataAsync();
                model.Id = Guid.NewGuid();
                list.Add(model);
                await this.SaveDataAsync(list);
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        public async Task UpdateAsync(Entry model)
        {
            await this.semaphore.WaitAsync();
            try
            {
                var list = await this.LoadDataAsync();
                var existingEntry = list.First(entry => entry.Id == model.Id);
                existingEntry.Amount = model.Amount;
                existingEntry.Description = model.Description;
                await this.SaveDataAsync(list);
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        public async Task RemoveAsync(Entry model)
        {
            await this.semaphore.WaitAsync();
            try
            {
                var list = await this.LoadDataAsync();
                var existingEntry = list.First(entry => entry.Id == model.Id);
                list.Remove(existingEntry);
                await this.SaveDataAsync(list);
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        public async Task RemoveAllAsync()
        {
            await this.semaphore.WaitAsync();
            try
            {
                await this.SaveDataAsync(new List<Entry>());
            }
            finally
            {
                this.semaphore.Release();
            }
        }

        private void NotifyListChanged()
        {
            var handler = this.ListChanged;
            if (handler != null)
            {
                handler();
            }
        }

        #region File Handling
        private async Task<List<Entry>> LoadDataAsync()
        {
            if ((await FileSystem.Current.LocalStorage.CheckExistsAsync(FileName)) == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(FileName);
                using (var stream = await file.OpenAsync(FileAccess.Read))
                {
                    using (var xmlReader = XmlReader.Create(stream))
                    {
                        var dataContainer = DataContractSerializer.ReadObject(xmlReader) as DataContainer;
                        if (dataContainer != null && dataContainer.Entries.Any())
                        {
                            return dataContainer.Entries;
                        }
                    }
                }
            }

            return await this.GetDummyDataAsync();
        }

        private async Task SaveDataAsync(List<Entry> entries)
        {
            var file = await FileSystem.Current.LocalStorage.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                using (var xmlWriter = XmlWriter.Create(stream))
                {
                    DataContractSerializer.WriteObject(xmlWriter, new DataContainer { Entries = entries });
                }
            }

            this.NotifyListChanged();
        }
        #endregion File Handling

        #region Dummy Data

        private bool once;

        private async Task<List<Entry>> GetDummyDataAsync()
        {
            if (this.once)
            {
                return new List<Entry>();
            }

            this.once = true;

            var list = new List<Entry>
                       {
                           new Entry { Id = Guid.NewGuid(), Description = "Intel Xeon E5-2698V3 16 CORE CPU", Amount = 2 },
                           new Entry { Id = Guid.NewGuid(), Description = "Kühlerpaste 20ml Tube", Amount = 2 },
                           new Entry { Id = Guid.NewGuid(), Description = "AB-14 Kühlkörper", Amount = 2 },
                           new Entry { Id = Guid.NewGuid(), Description = "16GB DDR4 RAM Riegel", Amount = 20 },
                           new Entry { Id = Guid.NewGuid(), Description = "1000W Netzteil 90+", Amount = 1 },
                           new Entry { Id = Guid.NewGuid(), Description = "RJ45 CAT8 Kabel 0,5m", Amount = 12 },
                           new Entry { Id = Guid.NewGuid(), Description = "RJ45 CAT8 Kabel 1m", Amount = 18 },
                           new Entry { Id = Guid.NewGuid(), Description = "RJ45 CAT8 Kabel 2m", Amount = 5 },
                           new Entry { Id = Guid.NewGuid(), Description = "RJ45 CAT8 Kabel 10m", Amount = 8 },
                           new Entry { Id = Guid.NewGuid(), Description = "RJ45 CAT8 Kabel 20m", Amount = 7 },
                           new Entry { Id = Guid.NewGuid(), Description = "16 Slot Router", Amount = 1 },
                           new Entry { Id = Guid.NewGuid(), Description = "Ersatz Blade-Frontblende", Amount = 1 },
                           new Entry { Id = Guid.NewGuid(), Description = "6TB Enterprise Hard Disk SAS2", Amount = 8 },
                           new Entry { Id = Guid.NewGuid(), Description = "1TB Enterprise SSD SATA-III", Amount = 2 },
                           new Entry { Id = Guid.NewGuid(), Description = "1TB Enterprise SSD PCIe", Amount = 1 },
                           new Entry { Id = Guid.NewGuid(), Description = "ESX Lizenz", Amount = 2 },
                           new Entry { Id = Guid.NewGuid(), Description = "MS SQL Server Kernlizenz", Amount = 16 },
                           new Entry { Id = Guid.NewGuid(), Description = "MS Windows Server Core Lizenz", Amount = 2 },
                       };

            await this.SaveDataAsync(list);

            return list;
        }

        #endregion Dummy Data
    }
}
