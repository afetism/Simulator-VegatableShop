using f_1.DB;
using f_1.Helper;
using System.Linq;
using System.Threading.Channels;
using System.Timers;

namespace f_1.Models
{
    internal class Market   
    {
        public Queue<Customer> Customers { get; set; } = new();
        public Dictionary<VegetableName, Stack<Vegetable>> Stands { get; set; } = new();
        public Dictionary<VegetableName, float> SpoiledVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> ToxicVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> BoughtVegetable { get; set; } = new();
        public Dictionary<VegetableName, float> NewVegetable { get; set; } = new();
        public Dictionary<VegetableName, int > VegatableRating { get; set; } = new();

        public DateTime currentDate { get; set; }
        public int CustomerCount { get; set; }
        public int UnHappyCustomer { get; set; }
        public int HappyCustomer { get; set; }
        public int Rating { get; set; } 
        public double Revenue { get; set; } 
        public int SpoiledVegetableCount { get; set; } 
        public int ToxicVegetableCount { get; set; } 

       


        readonly Random random = new Random();
       
        public Market()
        {
            BoughtVegetable=new();
            NewVegetable=new();
            Customers =new();
            Stands=new();
            SpoiledVegetable=new();
            ToxicVegetable=new();
            VegatableRating=new();
            this.currentDate=  DateTime.Now;
            CustomerCount=0;
            UnHappyCustomer=0;
            HappyCustomer=0;
            Rating=100;
            Revenue=10000;
            SpoiledVegetableCount=0;
            ToxicVegetableCount=0;
        }

        void AddCustomer()
        {
            int MaxCustomer;
            try
            {
                MaxCustomer=CheckRating.CustomerRating();
            }
            catch (Exception ) { MaxCustomer =6; }
            int customCount = random.Next(MaxCustomer-5, MaxCustomer);
            CustomerCount=0;
            for (int i = 0; i < customCount; i++)
            {
                Dictionary<VegetableName, float> shoppingList = new();
                foreach (VegetableName vegetable in Enum.GetValues(typeof(VegetableName)))
                    if (random.Next(2) == 0)
                    {
                        float quantity = ((float)(random.Next(1, 4)/1.0));
                        shoppingList[vegetable] = quantity;
                    }

                Customers.Enqueue(new Customer(shoppingList));
                CustomerCount++;
            }
        }

        void RemoveCustomer()
        {
            int customerCount = Customers.Count;
            UnHappyCustomer=0;
            HappyCustomer=0;
            SpoiledVegetableCount=0;
            ToxicVegetableCount=0;
            for (int i = 0; i < customerCount; i++)
            {
                Customer customer = Customers.Dequeue();
                bool isOkey = customer.BuyVegetable(this);
                if (!isOkey)
                    UnHappyCustomer++;
                else HappyCustomer++;

            }
        }
       
        void FillStands()
        {
          
            foreach (VegetableName vegetable in Enum.GetValues(typeof(VegetableName)))
            {

                if (!Stands.ContainsKey(vegetable))
                    Stands[vegetable] = new Stack<Vegetable>();

                var newVeg= new Stack<Vegetable>();
                float vegetablePrice = (float)(random.Next(0, 5)+0.5);
                float vegCount = (float)(random.Next(10, 30)+0.5);
                try
                {
                   bool check= CheckRating.VegatableRating(vegetable);
                    if (check)
                    {
                        vegetablePrice+=random.Next(1, 3);
                        vegCount+=random.Next(1, 3);
                    }
                    else
                    {
                        vegetablePrice-=1;
                        vegCount-=1;
                    }
                }
                catch(Exception)
                {

                }
                if (Revenue >=vegetablePrice*vegCount)
                {
                    newVeg.Push(new Vegetable(vegetablePrice, vegCount));
                    if (Stands[vegetable].Any())
                    {
                        var concatStand = Stands[vegetable].Concat(newVeg).Reverse();
                        NewVegetable[vegetable]=newVeg.Peek().Count;
                        Stands[vegetable] = new Stack<Vegetable>(concatStand);
                    }
                    else
                        Stands[vegetable]=newVeg;
                    Revenue-=vegetablePrice*vegCount;
                }
                

            }

          
        }
        void ClearStand()
        {

            foreach(VegetableName vegetable in Enum.GetValues(typeof(VegetableName)))
            {
                if (Stands.ContainsKey(vegetable))
                {
                  
                            foreach (var item in Stands[vegetable])
                            {
                                 if (item.State==VegetableState.Rotten && Stands[vegetable].First().State==VegetableState.Rotten)
                                 {
                                     SpoiledVegetable[vegetable] =  Stands[vegetable].Peek().Count;                                
                                     Stands[vegetable].Pop();
                                     

                                 }
                            }
                }
            }
        }  

        void RandomlyChangeStandState()
        {
            int probability = random.Next(100);
            if (probability >= 0 && probability < 5)
            {
                foreach (var stack in Stands.Values)
                {
                    foreach (var vegetable in stack)
                    {
                        if (vegetable.State == VegetableState.Fresh)
                        {
                            vegetable.State = probability % 2 == 0 ? VegetableState.Rotten : VegetableState.Toxic;
                        }
                    }
                }
            }
        }

        void ChangeState()
        {
            foreach (var stack in Stands.Values)
            {
                foreach (var vegetable in stack)
                {
                    vegetable.ChangeState();
                }
            }
        }


        void AddDays()
        {
            currentDate=currentDate.AddDays(1);
        }
       

        public void DisplayStatus()
        {
            Console.WriteLine("Market Status");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Total Customers: {CustomerCount}");
            Console.WriteLine($"Happy Customers: {HappyCustomer}");
            Console.WriteLine($"Unhappy Customers: {UnHappyCustomer}");
            Console.WriteLine($"Toxic Vegetables: {ToxicVegetableCount}");
            Console.WriteLine($"Spoiled Vegetables: {SpoiledVegetableCount}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Total Revenue: {Revenue}");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Spoiled Vegetables:");
            foreach (var spoiledVeg in SpoiledVegetable)
            {
                Console.WriteLine($"- {spoiledVeg.Key}: {spoiledVeg.Value} kg");
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Toxic Vegetables:");
            foreach (var toxicVeg in ToxicVegetable)
            {
                Console.WriteLine($"- {toxicVeg.Key}: {toxicVeg.Value} kg");
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Bought Vegetables: ");
            foreach (var boughtVeg in BoughtVegetable)
            {
                Console.WriteLine($"- {boughtVeg.Key}: {boughtVeg.Value} kg");
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("New Vegetables: ");
            foreach (var newVeg in NewVegetable)
            {
                Console.WriteLine($"- {newVeg.Key}: {newVeg.Value} kg");
            }
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine($"Current Rating: {Rating}");
            Console.WriteLine("--------------------------------------------");
        }

        void Date() => Console.WriteLine($"TODAY: {currentDate.ToString("dd/MM/yyyy")}");

        void DisplayStands()
        {
           
            Console.WriteLine("Stands:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| Stand's name | Veg State | Price | Kq          |");
            Console.WriteLine("--------------------------------------------------");

            foreach (var vegetableType in Stands)
            {
                var standName = vegetableType.Key.ToString();
                var vegetables = vegetableType.Value;
                if(vegetables.Count!=0)
                Console.WriteLine($"| {standName,-12} | {vegetables.Peek().State,-9} | {vegetables.Peek().VegetablePrice,-5} | {vegetables.Peek().Count,-12} |");

                foreach (var vegetable in vegetables.Skip(1))
                {
                    Console.WriteLine($"|              | {vegetable.State,-9} | {vegetable.VegetablePrice,-5} | {vegetable.Count,-12} |");
                }
            }

            Console.WriteLine("--------------------------------------------------");
        }





        int daysElapsed { get; set; } = 1;
       

        void WeeklyReport()
        {
            MarketDB<DTO> marketDB = new("dailyReport.json");
            List<DTO> list = marketDB.GetReports;
            int batchSize = 7;
            int totalBatches = list.Count / batchSize;
            MarketDB<DTO1> weeklyReport = new("weeklyReport.json");
            for (int i = weeklyReport.GetReports.Count; i<totalBatches; i++)
            {
                List<DTO> batch = list.Skip((i) * batchSize).Take(batchSize).ToList();


                int customerCount = 0;
                int unHappyCustomer = 0;
                int happyCustomer = 0;
                int spoiledVegetableCount = 0;
                int toxicVegetableCount = 0;
                int rating = 0;
                double revenue = 0;
                DateTime startTime;
                DateTime endTime;
                Dictionary<VegetableName, float> newVegetable;
                Dictionary<VegetableName, int> vegRating;

                startTime = batch.Select(i => i.Time).FirstOrDefault();
                endTime =startTime.AddDays(6);
                string time = startTime.ToString("dd/MM/yyyy") + "-"+ endTime.ToString("dd/MM/yyyy");
                customerCount =batch.Select(i => i.CustomerCount).Sum();
                unHappyCustomer=batch.Select(i => i.UnHappyCustomer).Sum();
                happyCustomer=batch.Select(i => i.HappyCustomer).Sum();
                spoiledVegetableCount=batch.Select(i => i.SpoiledVegetableCount).Sum();
                toxicVegetableCount=batch.Select(i => i.ToxicVegetableCount).Sum();
                rating = batch.Select(i => i.Rating).LastOrDefault();
                revenue = batch.Select(i => i.Revenue).LastOrDefault();
                newVegetable=batch.SelectMany(i => i.NewVegetable)
                                  .GroupBy(kvp => kvp.Key)
                                  .ToDictionary(g => g.Key, g => g.Sum(kvp => kvp.Value));
                vegRating=batch.Select(i => i.VegatableRating).LastOrDefault();

                DTO1 dTO1 = new(customerCount, unHappyCustomer, happyCustomer, spoiledVegetableCount, toxicVegetableCount, time, rating, newVegetable, vegRating, revenue);
               
                weeklyReport.GetReports.Add(dTO1);
                

            }
            weeklyReport.SaveChanges();
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
           // Console.Clear();
            SpoiledVegetable = new();
            ToxicVegetable  = new();
            BoughtVegetable = new();
            NewVegetable = new();   

            AddCustomer();
            RandomlyChangeStandState();
            RemoveCustomer();
            daysElapsed++;
            
            if (daysElapsed%3==0)
            {
                ChangeState();
               
                FillStands();
            }
            if (daysElapsed%4==0)
                ClearStand();
         
               


            DTO dTO = new(currentDate, CustomerCount, UnHappyCustomer, HappyCustomer, Rating, Revenue, SpoiledVegetableCount, ToxicVegetableCount, NewVegetable, SpoiledVegetable, ToxicVegetable, BoughtVegetable, Stands, daysElapsed, VegatableRating);
            MarketDB<DTO> marketDB = new("dailyReport.json");
            marketDB.GetReports.Add(dTO);
            marketDB.SaveChanges();
            Date();
            DisplayStands();
            DisplayStatus();
            
            AddDays();

           


            Console.WriteLine("Press Any Key To Stop......");
            Console.WriteLine();
        }


 




        public void Start()
        {
            MarketDB<DTO> marketDB = new("dailyReport.json");

       
            if (marketDB.GetReports.Any())
            {
              
               
                currentDate = marketDB.GetReports.Select(i => i.Time).LastOrDefault();
                Stands = marketDB.GetReports.Select(i => i.Stands).LastOrDefault();
                Rating = marketDB.GetReports.Select(i => i.Rating).LastOrDefault();
                Revenue = marketDB.GetReports.Select(i => i.Revenue).LastOrDefault();
                daysElapsed= marketDB.GetReports.Select(i => i.CurrentDay).LastOrDefault();
                AddDays();
               
               
            }
        

            
            else
            {
               
                FillStands();
                daysElapsed=0;
            }


            DisplayStands();
           

          


            Console.WriteLine();

            Console.WriteLine();



            System.Timers.Timer timer = new System.Timers.Timer(500); // 1 day
            

            timer.Elapsed += OnTimedEvent;
           
            timer.AutoReset = true;
            timer.Enabled = true;

            
            Console.WriteLine("Market simulation started. Press any key to stop...");





            
            Console.ReadKey();
           


            timer.Stop();
            timer.Dispose();

            WeeklyReport();




        }
    }
}




