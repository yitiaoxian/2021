using System;
using System.Collections.Generic;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Execute();

            StartProgram program = new StartProgram();
            program.Start();
            Console.ReadLine();

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
