namespace DataLayer.ViewModel
{
    public class ShopCartItem
    {
        public int ProductID { get; set; }
        public int Count { get; set; }

    }
    public class ShopCartItemViewModel
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }

    }
}