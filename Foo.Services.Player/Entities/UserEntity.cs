using Microsoft.WindowsAzure.Storage.Table;

namespace Foo.Services.User.Entities
{
    /// <summary>
    /// The User Entity Model.
    /// </summary>
    public class UserEntity : TableEntity
    {
        /// <summary>
        /// The User Entity Constructor.
        /// </summary>
        /// <param name="sKey">The partition key</param>
        /// <param name="sRow">The Row key</param>
        public UserEntity(string sKey, string sRow)
        {
            this.PartitionKey = sKey;
            this.RowKey = sRow;
        }

        /// <summary>
        /// The User Entity Constructor.
        /// </summary>
        public UserEntity() { }

        /// <summary>
        /// The User Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The User Rating.
        /// </summary>
        public int Rating { get; set; }
        

        /// <summary>
        /// The User status.
        /// </summary>
        public int Status { get; set; }
    }
}
