using System;
using System.ComponentModel;

namespace DelegateDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Sender sender = new Sender();
            Receiver reciver = new Receiver(sender);
            sender.SendMessage("去买东西");
            Console.Read();
        }
    }
    public delegate void SendMessageEventHandler(string msg);//定义委托
    class Sender
    {
        public event SendMessageEventHandler SendMessageEvent;//定义事件
        public void SendMessage(string msg)
        {
            Console.WriteLine("Sender发布命令：" + msg);
            SendMessageEvent?.Invoke(msg);//调用事件
        }
    }
    class Receiver
    {
        Sender sender;
        public Receiver(Sender sender)
        {
            this.sender = sender;
            sender.SendMessageEvent += Sender_SendMessageEvent;
        }

        private void Sender_SendMessageEvent(string msg)
        {
            Console.WriteLine("Receiver执行命令：" + msg);
        }
    }
}
