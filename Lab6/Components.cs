using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    // Интерфейс для прокси и объекта
    interface IComponent
    {
        Component GetComponent(int ID);
    }

    // Прокси списка компонентов умного дома
    class ComponentStoreProxy: IComponent
    {
        List<Component> UsedComponents;
        ComponentStore Store;

        public ComponentStoreProxy()
        {
            UsedComponents = new List<Component>();
        }

        public Component GetComponent(int ID)
        {
            Component comp = UsedComponents.FirstOrDefault(x => x.ID == ID);
            if (comp == null)
            {
                if (Store == null)
                    Store = new ComponentStore();
                comp = Store.GetComponent(ID);
                UsedComponents.Add(comp);
            }
            return comp;
        }
    }

    // Список компонентов умного дома
    class ComponentStore: IComponent
    {
        ComponentContext DB;

        public ComponentStore()
        {
            DB = new ComponentContext();
        }
        public Component GetComponent(int ID)
        {
            return DB.Components.FirstOrDefault(p => p.ID == ID);
        }
    }

    // Реализация "БД", хранящей информацию о компонентах умного дома
    class ComponentContext
    {
        public List<Component> Components { get; set; }
        public ComponentContext()
        {
            Components = new List<Component>();
            int id = 0;
            Components.Add(new Sensor(id++, "Sensor", 0, 0));
            Components.Add(new Sensor(id++, "Sensor", 0, 1));
            Components.Add(new Sensor(id++, "Sensor", 0, 2));
            Components.Add(new Sensor(id++, "Sensor", 1, 0));
            Components.Add(new Sensor(id++, "Sensor", 1, 1));
            Components.Add(new Sensor(id++, "Sensor", 1, 2));
            Components.Add(new Sensor(id++, "Sensor", 2, 0));
            Components.Add(new Sensor(id++, "Sensor", 2, 1));
            Components.Add(new Sensor(id++, "Sensor", 2, 2));
            Components.Add(new Actuator(id++, "Actuator", 0, 0));
            Components.Add(new Actuator(id++, "Actuator", 0, 1));
            Components.Add(new Actuator(id++, "Actuator", 1, 0));
            Components.Add(new Actuator(id++, "Actuator", 1, 1));
            Components.Add(new Actuator(id++, "Actuator", 2, 0));
            Components.Add(new Actuator(id++, "Actuator", 2, 1));
        }
    }

    // Компонент умного дома
    abstract class Component
    {
        public int ID { get; set; }
        protected string ComponentType { get; set; }

        public Component(int Id, string Type)
        {
            this.ID = Id;
            this.ComponentType = Type;
        }

        public virtual void GetInfo()
        {
            Console.WriteLine(ID + ' ' + ComponentType);
        }
    }

    // Типы датчиков
    enum SensorTypes : byte
    {
        Temperature,
        Moisture,
        Illumination
    }

    // Значение сигнала
    enum SensorValues : byte
    {
        Low,
        Normal,
        High
    }

    // Реализация компонента-сенсора
    class Sensor: Component
    {
        SensorTypes SensorType { get; set; }
        SensorValues SignalValue { get; set; }

        public Sensor(int Id, string Type, byte Sensor, byte Value) : base(Id, Type)
        {
            SensorType = (SensorTypes)Sensor;
            SignalValue = (SensorValues)Value;
        }
        public override void GetInfo()
        {
            Console.WriteLine("{0} {1} {2} = {3}", SensorType, ComponentType, ID, SignalValue);
        }
    }

     // Типы исполнительных устройств
    enum ActuatorTypes : byte
    {
        Lights, 
        Heatings,
        Cameras
    }

    // Состояния исполнительных устройств
    enum ActuatorValues : byte
    { 
        On, 
        Off 
    }

    // Реализация компонента-исполнительного устройства
    class Actuator: Component
    {
        ActuatorTypes ActuatorType { get; set; }
        ActuatorValues ActuatorValue { get; set; }

        public Actuator(int Id, string Type, byte Actuator, byte Value) : base(Id, Type)
        {
            ActuatorType = (ActuatorTypes)Actuator;
            ActuatorValue = (ActuatorValues)Value;
        }
        public override void GetInfo()
        {
            Console.WriteLine("{0} {1} {2} = {3}", ActuatorType, ComponentType, ID, ActuatorValue);
        }
    }
}
