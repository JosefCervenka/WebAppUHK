using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Recipe
{
    public class Step : BaseEntity
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public string Text { get; set; }
    }
}
