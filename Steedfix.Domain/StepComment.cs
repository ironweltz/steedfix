using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class StepComment:Comment
    {
        public Guid StepId { get; set; }
        [ForeignKey("StepId")]
        public Step Step { get; set; }
    }
}
