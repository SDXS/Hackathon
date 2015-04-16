namespace ShoppingList.Portable.Models
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Entry
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Amount { get; set; }
    }
}
