using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using f_1.Models;
namespace f_1.DB
{
    internal record DTO
    {

      public DTO ()
        {

         }

        
        public DTO(DateTime time ,int customerCount, int unHappyCustomer, int happyCustomer, int rating, double revenue, int spoiledVegetableCount, int toxicVegetableCount, Dictionary<VegetableName, float> newVegetable, Dictionary<VegetableName, float> spoiledVegetable, Dictionary<VegetableName, float> toxicVegetable, Dictionary<VegetableName, float> boughtVegetable,Dictionary<VegetableName,Stack<Vegetable>>stands,int day, Dictionary<VegetableName, int> vegatableRating)
        {
            Time = time;
            CustomerCount=customerCount;
            UnHappyCustomer=unHappyCustomer;
            HappyCustomer=happyCustomer;
            Rating=rating;
            Revenue=revenue;
            SpoiledVegetableCount=spoiledVegetableCount;
            ToxicVegetableCount=toxicVegetableCount;
            NewVegetable=newVegetable;
            SpoiledVegetable=spoiledVegetable;
            ToxicVegetable=toxicVegetable;
            BoughtVegetable=boughtVegetable;
            Stands = stands;
            CurrentDay=day;
            VegatableRating=vegatableRating;
        }

        public DateTime Time { get; set; }
        public int CustomerCount { get; set; }
        public int UnHappyCustomer { get; set; }
        public int HappyCustomer { get; set; }
        public int Rating { get; set; }
        public double Revenue { get; set; }
        public int SpoiledVegetableCount { get; set; }
        public int ToxicVegetableCount { get; set; }
        public int CurrentDay { get; set; }

        public Dictionary<VegetableName, Stack<Vegetable>> Stands { get; set; } = new();
        public Dictionary<VegetableName, float> NewVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> SpoiledVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> ToxicVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> BoughtVegetable { get; set; } = new();

        public Dictionary<VegetableName, int> VegatableRating { get; set; }
    }
}