using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class Job:EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        /// <summary>
        /// We will only allow one main image for the job.
        /// </summary>
        public Guid? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
        /// <summary>
        /// Has a many-to-many table JobTags
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Has a many-to-many table JobTools
        /// </summary>
        public virtual ICollection<Tool> Tools { get; set; }

        /// <summary>
        /// Has a many-to-many table JobParts
        /// </summary>
        public virtual ICollection<Part> Parts { get; set; }

        public virtual ICollection<JobLike> Likes { get; set; }

        public virtual ICollection<JobComment> Comments { get; set; }

        public virtual ICollection<JobRating> Ratings { get; set; }
    }
}
