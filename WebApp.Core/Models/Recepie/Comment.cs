using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;
using WebApp.Core.Models.Sys;

namespace WebApp.Core.Models.Recipe
{
    public class Comment : BaseEntity
    {
        public int AuthorId { get; set; }

        public User Author { get; set; }

        public string Text { get; set; }
    }
}
