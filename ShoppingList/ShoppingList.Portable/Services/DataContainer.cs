namespace ShoppingList.Portable.Services
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using ShoppingList.Portable.Models;

    [DataContract]
    public class DataContainer
    {
        [DataMember]
        public List<Entry> Entries { get; set; }
    }
}