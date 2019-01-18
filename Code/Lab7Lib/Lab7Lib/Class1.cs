using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7Lib
{
    public interface IBuyer
    {
        int BuyProducts(int productCost);

        int Update(int productCost);

        string GetStringData();
    }


    public interface IShop
    {
        void AddNewBuyer(IBuyer buyer);

        void RemoveBuyer(int BuyerIndex);

        void BringProduct(int productsCount);

        void NotifyBuyers();

        int GetProductCost();

        string GetStringData();

        IEnumerable<string> GetBuyersStringData();

        void Modelate();
    }

    public class WholesaleBuyer : IBuyer
    {
        int batchSize;
        int interval;
        int curIter = 0;
        int productsCount;
        int totalSum;
        Random rnd = new Random();

        public WholesaleBuyer()
        {
            batchSize = 10;
            productsCount = 0;
            totalSum = 70000;
            interval = 5;
        }

        public WholesaleBuyer(int productsCount, int totalSum, int interval)
        {
            this.productsCount = productsCount;
            this.totalSum = totalSum;
            this.interval = interval;
        }

        public int BuyProducts(int productCost)
        {
            if (interval <= curIter)
            {
                curIter = 0;
                int sum = batchSize * productCost;

                if (totalSum - sum >= 0)
                {
                    productsCount += batchSize;
                    totalSum -= sum;
                    return batchSize;
                }
            }
            else
                curIter++;

            return 0;
        }

        public int Update(int productCost)
        {
            const double startCost = 100;

            batchSize = (int)Math.Truncate(10 * (startCost / (double)productCost));
            return BuyProducts(productCost);
        }

        public string GetStringData()
        {
            return string.Format("Число продуктов оптового покупателя: {0}; всего денег: {1} руб.",productsCount, totalSum);
        }
    }

    public class RetailBuyer : IBuyer
    {
        double probability;
        int productsCount;
        int totalSum;
        Random rnd = new Random();

        public RetailBuyer()
        {
            productsCount = 0;
            totalSum = 35000;
            probability = 0.5;
        }

        public RetailBuyer(int productsCount, int totalSum)
        {
            this.productsCount = productsCount;
            this.totalSum = totalSum;
        }

        public int BuyProducts(int productCost)
        {
            if (totalSum - productCost >= 0 && rnd.NextDouble() <= probability)
            {
                productsCount++;
                totalSum -= productCost;
                return 1;
            }
            return 0;
        }

        public int Update(int productCost)
        {
            const double startCost = 100;
            probability = 0.5 * ((double)startCost / (double)productCost);

            return BuyProducts(productCost);
        }

        public string GetStringData()
        {
            return string.Format("Число продуктов розничного покупателя: {0}; всего денег: {1} руб.", productsCount, totalSum);
        }
    }

    public class Shop : IShop
    {
        List<IBuyer> buyers;
        int productsCount;
        int productCost;
        Random rnd = new Random();

        public Shop()
        {
            buyers = new List<IBuyer>()
            {
                new WholesaleBuyer(),
                new WholesaleBuyer(0,100000, 5),
                new RetailBuyer(),
                new RetailBuyer(0,20000)
            };
            productCost = 100;
            productsCount = 150;
        }

        public Shop(int productCost, int productsCount)
        {
            buyers = new List<IBuyer>();
            this.productCost = productCost;
            this.productsCount = productsCount;
        }

        public void AddNewBuyer(IBuyer buyer)
        {
            buyers.Add(buyer);
        }

        public void RemoveBuyer(int buyerIndex)
        {
            if (buyerIndex >= 0 && buyerIndex < buyers.Count)
                buyers.RemoveAt(buyerIndex);
        }

        public void NotifyBuyers()
        {
            foreach (IBuyer buyer in buyers)
            {
                productsCount -= buyer.Update(productCost);
                if (productsCount <= 0)
                    BringProduct(rnd.Next(150, 170));
            }
        }

        public void BringProduct(int count)
        {
            productsCount += count;
        }

        public int GetProductCost()
        {
            return productCost;
        }

        public string GetStringData()
        {
            return string.Format("Цена продукта за шт. в руб.: {0}",productCost);
        }

        public IEnumerable<string> GetBuyersStringData()
        {
            List<string> buyersStringData = new List<string>();

            foreach (IBuyer buyer in buyers)
                buyersStringData.Add(buyer.GetStringData());

            return buyersStringData;
        }

        public void Modelate()
        {
            const int maxProductCostChange = 2;

            int productCostChange = rnd.Next(-maxProductCostChange, maxProductCostChange);
            if (productCost + productCostChange > 0)
                productCost += productCostChange;
        }
    }
}
