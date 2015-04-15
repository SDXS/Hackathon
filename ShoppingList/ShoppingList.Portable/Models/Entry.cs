namespace ShoppingList.Portable.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Entry
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Amount { get; set; }
    }
}
