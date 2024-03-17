using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace f_1.Models
{
    internal class Customer
    {
        public Customer(Dictionary<VegetableName, float> customerList)
        {
            CustomerList=customerList;
        }

      
        public Dictionary<VegetableName,float> CustomerList { get; set; }= new();
       public  bool BuyVegetable(Market market)
        {
            double cash = 0;
            bool buyAnyVeg = false;
            foreach(var vegetable in CustomerList) {
           
           

         
           
                
                var vegetableName = vegetable.Key;
                var vegCount = vegetable.Value;

                if (market.Stands.TryGetValue(vegetableName, out var stand) && stand.Count > 0)
                {

                    Vegetable boughtVegetable = market.Stands[vegetableName].Peek();

                    if (!market.VegatableRating.ContainsKey(vegetableName))
                        market.VegatableRating[vegetableName]=new();

                    if (boughtVegetable.IsRotten)
                    {
                        market.Rating-=10;
                        market.Stands[vegetableName].Pop();
                        market.SpoiledVegetable[vegetableName]=boughtVegetable.Count;
                        market.SpoiledVegetableCount++;
                        market.VegatableRating[vegetableName]-=5;
                        return false;
                    }

                    else if (boughtVegetable.IsToxic)
                    {

                        market.Rating-=10;
                        market.ToxicVegetableCount++;
                        market.ToxicVegetable[vegetableName]=boughtVegetable.Count;
                        market.Stands[vegetableName].Pop();
                        market.VegatableRating[vegetableName]-=10;
                        return false;
                    }
                    else
                    {
                        if (boughtVegetable.Count>=vegCount)
                        {
                            market.Stands[vegetableName].Peek().Count-=vegCount;
                            buyAnyVeg=true;

                            market.Revenue+=market.Stands[vegetableName].Peek().VegetablePrice*vegCount;

                            if (boughtVegetable.Count==vegCount)
                                market.Stands[vegetableName].Pop();
                            if (!market.BoughtVegetable.ContainsKey(vegetableName))
                                market.BoughtVegetable[vegetableName]=new();

                          
                            market.BoughtVegetable[vegetableName] +=vegCount;
                            market.VegatableRating[vegetableName]+=20;
                            market.Rating+=20;
                        }
                        else
                        {
                            market.Rating-=20;
                            market.VegatableRating[vegetableName]-=20;
                        }


                    
                    }



                }
                else
                {
                    if (!buyAnyVeg)
                        market.Rating-=10;
                    return false;
                }
                    
               
            }
            
                
           
          

            return true;
        }


    }
}

