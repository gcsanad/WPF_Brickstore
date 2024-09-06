using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoBriksz
{
    internal class Lego
    {
        //ItemID, ItemName, CategoryName, ColorName, Qty
        string ItemId;
        string ItemName;
        string CategoryName;
        string ColorName;
        int Qty;

        public Lego(string beolvas)
        {
            string[] feloszt = beolvas.Split(';');
            ItemId = feloszt[0];
            ItemName = feloszt[1];
            CategoryName = feloszt[2];
            ColorName = feloszt[3];
            Qty = int.Parse(feloszt[4]);
        }

        public string ItemId1 { get => ItemId; set => ItemId = value; }
        public string ItemName1 { get => ItemName; set => ItemName = value; }
        public string CategoryName1 { get => CategoryName; set => CategoryName = value; }
        public string ColorName1 { get => ColorName; set => ColorName = value; }
        public int Qty1 { get => Qty; set => Qty = value; }
    }
}
