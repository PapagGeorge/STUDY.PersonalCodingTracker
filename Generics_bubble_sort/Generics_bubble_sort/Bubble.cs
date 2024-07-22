namespace Generics_bubble_sort
{
    public class Bubble <T> where T : IComparable<T>
    {
        public T[] Array { get; set; }

        public Bubble(T[] array)
        {
            Array = array;
        }

        public T[] BubbleSortAscending()
        {
            for (int i = 0; i < Array.Length - 1; i++)
            {
                for (int j = 0; j < Array.Length - i - 1; j++)
                {
                    if (Array[j].CompareTo(Array[j + 1]) > 0)
                    {
                        Swap(j, j + 1);
                    }
                }
            }
            return Array;
        }

        public T[] BubbleSortDescending()
        {
            for (int i = 0; i < Array.Length - 1; i++)
            {
                for (int j = 0; j < Array.Length - i - 1; j++)
                {
                    if (Array[j].CompareTo(Array[j + 1]) < 0)
                    {
                        Swap(j, j + 1);
                    }
                }
            }
            return Array;
        }

        private void Swap(int index1, int index2)
        {
            T temp = Array[index1];
            Array[index1] = Array[index2];
            Array[index2] = temp;
        }
    }
}
