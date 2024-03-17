using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using f_1.DB;
namespace f_1.Models
{

    public enum VegetableName
    {
        Tomato,
        Potato,
        Carrot,
        Onion,
        Cucumber
    }

    enum VegetableState
    { 
       Fresh,
       Normal,
       Rotten,
       Toxic
    
    }


    internal class Vegetable
    {
       public VegetableState State { get; set; }
       public float Count { get; set; }
       public float VegetablePrice { get; init; }
      

       Random random=new Random();
        public Vegetable()
        {

        }
        public Vegetable(float vegetablePrice,float vegCount)
        {
            
            State =random.Next(4)<=2? VegetableState.Fresh: VegetableState.Toxic; //75-25 ehtimal
            Count=vegCount;
            VegetablePrice =vegetablePrice;

        }

        public bool IsRotten => State==VegetableState.Rotten;
        public bool IsToxic => State==VegetableState.Toxic;

        public void ChangeState()
        {
        
           switch (State)
           {
                case VegetableState.Fresh:
                    State=VegetableState.Normal;
                    break;
                case VegetableState.Normal:
                    State=VegetableState.Rotten;
                    break;
                case VegetableState.Rotten:
                    State=VegetableState.Toxic;
                    break;
                default:
                    break;

           }
        }


  
        public override string ToString()
        {
            return $"Veg State: {State} Price: {VegetablePrice} Kq:{Count} ";
        }
        
          
    }
}