using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library_10;

namespace Лаба12_часть3_Дерево
{
    public class Point_Tree<T> where T : IComparable, IInit, new()
    {
        public T? Data { get; set; }
        public Point_Tree<T>? Left { get; set; }
        public Point_Tree<T>? Right { get; set; }

        //public static T MakeRandomItem() //создание информационного поля
        //{
        //    T data = new T();
        //    data.RandomInit();
        //    return data;
        //}

        public Point_Tree()
        {
            this.Data = default(T); //если мы подставим сюда ссылку, то будет null, иначе (если значимый тип) будет присвоено 0
            this.Left = null;
            this.Right = null;
        }

        public Point_Tree(T data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        public override string ToString() //преобразование элемента типа Point в строку 
        {
            return Data == null ? "" : Data.ToString(); //проверка на null (если Data пустая, будет возвращена пустая строка)
        }

        public int CompareTo(Point_Tree<T> other)
        {
            return Data.CompareTo(other.Data);
        }
    }
}
