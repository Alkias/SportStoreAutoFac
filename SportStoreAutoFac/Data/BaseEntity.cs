using System.ComponentModel.DataAnnotations;

namespace SportStoreAutoFac.Data
{
    /// <summary>
    /// Represents the base class for entities
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}