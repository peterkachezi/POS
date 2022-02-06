using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POS.Data.Models
{
    public partial class County
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

    }
}
