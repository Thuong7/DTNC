using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuAn_DTNC.Models
{
    public class ComboFoodItem
    {
        [Key, Column(Order = 1)]
        public int ComboId { get; set; }

        [Key, Column(Order = 2)]
        public int FoodItemId { get; set; }

        public Combo Combo { get; set; }
        public FoodItem FoodItem { get; set; }
    }

}
