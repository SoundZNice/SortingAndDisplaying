using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortAlgorythms
{
    public interface ISort
    {
      

        IEnumerable<ExchangingPositions> getExchangingPositions();
    }

    public class ExchangingPositions
    {
        int _firstPosition;
        int _secondPosition;

        public ExchangingPositions(int x, int y)
        {
            _firstPosition = x;
            _secondPosition = y;
        }

        public void getPos(out int x, out int y)
        {
            x = _firstPosition;
            y = _secondPosition;
        }
    }

    public class BubbleSort : ISort
    {
        int[] _array;

        int _length;

        public BubbleSort(int[] array, int length)
        {
            _array = array;
            _length = length;

        }

        public IEnumerable<int[]> getStepByStepArrays()
        {
            List<int[]> arrays = new List<int[]>();

            arrays.Add(_array);

            bool flag = true;

            while (flag)
            {
                flag = false;
                for (int i = 1; i < _length; i++)
                {
                    if (_array[i - 1] > _array[i])
                    {
                        int buff = _array[i - 1];
                        _array[i - 1] = _array[i];
                        _array[i] = buff;
                        flag = true;

                        arrays.Add(_array);
                    }
                }
            }

            return arrays;
        }

        public IEnumerable<ExchangingPositions> getExchangingPositions()
        {
            bool flag = true;

            while (flag)
            {
                flag = false;
                for (int i = 1; i < _length; i++)
                {
                    if (_array[i - 1] > _array[i])
                    {
                        int buff = _array[i - 1];
                        _array[i - 1] = _array[i];
                        _array[i] = buff;
                        flag = true;

                        yield return new ExchangingPositions(i, i - 1);
                    }
                }
            }
        }

        public static void createArray(out int[] array, int elementsNumber)
        {
            array = new int[elementsNumber];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            for (int i = 0; i < 10000; i++)
            {
                swapValues(ref array[rnd.Next(elementsNumber - 1)], ref array[rnd.Next(elementsNumber - 1)]);
            }
        }

        private static void swapValues(ref int a, ref int b)
        {
            int buff = a;
            a = b;
            b = buff;
        }
    }

    public class QuickSortClass : ISort
    {
        int[] _array;

        int _length;

        List<ExchangingPositions> lep = new List<ExchangingPositions>();

        public QuickSortClass(int[] array, int length)
        {
            _array = array;
            _length = length;

        }

        public IEnumerable<int[]> getStepByStepArrays()
        {
            List<int[]> arrays = new List<int[]>();

            arrays.Add(_array);

            bool flag = true;

            while (flag)
            {
                flag = false;
                for (int i = 1; i < _length; i++)
                {
                    if (_array[i - 1] > _array[i])
                    {
                        int buff = _array[i - 1];
                        _array[i - 1] = _array[i];
                        _array[i] = buff;
                        flag = true;

                        arrays.Add(_array);
                    }
                }
            }

            return arrays;
        }

        public IEnumerable<ExchangingPositions> getExchangingPositions()
        {
            QuickSort(_array , 0, _length - 1);
            return lep;
        }

        public static void createArray(out int[] array, int elementsNumber)
        {
            array = new int[elementsNumber];
            Random rnd = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }

            for (int i = 0; i < 10000; i++)
            {
                swapValues(ref array[rnd.Next(elementsNumber - 1)], ref array[rnd.Next(elementsNumber - 1)]);
            }
        }

        private static void swapValues(ref int a, ref int b)
        {
            int buff = a;
            a = b;
            b = buff;
        }

        private void QuickSort(int[] a, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int num = a[start];

            int i = start, j = end;

            while (i < j)
            {
                while (i < j && a[j] > num)
                {
                    j--;
                }

                a[i] = a[j];
                lep.Add(new ExchangingPositions(i, j));
                while (i < j && a[i] < num)
                {
                    i++;
                }

                a[j] = a[i];
                lep.Add(new ExchangingPositions(i, j));
            }

            a[i] = num;
            
            QuickSort(a, start, i - 1);
            QuickSort(a, i + 1, end);
        }
    }
}
