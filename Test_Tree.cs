using Лаба12_часть3_Дерево;
using Library_10;

namespace Tree_Tests
{
    [TestClass]
    public class Test_Tree
    {
        //блок ошибок
        [TestMethod]
        public void Print_EmptyTree_Exception() //метод проверки возникновения ошибки при попытке печати пустого дерева
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                tree.ShowTree();
            });
        }

        [TestMethod]
        public void Print_FindMinElement_InEmptyTree_Exception() //метод проверки возникновения ошибки при попытке найти минимальный элемент в пустой дереве
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();
            Assert.ThrowsException<Exception>(() =>
            {
                tree.FindMin();
            });
        }
        //блок ошибок закончен

        //проверка конструктора дерева
        [TestMethod]
        public void CreateTree_CheckCount_Test() //проверка работы Count
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(5);
            Assert.AreEqual(5, tree.Count);
        }

        //проверка класса Point_Tree
        [TestMethod]
        public void CreatePointTree_CheckNull_Test() //проверка конструктора по умолчанию для создания Point 
        {
            Point_Tree<Instrument> p = new Point_Tree<Instrument>();
            Assert.IsNull(p.Left);
        }

        [TestMethod]
        public void Point_ToString_NotNull_Test() //проверка ToString для НЕ постуой строки
        {
            HandTool tool = new HandTool();
            Point_Tree<Library_10.Instrument> p = new Point_Tree<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), tool.ToString());
        }

        [TestMethod]
        public void Point_ToString_Null_Test() //проверка метода ToString для пустой строки
        {
            HandTool tool = new HandTool();
            tool = null;
            Point_Tree<Library_10.Instrument> p = new Point_Tree<Library_10.Instrument>(tool);
            Assert.AreEqual(p.ToString(), "");
        }

        [TestMethod]
        public void TestCompareTo() //проверка нового компаратора  (по id)
        {
            Instrument tool1 = new Instrument("E", 2);
            Instrument tool2 = new Instrument("Er", 22);
            Instrument toolEqual = new Instrument("E", 2);

            Point_Tree<Instrument> node1 = new Point_Tree<Instrument>(tool1);
            Point_Tree<Instrument> node2 = new Point_Tree<Instrument>(tool2);

            int result1 = node1.CompareTo(node2); // Сравнение node1 с node2
            int result2 = node2.CompareTo(node1); // Сравнение node2 с node1
            int result3 = node1.CompareTo(node1); // Сравнение node1 с самим собой

            Assert.AreEqual(-1, result1); // node1 меньше node2, ожидаем -1
            Assert.AreEqual(1, result2); // node2 больше node1, ожидаем 1
            Assert.AreEqual(0, result3); // node1 равен node1, ожидаем 0
        }

        //проеврка методов MyTree
        [TestMethod]
        public void TestFindMinRecursive_WhenRootIsNull_ShouldThrowException()
        {
            MyTree<Instrument> tree = new MyTree<Instrument>();

            Assert.ThrowsException<Exception>(() => tree.FindMin(), "Дерево пусто. Не существует минимального значения.");
        }

        [TestMethod]
        public void TestFindMin_OneNodeTree() //проверка нахождения минимального элемента в дереве с одним узлом
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument();
            tool1 = tree.GetRoot();
            Instrument min = tree.FindMin();
            Assert.AreEqual(tool1, min);
        }

        [TestMethod]
        public void TestFindMin_FullTree() //нахождение минимального элемента в заполненном дереве
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument("E", 10);
            Instrument tool2 = new Instrument("Er", 5);
            Instrument tool3 = new Instrument("E", 20);
            Instrument tool4 = new Instrument("Er", 3);
            Instrument tool5 = new Instrument("E", 15);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);

            Instrument min = tree.FindMin();

            Assert.AreEqual(tool4, min);
        }

        [TestMethod]
        public void Test_TransformOneNodeTree() //проверка трансформера дерева с одним узлом
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument();
            Instrument tool2 = new Instrument();

            tool1 = tree.GetRoot();
            tree.TransformToSearchTree();
            tool2 = tree.GetRoot();
            Assert.AreEqual(tool1, tool2);
        }

        [TestMethod]
        public void Test_TransformFullNodeTree() //мето для проверки трансформера дерева поиска
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            Instrument tool1 = new Instrument("E", 182);
            Instrument tool2 = new Instrument("Er", 145);
            Instrument tool3 = new Instrument("E", 198);
            Instrument tool4 = new Instrument("Er", 185);
            Instrument tool5 = new Instrument("E", 170);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);

            Instrument toolRoot = new Instrument();
            tree.TransformToSearchTree();
            tree.BalanceSearchTree();
            toolRoot = tree.GetRoot();
            Assert.AreEqual(tool1, toolRoot);
        }

        //тестирование методов удаления
        [TestMethod]
        public void Test_DeleteFullNodeTree() //метод для проверки удаления
        {
            MyTree<Instrument> tree = new MyTree<Instrument>(1);
            MyTree<Instrument> treeNew = new MyTree<Instrument>(1);

            Instrument tool1 = new Instrument("E", 177);
            Instrument tool2 = new Instrument("Er", 152);
            Instrument tool3 = new Instrument("E", 124);
            Instrument tool4 = new Instrument("Er", 108);
            Instrument tool5 = new Instrument("E", 103);
            tree.AddPoint(tool1);
            tree.AddPoint(tool2);
            tree.AddPoint(tool3);
            tree.AddPoint(tool4);
            tree.AddPoint(tool5);

            Instrument toolRoot = new Instrument();
            treeNew = tree.TransformToSearchTree();
            treeNew.BalanceSearchTree();
            treeNew.RemoveElement(tool3);
            treeNew.BalanceSearchTree();
            toolRoot = treeNew.GetRoot();
            Assert.AreEqual(tool4, toolRoot);
        }
    }
}
