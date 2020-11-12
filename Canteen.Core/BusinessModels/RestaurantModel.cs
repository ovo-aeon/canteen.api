using Canteen.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Canteen.Core.BusinessModels
{
    public class RestaurantModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        [Required]
        public string Lat { get; set; }
        [Required]
        public string Long { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string SearchKeyWords { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(13)]
        public string Phone { get; set; }
        public string LogoName { get; set; }
        [Required]
        public IFormFile Logo { get; set; }

        internal Restaurant Create(RestaurantModel model)
        {
            if (model == null) return null;

            var rest = new Restaurant();
            rest.Address = model.Address;
            rest.Description = model.Description;
            rest.Lat = model.Lat;
            rest.Long = model.Long;
            rest.Name = model.Name;
            rest.Phone = model.Phone;
            rest.SearchKeyWords = model.SearchKeyWords;

            return rest;
        }
    }
}
