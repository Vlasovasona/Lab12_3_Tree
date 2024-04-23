using System.Collections;
using System.Diagnostics.Metrics;
using Library_10;
using System.Collections.Generic;
using Лаба12_часть3_Дерево;
namespace Лаба12_часть2
{
    internal class Program
    {
        static sbyte InputSbyteNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            sbyte number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = sbyte.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert) Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        static int InputIntNumber(string msg = "Введите число")  //функция для проверки введенного числа на тип sbyte
        {
            Console.WriteLine(msg); //вывод сообщения msg
            bool isConvert; //объявление переменной, отвечающей за проверку на корректность
            int number; //переменная, которой будет присвоено корректно введенное число
            do
            {
                isConvert = int.TryParse(Console.ReadLine(), out number); //проверка на принадлежность типу sbyte
                if (!isConvert) Console.WriteLine("Неправильно введено число. Возможно вы ввели слишком длинное число. Попробуйте заново"); //в случае провала, вывод сообщения о некорректном вводе числа
            } while (!isConvert); //повторение цикла до тех пор, пока пользователь не введет корректное число
            return number; //ф-ция принимает значение введенного корректного числа
        }

        static void Clear(ref MyTree<MeasuringTool> tree) //метод очищения памяти 
        {
            tree = null;
            GC.Collect(); //метод, который позволяет сборщику мусора выполнить сборку 
        }

        static void Main(string[] args)
        {
            MyTree<MeasuringTool> tree = new MyTree<MeasuringTool>();
            MyTree<MeasuringTool> searchTree = new MyTree<MeasuringTool>();
            sbyte answer1; //объявление переменных, которые отвечают за выбранный пункт меню
            do
            {
                Console.WriteLine("1. Сформировать идеально сбалансированное бинарное дерево");
                Console.WriteLine("2. Вывести дерево/деревья");
                Console.WriteLine("3. Найти миниальный элемент в дереве");
                Console.WriteLine("4. Преобразовать ИСД в дерево поиска");
                Console.WriteLine("5. Удалить из дерева поиска элемент с заданным ключом");
                Console.WriteLine("6. Удалить деревья из памяти");
                Console.WriteLine("7. Завершение работы программы");

                answer1 = InputSbyteNumber();

                switch (answer1)
                {
                    case 1: //формирование дерева
                        {
                            try
                            {
                                sbyte size = InputSbyteNumber("Введите размер списка");
                                if (size == 0) throw new Exception("дерево не может быть нулевой длины");
                                tree = new MyTree<MeasuringTool>(size);
                                Console.WriteLine("Дерево сформировано");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 2: //вывод на экран
                        {
                            Console.WriteLine("Вывод ИСД:");
                            try
                            {
                                tree.ShowTree();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            Console.WriteLine("Вывод дерева поиска:");
                            try
                            {
                                searchTree.ShowTree();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Выполнение провалено: {e.Message}");
                            }
                            break;
                        }
                    case 3: //минимальный элемент дерева
                        {
                            if (tree.Count == 0 || tree == null) Console.WriteLine("ИС дерево пустое, операция невозможна");
                            else
                            {
                                MeasuringTool foundTool = new MeasuringTool(); //создаем объект, в который запишем найденный элемент
                                try
                                {
                                    foundTool = tree.FindMin(); //нашли минимальный элемент в ИСД
                                    foundTool.Show(); //вывели на экран
                                    Console.WriteLine("Операция прошла успешно");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                }
                            }
                            break;
                        }
                    case 4: //преобразовать ИСД в дерево поиска
                        {
                            if (tree.Count == 0 || tree == null) Console.WriteLine("Идеально сбалансированное дерево пустое, операция невозможна");
                            else
                            {
                                // Выводим исходное идеально сбалансированное дерево
                                Console.WriteLine("Исходное идеально сбалансированное дерево:");
                                try
                                {
                                    tree.ShowTree();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                }
                                // Преобразуем идеально сбалансированное дерево в дерево поиска
                                try
                                {
                                    searchTree = tree.TransformToSearchTree();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                }
                                // Выводим преобразованное дерево поиска
                                Console.WriteLine("Преобразованное дерево поиска:");
                                try
                                {
                                    Console.WriteLine("Дерево до балансировки:");
                                    searchTree.ShowTree();
                                    Console.WriteLine("Дерево после балансировки:");
                                    searchTree.BalanceSearchTree();
                                    searchTree.ShowTree();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                }
                            }
                            break;
                        }
                    case 5: //удаление из дерева поиска элемент с заданным ключом
                        {
                            int count = searchTree.Count;
                            if (searchTree.Count == 0) Console.WriteLine("ИС дерево пустое, операция невозможна");
                            else
                            {
                                int toolToDelete = InputIntNumber("Введите id элемента для удаления");
                                MeasuringTool tool = new MeasuringTool();
                                tool.id.Number = toolToDelete;
                                try
                                {
                                    searchTree.RemoveElement(tool);
                                    
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"Выполнение провалено: {e.Message}");
                                    break;
                                }
                                if (searchTree.Count == 0)
                                {
                                    Console.WriteLine("После удаления было получено пустое дерево");
                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        searchTree = searchTree.TransformToSearchTree();
                                        searchTree.BalanceSearchTree();
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                    }
                                }
                                if (count > searchTree.Count)
                                {
                                    Console.WriteLine("удаление прошло успешно. Вывожу новое дерево:");
                                    try
                                    {
                                        searchTree.ShowTree();
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine($"Выполнение провалено: {e.Message}");
                                    }
                                }
                                else 
                                {
                                    Console.WriteLine("Выполнение провалено: не найден элемент для удаления.");
                                }
                            }
                            break;
                        }
                    case 6: //удалить ИСД из памяти
                        {
                            Clear(ref tree);
                            break;
                        }
                    case 7: //выход из программы
                        {
                            Console.WriteLine("Демонстрация завершена");
                            break;
                        }
                    default: //неправильный ввод пункта меню
                        {
                            Console.WriteLine("Неправильно задан пункт меню");
                            break;
                        }
                }
            } while (answer1 != 7); //цикл повторяется пока пользователь не введет число 7
        }
    }
}

