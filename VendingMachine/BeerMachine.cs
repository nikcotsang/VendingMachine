using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class BeerMachine
    {
        public int DepositedTotal { get; set; }
        private List<Beer> BeerList = new List<Beer>();

        public BeerMachine()
        {
            BeerList.Add(new Beer { Code = 'L', Description = "Coors Light", Inventory = 10, Price = 12 });
            BeerList.Add(new Beer { Code = 'M', Description = "Molson Canadaian", Inventory = 10, Price = 12 });
            BeerList.Add(new Beer { Code = 'B', Description = "Budweiser", Inventory = 10, Price = 15 });
            BeerList.Add(new Beer { Code = 'X', Description = "Exit", Inventory = 0, Price = 0 });
            DepositedTotal = 0;
        }

        public bool DepositCoin(int money)
        {
            //$1 and $2 coins, and $5 and $10 bills.
            switch (money)
            {
                case 1:
                case 2:
                case 5:
                case 10:
                    DepositedTotal += money;
                    return true;
                
                default:
                    Console.WriteLine("Invalid depoist, please try again");
                    return false; ;
            }
        }

        public bool checkEnoughToBuy(char selection)
        {
            var b = BeerList.Where(p => p.Code == selection && p.Price <= DepositedTotal).ToList().ToList();
            if (b != null && b.Count == 1)
                return true;
            else
                return false;
        }

        public void DisplayBeerSelection()
        {
            char selection = 'A';

            while (selection != 'X')
            {
                Console.WriteLine("Please choose one of the following beer:");
                foreach (var b in BeerList)
                {
                    if (b.Inventory > 0)
                    {
                        Console.WriteLine(b.Code + " - " + b.Description);
                    }
                }
                selection = Convert.ToChar(Console.ReadLine().ToUpper());
                if (selection != 'X')
                    MakeBeerSelection(selection);
            }
        }

        private void MakeBeerSelection(char selection)
        {
            bool ok = false;
            while (!ok)
            {
                switch (selection)
                {
                    case 'L':
                        Console.WriteLine("Thanks you for choosing Coors Light");
                        AcceptEnoughCoins(selection);
                        ReturnChange(selection);
                        ok = true;
                        break;
                    case 'M':
                        Console.WriteLine("Thanks you for choosing Molson Canadaian");
                        ok = true;
                        AcceptEnoughCoins(selection);
                        ReturnChange(selection);
                        break;
                    case 'B':
                        Console.WriteLine("Thanks you for choosing Budweiser");
                        AcceptEnoughCoins(selection);
                        ReturnChange(selection);
                        ok = true;
                        break;
                    case 'X':
                        Console.WriteLine("Cancelled");
                        ReturnChange(selection);
                        ok = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection");
                        selection = Convert.ToChar(Console.ReadLine().ToUpper());
                        break;
                }
            }
        }

        public void ReturnChange(char selection)
        {            
            var b = GetBeerByCode(selection);
            if (b != null && b.Price <= DepositedTotal)            
            {
                Console.WriteLine("Your change is {0:C}", DepositedTotal - b.Price);
                DepositedTotal -= b.Price;
                Console.WriteLine("Beer Remaining is " + GetBeerRemaining(selection));
                Console.WriteLine("Thank you");
                Console.WriteLine();
            }
        }

        private void AcceptEnoughCoins(char selection)
        {
            while (!checkEnoughToBuy(selection))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter $1 or $2 coins, or $5 or $10 bills)");
                DepositCoin(Convert.ToInt32(Console.ReadLine()));
            }
            ReduceInventory(selection);
        }

        public void ReduceInventory(char selection)
        {
            var b = GetBeerByCode(selection);            
            if (b != null && b.Price <= DepositedTotal)
            {
                b.Inventory -= 1;
            }
        }

        public void AddInventory(char selection, int qty)
        {
            var b = GetBeerByCode(selection);
            if (b != null)
            {
                if (b.Inventory < 10)
                {
                    b.Inventory += qty;
                }
            }
        }

        public Beer GetBeerByCode(char code)
        {
            return BeerList.Where(p => p.Code == code).First();
        }

        public int GetBeerRemaining(char code)
        {
            var b = BeerList.Where(p => p.Code == code).First();
            if (b!=null)
            {
                return b.Inventory;
            }
            return 0;
        }
    }

    public class Beer
    {
        public char Code { get; set; }
        public string Description { get; set; }
        public int Inventory { get; set; }
        public int Price { get; set; }
    }
}
