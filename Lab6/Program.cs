using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            IComponent HouseProxy = new ComponentStoreProxy();

            // ID компонентов для демонстрационного извлечения из "БД"
            int ID1 = new Random(1234).Next(15);
            int ID2 = new Random(5678).Next(15);
            // информация о компонентах с ID=ID1 и ID=ID2 извлекается из "БД"
            Component Component1 = HouseProxy.GetComponent(ID1);
            Component1.GetInfo();
            Component Component2 = HouseProxy.GetComponent(ID2);
            Component2.GetInfo();
            // информация о компоненте с ID=ID1 извлекается из Proxy
            Component1 = HouseProxy.GetComponent(ID1);
            Component1.GetInfo();

            Console.ReadLine();
        }
    }
}
