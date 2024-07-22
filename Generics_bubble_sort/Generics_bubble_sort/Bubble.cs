namespace Generics_bubble_sort
{
    public static class Bubble <T>
    {

        public static T[] BubbleSortAscending<T>(T[] array) where T : IComparable<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap<T>(array, j, j+1);
                    }
                }    
            }
            
            return array;
        }

        private static void Swap<T>(T[] arrayToSwap, int index1, int index2)
        {
            T temp = arrayToSwap[index1];
            arrayToSwap[index1] = arrayToSwap[index2];
            arrayToSwap[index2] = temp;
        }
    }
}
