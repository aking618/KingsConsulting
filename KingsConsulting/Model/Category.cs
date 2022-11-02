using System;
namespace KingsConsulting.Model
{
    public class Category
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int price { get; set; }

        public Category(string title, string desc, int price)
        {
            this.Title = title;
            this.Description = desc;
            this.price = price;
        }
    }
}

