using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIPreExam.Models
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }

        
        public int ItemNumber { get; set; }

        
        public int Price { get; set; }

        
        public string CustomName { get; set; }

        
        public string CustomPhone { get; set; }
    }
}
