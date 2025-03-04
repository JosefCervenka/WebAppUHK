using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Recipe
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public double Count { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
