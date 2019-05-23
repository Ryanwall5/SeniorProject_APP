using SA_APP.Models.FromApi;
using Xamarin.Forms;

namespace SA_APP.Models
{
    public class CheckedItem : ShoppingItemAPP
    {
        public CheckedItem(ShoppingItemAPP item)
        {
            this.ItemChecked = false;
            this.departmentId = item.departmentId;
            this.department = item.department;
            this.lowerDepartment = item.lowerDepartment;
            this.lowerDepartmentId = item.lowerDepartmentId;
            this.aisleId = item.aisleId;
            this.aisle = item.aisle;
            this.section = item.section;
            this.sectionId = item.sectionId;
            this.shelf = item.shelf;
            this.shelfId = item.shelfId;
            this.slotId = item.slotId;
            
            this.name = item.name;        
            this.inStock = item.inStock;
            this.image = item.image;
            this.itemQuantity = item.itemQuantity;
            this.linkId = item.linkId;
            this.price = item.price;
        }

        public Color FrameBackGroundColor { get; set; } = Color.White;
        public Color LabelColor { get; set; } = Color.SteelBlue;

        public bool ItemChecked { get; set; }
    }
}
