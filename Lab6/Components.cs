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
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 0, 0));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 0, 1));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 0, 2));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 1, 0));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 1, 1));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 1, 2));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 2, 0));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 2, 1));
            Components.Add(new Sensor(id++, "Сенсор", DateTime.Now, 2, 2));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 0, 0));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 0, 1));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 1, 0));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 1, 1));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 2, 0));
            Components.Add(new Actuator(id++, "Исполнительный механизм", DateTime.Now, 2, 1));
        }
    }

    // Компонент умного дома
    abstract class Component
    {
        public int ID { get; set; }
        protected string ComponentType { get; set; }
        protected DateTime Time { get; set; }

        public Component(int Id, string Type, DateTime Time)
        {
            this.ID = Id;
            this.ComponentType = Type;
            this.Time = Time;
        }

        public virtual void GetInfo()
        {
            Console.WriteLine(ID + ' ' + ComponentType + ' ' + Time);
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

        public Sensor(int Id, string Type, DateTime Time, byte Sensor, byte Value) : base(Id, Type, Time)
        {
            SensorType = (SensorTypes)Sensor;
            SignalValue = (SensorValues)Value;
        }
        public override void GetInfo()
        {
            Console.WriteLine("{0}: {1} {2} {3} = {4}", Time, SensorType, ComponentType, ID, SignalValue);
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

        public Actuator(int Id, string Type, DateTime Time, byte Actuator, byte Value) : base(Id, Type, Time)
        {
            ActuatorType = (ActuatorTypes)Actuator;
            ActuatorValue = (ActuatorValues)Value;
        }
        public override void GetInfo()
        {
            Console.WriteLine("{0}: {1} {2} {3} = {4}", Time, ActuatorType, ComponentType, ID, ActuatorValue);
        }
    }
}
