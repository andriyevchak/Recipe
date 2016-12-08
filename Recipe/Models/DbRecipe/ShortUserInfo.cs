using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recipe.Models.DbRecipe
{
    public class ShortUserInfo
    {

        [Key]
        public long key { get; set; }

        public string UserId { get; set; }

        public double Mark { get; set; }
    }
}