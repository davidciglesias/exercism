public static class BinarySearch
{
    public static int Find(int[] input, int value)
    {
        int upper = input.Length;
        if (upper == 0) return -1;
        int lower = 0, newPosition, newValue;

        do
        {
            newPosition = (upper + lower) / 2;
            newValue = input[newPosition];
            if (value == newValue) return newPosition;
            if (newValue > value)
            {
                if (upper == newPosition) return -1;
                upper = newPosition;
            }
            else
            {
                if (lower == newPosition) return -1;
                lower = newPosition;
            }
        } while (true);
    }
}