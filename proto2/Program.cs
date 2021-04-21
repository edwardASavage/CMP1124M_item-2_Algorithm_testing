using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgAssFinal
{
    #region BST util classes and methods
    class BinaryNode
    {
        public double dataItem;
        public BinaryNode left;
        public BinaryNode right;

        public BinaryNode(double value)
        {
            dataItem = value;
            left = right = null;
        }

    }
    class BST
    {
        public BinaryNode root;

        public BST()
        {
            root = null;
        }

        public static int Depth(BinaryNode root)
        {
            // returns max depth
            if (root == null)
            {
                return 0;
            }
            else
            {
                int Ldepth = Depth(root.left);
                int Rdepth = Depth(root.right);
                if (Ldepth > Rdepth)
                {
                    return (Ldepth + 1);
                }
                else
                {
                    return (Rdepth + 1);
                }
            }
        }

        public int GetLevel(BinaryNode root, double dataItem, int level)
        {
            if (root == null)
            {
                return 0;
            }
            if (root.dataItem == dataItem)
            {
                return level;
            }
            return GetLevel(root.left, dataItem, level + 1) | GetLevel(root.right, dataItem, level + 1);
        }
        public void PrintLevel(BinaryNode root, int level)
        {
            
            if (root == null)
                return;
            if (level == 1)
                Console.Write(root.dataItem + " ");
            else if (level > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                PrintLevel(root.left, level - 1);
                Console.ForegroundColor = ConsoleColor.Green;

                PrintLevel(root.right, level - 1);
                Console.ResetColor();
            }
        }

        private BinaryNode InsertNode(BinaryNode root, double value)
        {
            if (root == null)
            {
                root = new BinaryNode(value);
            }
            else
            {
                if (root.dataItem > value)
                {
                    root.left = InsertNode(root.left, value);
                }
                else
                {
                    root.right = InsertNode(root.right, value);
                }
            }
            return root;
        }
        // mirror insert desc
        private BinaryNode InsertNode(BinaryNode root, double value, bool desc)
        {
            if (root == null)
            {
                root = new BinaryNode(value);
            }
            else
            {
                if (root.dataItem < value)
                {
                    root.left = InsertNode(root.left, value, desc);

                }
                else
                {
                    root.right = InsertNode(root.right, value, desc);
                }
            }

            return root;
        }

        public void Insert(double value)
        {
            root = InsertNode(root, value);
        }

        // overide  desc order
        public void Insert(double value, bool desc)
        {
            root = InsertNode(root, value, desc);
        }




        // BST closest searching method.
        public double FindClosestVal(BinaryNode root, double data)
        {
            if (root.left != null && data < root.dataItem)
            {
                double leftRes = FindClosestVal(root.left, data);
                return Math.Abs(leftRes - data) < Math.Abs(root.dataItem - data) ? leftRes : root.dataItem;
            }
            if (root.right != null && data > root.dataItem)
            {
                double rightRes = FindClosestVal(root.right, data);
                return Math.Abs(rightRes - data) < Math.Abs(root.dataItem - data) ? rightRes : root.dataItem;
            }
            return root.dataItem;
        }
        public double FindClosestVal(BinaryNode root, double data, bool asc)
        {
            if (root.left != null && data < root.dataItem)
            {
                double rightRes = FindClosestVal(root.right, data, asc);
                return Math.Abs(rightRes - data) < Math.Abs(root.dataItem - data) ? rightRes : root.dataItem;
               
            }
            if (root.right != null && data > root.dataItem)
            {
                double leftRes = FindClosestVal(root.left, data, asc);
                return Math.Abs(leftRes - data) < Math.Abs(root.dataItem - data) ? leftRes : root.dataItem;
            }
            return root.dataItem;
        }

      
        public void InOrder(BinaryNode root, int counter, int mod)
        {
           
            if (root != null)
            {
                counter++;
                InOrder(root.left, counter, mod);
                if (counter % mod == 0)
                {
                    counter++;
                    Console.Write("\nelement {0}: {1}", counter, root.dataItem);
                }
                InOrder(root.right, counter, mod);
               
            }
        }
    }
    #endregion

    class Program
    {
        // for counting purposes.
        // Converts the txt file into a double array dataset.
        #region text to data conversion
        private static double[] TxtToData(string path)
        {
            string[] stringData = File.ReadAllLines(path);
            stringData = stringData.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            double[] numData = new double[stringData.Length];

            for (int i = 0; i < numData.Length; i++)
            {
                double.TryParse(stringData[i], out numData[i]);
            }
            return numData;
        } // writes array from txt file, uses LINQ.
        #endregion
        // Sorting algorithms for asc and desc order.
        #region bubble sort methods
       

        public static void BubbleSortAsc(double[] data)
        {
            int n = data.Length - 1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (data[j + 1] < data[j])
                    {
                        double temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }   
            }
        }

        private static void BubbleSort(double[] data)
        {
            int n = data.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    
                    if (data[j + 1] > data[j])
                    {
                        double temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
        }
        #endregion

        #region quick sort methods



        public static void QuickSortAsc(double[] data, int left, int right)
        {

            double temp, pivot;
            int i, j;
            i = left;
            j = right;

            pivot = data[(left + right) / 2];

            do
            {
                while
                    ((data[i] < pivot) && (i < right)) i++;
                while
                    ((pivot < data[j]) && (j > left)) j--;

                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);
            

            if (left < j)
                QuickSortAsc(data, left, j);

            if (i < right)
                QuickSortAsc(data, i, right);
           
        }
        //dec
        public static void QuickSort(double[] data, int left, int right)
        {

            double temp, pivot;
            int i, j;
            i = left;
            j = right;

            pivot = data[(left + right) / 2];

            do
            {
                while
                    ((data[i] > pivot) && (i < right)) i++;
                while
                    ((pivot > data[j]) && (j > left)) j--;

                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                }
            } while (i <= j);

            if (left < j)
                QuickSort(data, left, j);

            if (i < right)
                QuickSort(data, i, right);
        }
        #endregion

        #region insertion sort methods
        private static void InsertionSortAsc(double[] data)
        {
            int numSorted = 1;
            int i;
            while (numSorted < data.Length)
            {
                double temp = data[numSorted];
                for (i = numSorted; i > 0; i--)  
                    if (temp > data[i - 1]) // flip for inverse
                        data[i] = data[i - 1];

                    else break;
                data[i] = temp;
                numSorted++;
            }
        }
        public static void InsertionSort(double[] data)
        {
            int numSorted = 1;
            int i;

            while (numSorted < data.Length)
            {
                double temp = data[numSorted];
                for (i = numSorted; i > 0; i--)
                {
                   
                    if (temp > data[i - 1]) // flip for inverse ... curr desc
                        data[i] = data[i - 1];
                    else
                        break;
                }
                data[i] = temp;
                numSorted++;
            }
        }
        #endregion
        // Builds the BST class with method provided.
        #region binary tree

        private static void Bst(double[] data, bool asc)
        {
            BST bst = new BST();

            // builds bst with unsorted data.
            if (asc == true)
                for (int i = 0; i < data.Length; i++)
                    bst.Insert(data[i]);
                    
            // builds mirror bst using overload value.
            else
                for (int i = 0; i < data.Length; i++)
                    bst.Insert(data[i], asc);
                    
            if (data.Length <= 256)
                bst.InOrder(bst.root, 0, 10);
            else if (data.Length <= 2048)
                bst.InOrder(bst.root, 0, 50);
            else
                bst.InOrder(bst.root, 0, 80);
            // value to locate.
            double target;
            Console.WriteLine("\nPlease input a numeric double value to locate: ");
            while (!double.TryParse(Console.ReadLine(), out target))
            {
                Console.WriteLine("Invalid input, expecting numeric double value. please retry: ");
                continue;
            }
            double dataItem;

            // find closest.
            if (asc == true)
                dataItem = bst.FindClosestVal(bst.root, Math.Abs(target));
            else
                dataItem = bst.FindClosestVal(bst.root, Math.Abs(target), asc);

            // Level traverse for value, 1 is root.
            int dataItemLevel = bst.GetLevel(bst.root, dataItem, 1);
            Console.Clear();

            Console.WriteLine("Closest value: {0} found. BST level: {1} holds this value.", dataItem, dataItemLevel);
            Console.WriteLine("Green for right nodes, Red for left nodes, White for root.\nLevel {0}: ", dataItemLevel);
            bst.PrintLevel(bst.root, dataItemLevel);
            
            Console.ReadKey();
        }

        #endregion
        // Binary search method
        #region searching methods
        private static void SortChoice(double[] data, bool asc)
        {
            
            int input;
            Console.WriteLine("1) for Binary search \n2) for Linear search");
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid Input. \n s1) for Binary search\b2) for Linear search");
                continue;
            }
            Console.Clear();
            if (input == 1)
                if (asc == true)
                    BinarySearchClosestAsc(data);
                else
                    BinarySearchClosest(data);
            else
                if (asc == true)
                    LinearSearchClosestAsc(data);
                else
                    LinearSearchClosest(data);            
            
        }
        private static void BinarySearchClosestAsc(double[] data)
        {
            double target;
            Console.WriteLine("Please input a numeric double value to search for: ");
            // input validation.
            while (!double.TryParse(Console.ReadLine(), out target))
            {
                Console.WriteLine("Invalid input. Numeric double values only, please retry: ");
                continue;
            }
            int left = 0;
            int right = data.Length - 1;
            int mid = 0;
            // non recursive
            do
            {
                mid = left + (right - left) / 2;
                if (target > (data[mid]))
                    // go forward
                    left = mid + 1;
                else
                    // go back
                    right = mid - 1;

                if (data[mid] == target)
                    Console.WriteLine("exact value matched.");
            } while (left <= right);

            if (data[mid] != target && mid!= 0)
            {
                if (Math.Abs(target - data[mid - 1]) < Math.Abs(target - data[mid]))
                    Console.WriteLine("LOWER MATCH {0}", data[mid-1]);
                else if (Math.Abs(target - data[mid - 1]) > Math.Abs(target - data[mid]))
                    Console.WriteLine("HIGHER MATCH {0}", data[mid ]);
                else
                    Console.WriteLine("TWO MATCHES: {0} , {1}", data[mid - 1], data[mid]);
            }


        }

        private static void BinarySearchClosest(double[] data)
        {
            double target;
            Console.WriteLine("Please input a numeric double value to search for: ");
            // input validation.
            while (!double.TryParse(Console.ReadLine(), out target))
            {
                Console.WriteLine("Invalid input. Numeric double values only, please retry: ");
                continue;
            }
            int left = 0;
            int right = data.Length - 1;
            int mid = 0;
            int operations = 0;
            // non recursive
            do
            {
                mid = left + (right - left) / 2;
                Console.WriteLine("mid element is {0}", data[mid]);
                operations++;
                // move mid over by one index.
                if (target < data[mid])
                {
                    left = mid + 1;
                }
                else
                {
                    // look backward
                    
                    right = mid - 1;
                }
                    
                if (data[mid] == target)
                {
                    Console.WriteLine("exact value matched at. {0}", data[mid]);
                }
            } while (left <= right);
            Console.WriteLine(operations);
            if (data[mid] != target)
            {
                if (Math.Abs(target - data[mid - 1]) > Math.Abs(data[mid] - target))
                {
                    Console.WriteLine("HIGHER MATCH {0}", data[mid]);
                }
                else if (Math.Abs(target - data[mid - 1]) < Math.Abs(data[mid] - target))
                {
                    Console.WriteLine("LOWER MATCH {0}", data[mid - 1]);
                }
                else
                {
                    Console.WriteLine("TWO MATCHES: {0} , {1}", data[mid - 1], data[mid]);
                }
            }


        }

        private static void LinearSearchClosestAsc(double[] data)
        {
            double target;
            Console.WriteLine("Please input a numeric double value to search for: ");
            // input validation.
            while (!double.TryParse(Console.ReadLine(), out target))
            {
                Console.WriteLine("Invalid input. Numeric double values only, please retry: ");
                continue;
            }


            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == target)
                {
                    Console.WriteLine("EXACT MATCH FOR {0} at element {1}", target, i);
                    break;
                }

                if (data[i] > target && i != 0)
                { 
                    if (Math.Abs(target - data[i - 1]) > Math.Abs(target - data[i]))
                    {
                       
                        Console.WriteLine("element: {0} provides HIGHER value: {1} as closest match.", i, data[i]);
                        break;
                    }
                    else if (Math.Abs(target - data[i - 1]) < Math.Abs(target - data[i]))
                    {
                        Console.WriteLine("element: {0} provides LOWER value: {1} as closest match.", i, data[i - 1]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Two EQUAL closest matches found:\n{0} at element: {1}.\nAnd{2} at element: {3}.", data[i - 1], i-1, data[i], i);
                        break;
                    }
                }
                else if (i <= 0)
                {
                    Console.WriteLine("Value searched was lower than the initial value in this data set. element 0 is: {0}", data[0]);
                    break;
                }
            }
        }

        private static void LinearSearchClosest(double[] data)
        {
            double target;
            Console.WriteLine("Please input a numeric double value to search for: ");
            // input validation.
            while (!double.TryParse(Console.ReadLine(), out target))
            {
                Console.WriteLine("Invalid input. Numeric double values only, please retry: ");
                continue;
            }
            int operations = 0;


            for (int i = 0; i < data.Length; i++)
            {

                
                operations++;
                if (data[i] == target)
                {
                    Console.WriteLine("EXACT MATCH FOR {0} at element {1}", target, i);
                    break;
                }


                if (data[i] < target && i!= 0)
                {
                    if (Math.Abs(target - data[i - 1]) < Math.Abs(target - data[i]))
                    {

                        Console.WriteLine("element: {0} provides HIGHER value: {1} as closest match.", i, data[i]);
                        break;
                    }
                    else if (Math.Abs(target - data[i - 1]) > Math.Abs(target - data[i]))
                    {
                        Console.WriteLine("element: {0} provides LOWER value: {1} as closest match.", i, data[i - 1]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Two EQUAL closest matches found:\n{0} at element: {1}.\nAnd{2} at element: {3}.", data[i - 1], i - 1, data[i], i);
                        break;
                    }
                }
                else if(i < 0)
                {
                    Console.WriteLine("Value searched was lower than the initial value in this data set. element 0 is: {0}", data[0]);
                    break;
                }
                
            }
            Console.WriteLine(operations);
        }
        
        #endregion

        // Util methods for choosing, merging and displaying arrays of non BST type.
        #region array printing, choosing and merging methods
        static string ArrayChoice()
        {
            string size = "";
            string format = "";
            int input;
            Console.WriteLine("Please select an array size: \n1 for 256 \n2 for 2048 \n3 for 4096");
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please select an array size: \n1 for 256 \n2 for 2048 \n3 for 4096 ");
                continue;
                
            }
 
            switch (input)
            {
                case 1:
                    size = "256.txt";
                    break;
                case 2:
                    size = "2048.txt";
                    break;
                case 3:
                    size = "4096.txt";
                    break;
            } // get array size.
            Console.Clear();
            Console.WriteLine("Please select a format: \n1 for low \n2 for mean \n3 for high");

            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Please select a format: \n1 for low \n2 for mean \n3 for high");
                continue;
            }
            switch (input)
            {
                case 1:
                    format = "Low_";
                    break;
                case 2:
                    format = "Mean_";
                    break;
                default:
                    format = "High_";
                    break;
            } 
            Console.Clear();
            return format + size;
        }

        // calls arrayChoice to define two arrays for merging.
        static double[] MergeArrays(string firstChoice, string secondChoice)
        {
            double[] arr1 = TxtToData(firstChoice);
            double[] arr2 = TxtToData(secondChoice);

            double[] arr3 = new double[arr1.Length + arr2.Length];

            int i, j;
            for (i = 0; i < arr1.Length; i++)
            {
                arr3[i] = arr1[i];
            }
            for (j = 0; j < arr2.Length; j++)
            {
                arr3[i] = arr2[j];
                i++;
            }
            return arr3;
        } 
        
        static void PrintArr(double[] arr)
        {
            switch (arr.Length)
            {
                case int n when (n <= 256):
                    Console.WriteLine("\nPrinting every: 10 iterations\n");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i % 10 == 0 && i > 0)
                        {
                            Console.WriteLine("element {0}: {1}", i, arr[i]);
                        }
                    }
                    break;

                case int n when (n <= 2048 && n > 256):
                    Console.WriteLine("\nPrinting every: 50 iterations\n");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i % 50 == 0 && i > 0)
                        {
                            Console.WriteLine("element {0}: {1}", i, arr[i]);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("\nPrinting every: 80 iterations\n");
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i % 80 == 0 && i > 0)
                            Console.WriteLine("element {0}: {1}", i, arr[i]);
                    }
                    break;
            }
        }
        #endregion

        // Choice selection for sorting.
        #region sorting choice methods
        private static void Sort(double[] data)
        {
            Console.WriteLine("Please select ascending or descending sorting order: \n1 for ascending \n2 for descending");
            int input;
            bool asc = false;
        
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input: \n1 for ascending \n2 for descending");
                continue;
            }

            if (input == 1)
                asc = true;

            Console.WriteLine("Please select an algorithm to sort with:\n1 for Bubble\n2 for Insertion\n3 for Quick \n4 for BST");
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input. Select \n1 for Bubble\n2 for Insertion\n3 for Quick \n4 for BST");
                continue;
            }


            switch (input)
            {
                case 1:
                    if (asc == true) // asc
                        BubbleSortAsc(data);
                    else // desc
                        BubbleSort(data);
                    PrintArr(data);
                    SortChoice(data, asc);

                    break;

                case 2:
                    if (asc == true)
                        InsertionSortAsc(data);
                    else
                        InsertionSort(data);
                    PrintArr(data);
                    SortChoice(data, asc);
                    break;

                case 3:
                    if (asc == true)
                        QuickSortAsc(data, 0, data.Length - 1);
                    else
                        QuickSort(data, 0, data.Length - 1);
                    PrintArr(data);
                    SortChoice(data, asc);
                    //BinarySearchClosest(data);
                    break;
                default:
                    Bst(data, asc);
                    break;
            } // Perform sorting
        }
        #endregion

        private static void Driver()
        {
            string path = ArrayChoice();
            Console.WriteLine("chosen array: {0} \n", path);
            double[] data;
            int input;
            Console.WriteLine("Merge this array with another selection? \n1) for merge with another array. \n2) for chosen array only.");
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Invalid input.\n1) for merge with another array. \n2) for chosen array only.");
                continue;
            }
            if (input == 1)
            {
                string path2 = ArrayChoice();
                data = MergeArrays(path, path2);
                Console.WriteLine("arrays {0} and {1} merged.", path, path2);
            }
            else
                data = TxtToData(path);
            Sort(data);          
            Console.WriteLine("\nanalysed array total length: {0}", data.Length);
        }

        private static void Main(string[] args)
        {
            Driver();
            Console.ReadKey();
        }
    }
}