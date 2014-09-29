using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    /// <summary>
    /// Base class that all other entities in the domain should inherit from.
    /// We want every class to have common properties.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Set the Id and created date for new objects
        /// </summary>
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; private set; }

        [Required]
        public DateTime CreatedDate { get; private set; }

        [Required]
        public string CreatedByUserId { get; set; }

        [ForeignKey("CreatedByUserId")]
        public User CreatedByUser { get; set; }
    }
}
