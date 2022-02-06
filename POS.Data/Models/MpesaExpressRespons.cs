using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace POS.Data.Models
{

    [Table("MpesaExpressResponses")]
    public partial class MpesaExpressRespons
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MerchantRequestID { get; set; }

        [StringLength(50)]
        public string CheckoutRequestID { get; set; }

        [StringLength(50)]
        public string ResponseCode { get; set; }

        [StringLength(50)]
        public string ResponseDescription { get; set; }

        [StringLength(50)]
        public string CustomerMessage { get; set; }
    }
}
