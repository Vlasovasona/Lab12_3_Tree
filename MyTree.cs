using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Library_10;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Лаба12_часть3_Дерево
{
    public class MyTree<T> where T : IInit, IComparable, ICloneable, new()
    {
        Point_Tree<T>? root = null;

        int count = 0;

        public int Count => count;

        public MyTree() { }

        public MyTree(int length) //конструктор с параметром длины
        {
            count = length;
            root = MakeTree(length, root);
        }

        public void ShowTree()
        {
            Print(root); //вызов рекурсивной функции для печати дерева
        }

        //ИСД
        Point_Tree<T>? MakeTree(int length, Point_Tree<T>? point) //метод для создания идеально сбалансированного бинарного дерева
        {
            if (length == 0) return null;
            T data = new T();
            data.RandomInit();
            Point_Tree<T> newItem = new Point_Tree<T>(data); //создаем новый элемент
            int nl = length / 2; //распределяем по левому и правому деревьям 
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left); //рекурсивно заполняем дерево
            newItem.Right = MakeTree(nr, newItem.Right);
            return newItem;
        }

        public void Print(Point_Tree<T>? p, int padding = 5)
        {
            if (count == 0 || root == null) throw new Exception("empty tree");
            if (p != null)
            {
                Print(p.Left, padding + 5);
                for (int i = 0; i < padding; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(p.Data) ;
                Print(p.Right, padding + 5);
            }
        }

        //методы для нахождения элемента с минимальным значением id в идеально сбалансированном дереве 
        public T FindMin()
        {
            if (root == null || count == 0)
            {
                throw new Exception("Дерево пусто. Не существует минимального значения.");
            }
            T min = root.Data; // Устанавливаем начальное минимальное значение как значение корня
            FindMinRecursive(root, ref min); // Вызов рекурсивного метода для сравнения элементов дерева
            return min; // Возвращаем найденный минимальный элемент
        }

        private void FindMinRecursive(Point_Tree<T>? node, ref T min) //рекурсивный метод для нахождения минимального элемента ИСД
        {
            if (node == null)
            {
                return;
            }
            // Сравниваем текущий узел с текущим минимальным значением и обновляем при необходимости
            if (node.Data.CompareTo(min) < 0)
            {
                min = node.Data;
            }
            // Рекурсивно вызываем метод для левого и правого поддеревьев
            FindMinRecursive(node.Left, ref min);
            FindMinRecursive(node.Right, ref min);
        }
        //методы для нахождения элемента с минимальным значением id в ИСД конец

        //методы для дерева поиска
        public void AddPoint(T oldData) //добавление элемента в дерево поиска
        {
            T data = (T)oldData.Clone();
            Point_Tree<T>? node = root;
            Point_Tree<T>? current = null;
            bool isExist = false; //проверка существует ли элемент в дереве или нет
            while (node != null && !isExist) //пока не дошли до конца ветки и пока такой же элемент не найден
            {
                current = node; //объявили текущий элемент
                if (node.Data.CompareTo(data) == 0) //если элемент для добавления уже есть
                    isExist = true; //меняем флажок на true
                else //иначе начинаем искать место
                {
                    if (node.Data.CompareTo(data) > 0) //если Data меньше чем то, что находится в Point
                        node = node.Left; //смещаемся на девую ветку
                    else node = node.Right; //иначе смещаемся на правую ветку
                }
            }
            //нашли место для элемента
            if (isExist)
            {
                return;
            }
            Point_Tree<T> newPoint = new Point_Tree<T>(data);
            if (current.Data.CompareTo(data) > 0)
                current.Left = newPoint;
            else
                current.Right = newPoint;
            count++;
        }

        //методы балансировки дерева поиска
        public void BalanceSearchTree()
        {
            // Создаем список элементов для сохранения отсортированных данных дерева
            List<T> elements = new List<T>();

            // Выполняем обход дерева в порядке возрастания и добавляем данные в список
            InOrderTraversal(root, elements);

            // Перестраиваем дерево, чтобы достигнуть баланса, на основе отсортированного списка элементов
            root = BuildBalancedTree(elements, 0, elements.Count - 1);
        }

        // Рекурсивный обход дерева в порядке возрастания и добавление элементов в список
        private void InOrderTraversal(Point_Tree<T>? node, List<T> elements)
        {
            // Если узел пустой, выходим из метода
            if (node == null)
            {
                return;
            }

            // Рекурсивно обходим левое поддерево
            InOrderTraversal(node.Left, elements);
            // Добавляем данные текущего узла в список
            elements.Add(node.Data);
            // Рекурсивно обходим правое поддерево
            InOrderTraversal(node.Right, elements);
        }

        // Рекурсивное построение сбалансированного дерева из отсортированного списка элементов
        private Point_Tree<T>? BuildBalancedTree(List<T> elements, int start, int end)
        {
            // Если начальный индекс больше конечного, возвращаем пустой узел
            if (start > end)
            {
                return null;
            }

            // Находим середину массива для построения сбалансированного дерева
            int mid = (start + end) / 2;

            // Создаем новый узел из центрального элемента массива
            Point_Tree<T> newNode = new Point_Tree<T>(elements[mid]);

            // Рекурсивно строим левое поддерево из элементов перед серединой
            newNode.Left = BuildBalancedTree(elements, start, mid - 1);

            // Рекурсивно строим правое поддерево из элементов после середины
            newNode.Right = BuildBalancedTree(elements, mid + 1, end);

            return newNode; // Возвращаем новый узел, который стал корнем сбалансированного дерева
        }
        //методы для балансировки закончились

        void TransformToArray(Point_Tree<T> node, T[] array, ref int current) //метод для создания массива со значениями из ИСД
        {
            if (node != null)
            {
                TransformToArray(node.Left, array, ref current);
                array[current] = node.Data;
                current++;
                TransformToArray(node.Right, array, ref current);
            }
        }

        public MyTree<T> TransformToSearchTree() //трансфоормация ИСД в дерево поиска
        {
            if (count == 0 || root == null) throw new Exception("empty tree");
            else
            {
                T[] array = new T[count];
                int current = 0; // Инициализация переменной для отслеживания текущей позиции в массиве
                TransformToArray(root, array, ref current);

                MyTree<T> newSearchTree = new MyTree<T>();
                newSearchTree.root = new Point_Tree<T>(array[0]);
                newSearchTree.count = 1; // Устанавливаем счетчик элементов в 1

                // Добавляем остальные элементы массива в дерево поиска
                for (int i = 1; i < array.Length; i++) // Начинаем с индекса 1, так как первый элемент уже добавлен
                {
                    newSearchTree.AddPoint(array[i]);
                }
                return newSearchTree;
            }            
        }

        public void RemoveElement(T key) //вызов рекурсивной функции для удаления элемента из дерева поиска
        {
            // Вызов рекурсивного метода удаления элемента
            root = RemoveElementRecursive(root, key);
        }

        private Point_Tree<T>? RemoveElementRecursive(Point_Tree<T>? node, T key) //рекурсивная функция для удаления элемента из дерева поиска
        {
            if (node == null)
            {
                return node;
            }

            // Сравниваем ключ узла с заданным ключом
            if (node.Data.CompareTo(key) > 0)
            {
                // Рекурсивно идем влево, если ключ меньше
                node.Left = RemoveElementRecursive(node.Left, key);
            }
            else if (node.Data.CompareTo(key) < 0)
            {
                // Рекурсивно идем вправо, если ключ больше
                node.Right = RemoveElementRecursive(node.Right, key);
            }
            else
            {
                // Узел с заданным ключом найден, рассматриваем 3 случая
                if (node.Left == null && root.Right == null) //У ЭЛЕМЕНТА НЕТ ДОЧЕРНИХ УЗЛОВ
                {
                    node = null; //просто удаляем узел
                    count--; //обновляем счетчик
                }
                else if (node.Right != null && node.Left != null) //у узла есть оба дочерних элемента
                {
                    Point_Tree<T> maxNode = FindMaxValue(node.Left); //находим максимальный элемент в левом поддереве
                    node.Data = maxNode.Data; //ставим его вместо удаляемого
                    node.Left = RemoveElementRecursive(node.Left, maxNode.Data); //удаляем найденный максимальный элемент в правом поддереве
                }
                else 
                {
                    Point_Tree<T> child = new Point_Tree<T>(); //если есть один из дочерних узлов
                    if (node.Left != null) child = node.Left; 
                    else child = node.Right;                  //заменяем его единственным дочерним узлом
                    node = child;
                    count--;
                }
            }
            return node;
        }

        public Point_Tree<T>? FindMaxValue(Point_Tree<T>? node) //метод для нахождения максимального элемента в поддереве (нужен для удаления элемента из дерева поиска)
        {
            if (node == null)
            {
                return null;
            }
            Point_Tree<T>? maxNode = node;
            Point_Tree<T>? leftMaxValue = FindMaxValue(node.Left);
            Point_Tree<T>? rightMaxValue = FindMaxValue(node.Right);
            if (leftMaxValue!=null && leftMaxValue.Data.CompareTo(maxNode.Data) > 0)
                maxNode = leftMaxValue;
            if (rightMaxValue != null && rightMaxValue.Data.CompareTo(maxNode.Data) > 0)
                maxNode = rightMaxValue;
            return maxNode;
        }

        public T GetRoot()
        {
            return root.Data;
        }
    }
}
