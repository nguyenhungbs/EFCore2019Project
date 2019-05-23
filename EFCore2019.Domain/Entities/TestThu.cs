using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore2019.Domain.Entities
{
    public class TestThu
    {      
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
