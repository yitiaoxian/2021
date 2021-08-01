using System;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Execute();
            //Student student = new Student { Name = "zhangsan", Age = 20 };
            //Console.WriteLine("name=" + student.Name);
            //ChangeName(student);
            //Console.WriteLine("name="+student.Name);

            //List<int> list = new List<int> { 1,2,3,4,2};
            //int[] plist = new int[] { 1, 2, 3, 4, 2 };
            //int a = 32;
            //ChangeArray(in list);
            //ChangeArray( plist);
            //ChangeValueRef(a);

            //plist = plist[0..3];
            //ChangeValueRef(ref a);
            //StartProgram program = new StartProgram();
            //program.Start();
            //Console.ReadLine();
            CoreBusiness.Describe("XIAO Qianke","A101");
            AnimalTypeTestClass testClass = new AnimalTypeTestClass();
            Type type = testClass.GetType();
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(AnimalTypeAttribute))
                        Console.WriteLine("Method {0} has a pet {1} attribute.",mInfo.Name, ((AnimalTypeAttribute)attr).Pet);
                }
            }
            Console.ReadLine();
        }
        /// <summary>
        /// 默认为in
        /// </summary>
        /// <param name="a"></param>
        static void ChangeValueRef(int a)
        {
            a = -1;
        }
        static void ChangeValueRef(ref int a)
        {
            a = -1;
        }
        static void ChangeName(Student stu)
        {
            stu.Name = "lisi";
        }
        static void ChangeArray( int[] array )
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1;
            }
        }
        static void ChangeArray(in List<int> array)
        {
            for(int i = 0;i< array.Count;i++)
            {
                array[i] = 1;
            }
        }
        void InitialPoint(ref double pointX2, ref double point2)
        {
            pointX2 = 300.1;
            //pointX2.
        }
        public static void Execute()
        {
            var pointStruct1 = new PointStruct();
            var pointClass1 = new PointClass();
            var pointStruct2 = pointStruct1;
            var pointClass2 = pointClass1;
        }
    }
    public enum Animal
    {
        Dog = 1,//枚举动物类型，从1开始，0表示未初始化
        Cat,
        Bird
    }
    public class AnimalTypeAttribute : Attribute
    {
        public AnimalTypeAttribute(Animal pet)
        {
            thePet = pet;
        }
        protected Animal thePet;
        public Animal Pet
        {
            get { return thePet; }
            set { thePet = value; }
        }
    }
    class AnimalTypeTestClass
    {
        [AnimalType(Animal.Dog)]
        public void DogMethod()
        {

        }
        [AnimalType(Animal.Cat)]
        public void CatMethod()
        {

        }
        [AnimalType(Animal.Bird)]
        public void BirdMethod()
        {

        }
    }
    class Student
    {
        public string Name { set; get; }
        public int Age { set; get; } 
    }
    class StartProgram
    {
        public void Start()
        {
            List<double> ne = new List<double>();
            ne.Add(2.2);
            Console.WriteLine(ne.GetType().IsValueType);
            double pointX1 = 100.1;
            var point1 = new Point();
            point1.PointX = 200.1;
            Console.WriteLine("point1:{0}", point1.GetHashCode().ToString());

            InitialPoint(pointX1, point1);
           // InitialPoint2(ref pointX1,ref point1);
            Console.WriteLine(string.Format("pointX1:{0}", pointX1));
            Console.WriteLine(string.Format("point1.PointX:{0}", point1.PointX));
            Console.ReadKey();
        }
        void InitialPoint(double pointX2, Point point2)
        {
            Console.WriteLine("point2:{0}",point2.GetHashCode().ToString());
            pointX2 = 300.1;
            point2.PointX = pointX2;
        }
        void InitialPoint2(ref double pointX2, ref Point point2)
        {
            Console.WriteLine("point2:{0},point2.PointX:{1}", point2.GetHashCode().ToString(),point2.PointX);
            pointX2 = 300.1;
            point2.PointX = pointX2;
            Console.WriteLine("point2:{0},point2.PointX:{1}", point2.GetHashCode().ToString(), point2.PointX);
        }
    }
    public struct PointStruct
    {
        public double PointX { get; set; }
        public double PointY { get; set; }
    }
    public class PointClass
    {
        public double PointX { get; set; }
        public double PointY { get; set; }
    }
    public class Point
    {
        public double PointX { get; set; }
        public double PointY { get; set; }
    }
}
