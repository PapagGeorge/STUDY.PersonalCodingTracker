namespace ShortingAlgorithms.Algorithms
{
    public static class SortingFunctions
    {
        // Bubble Sort algorithm
        public static List<int> BubbleSortNumbers(List<int> numbersToShort)
        {
            int numbersToShortLength = numbersToShort.Count;

            // Outer loop for each pass
            for (int i = 0; i < numbersToShortLength - 1; i++)
            {
                // Inner loop for comparing adjacent elements
                for (int j = 0; j < numbersToShortLength - 1 - i; j++)
                {
                    if (numbersToShort[j] > numbersToShort[j + 1])
                    {
                        int temp = numbersToShort[j];
                        numbersToShort[j] = numbersToShort[j + 1];
                        numbersToShort[j + 1] = temp;
                    }
                }
            }

            return numbersToShort;
        }

        public static List<int> MergeSortNumbers(List<int> numbersToShort)
        {
            throw new NotImplementedException();
        }

        private static List<int> Merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (left[leftIndex] <= right[rightIndex])
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            result.AddRange(left.GetRange(leftIndex, left.Count - leftIndex));
            result.AddRange(right.GetRange(rightIndex, right.Count - rightIndex));

            return result;
        }

        public static List<int> QuickSortNumbers(List<int> numbersToShort)
        {
            throw new NotImplementedException();
        }

        public static List<int> InsertionSortNumbers(List<int> numbersToShort)
        {
            throw new NotImplementedException();
        }

        public static List<int> SelectionSortNumbers(List<int> numbersToShort)
        {
            throw new NotImplementedException();
        }

        public static List<int> HeapSortNumbers(List<int> numbersToShort)
        {
            throw new NotImplementedException();
        }
    }
}
