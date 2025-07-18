﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;
using WebApp.Core.Models.Common;
using WebApp.Core.Models.Sys;

namespace WebApp.Core.Models.Recipe
{
    public class Recipe : BaseEntity
    {
        public int AuthorId { get; set; }

        public User Author { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int HeaderPhotoId { get; set; }

        public Photo HeaderPhoto { get; set; }

        public List<Comment> Comments { get; set; }

        public List<Step> Steps { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        [NotMapped]
        public double? Average
        {
            get
            {
                if (Comments is [] or null)
                {
                    return 0;
                }
                
                return ((Comments?.Sum(x => x.Rating) ?? 0) / (double)(Comments!.Count));
            }
        }
    }
}