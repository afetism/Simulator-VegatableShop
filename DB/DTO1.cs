using f_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f_1.DB
{
    internal record DTO1
    {

        public DTO1()
        {
            
        }
        public DTO1(int customerCount, int unHappyCustomer, int happyCustomer, int spoiledVegetableCount, int toxicVegetableCount,string time,int rating, Dictionary<VegetableName, float> newVegetable, Dictionary<VegetableName, int> vegRating,double revenue)
        {
            CustomerCount=customerCount;
            UnHappyCustomer=unHappyCustomer;
            HappyCustomer=happyCustomer;
            SpoiledVegetableCount=spoiledVegetableCount;
            ToxicVegetableCount=toxicVegetableCount;
            Time = time;
            Rating = rating;
            NewVegetable=newVegetable;
            VegRating=vegRating;
            Revenue=revenue;
        }

        public string Time { get; set; } 
        public int CustomerCount { get; set; }
        public int UnHappyCustomer { get; set; }
        public int HappyCustomer { get; set; }
        public int SpoiledVegetableCount { get; set; }
        public int ToxicVegetableCount { get; set; }
        public int Rating { get; set; }
        public double Revenue { get; set; }
        public Dictionary<VegetableName, float> NewVegetable { get; set; } = new();
        public Dictionary<VegetableName, int> VegRating { get; set; } = new();


    }
}
