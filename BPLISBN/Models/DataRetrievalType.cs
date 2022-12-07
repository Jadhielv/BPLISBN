using System.ComponentModel;

namespace BPLISBN.Models
{
    public enum DataRetrievalType
    {
        [Description("Server")]
        Server = 1,
        [Description("Cache")]
        Cache = 2
    }
}
