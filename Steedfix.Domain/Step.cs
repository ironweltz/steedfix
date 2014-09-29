using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class Step:EntityBase
    {
        /// <summary>
        /// Each step is related on one specific job
        /// </summary>
        [Required]
        public Guid JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Order { get; set; }
        /// <summary>
        /// Each step can have only one image, but may not have an image
        /// </summary>
        public Guid? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        /// <summary>
        /// Each step can have multiple comments.
        /// This can facilitate users commenting on the step so that the job owner can update it if the detail can be improved.
        /// </summary>
        public virtual ICollection<StepComment> Comments { get; set; }
    }
}
