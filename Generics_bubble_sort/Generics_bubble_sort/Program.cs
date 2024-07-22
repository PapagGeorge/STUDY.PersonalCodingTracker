namespace Generics_bubble_sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbersToSort = { 5, 4, 2, 1, 3 };
            Bubble<int> bubbleNumbers = new Bubble<int>(numbersToSort);
            bubbleNumbers.PrintArray();
            bubbleNumbers.BubbleSortAscending();
            bubbleNumbers.PrintArray();
            bubbleNumbers.BubbleSortDescending();
            bubbleNumbers.PrintArray();

            char[] charatctersToSort = {  'a', 'd', 'c', 'b', 'e'};
            Bubble<char> bubbleCharacters = new Bubble<char>(charatctersToSort);
            bubbleCharacters.PrintArray();
            bubbleCharacters.BubbleSortAscending();
            bubbleCharacters.PrintArray();
            bubbleCharacters.BubbleSortDescending();
            bubbleCharacters.PrintArray();
        }
    }
}
