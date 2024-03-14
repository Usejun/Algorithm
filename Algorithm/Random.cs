namespace Algorithm
{
    public static class Random
    {
        private readonly static System.Random r = new System.Random();

        public static int Int(int min = int.MinValue, int max = int.MaxValue)
        {
            return r.Next(min, max);
        }

        public static int[] Range(int min, int max, int length)
        {
            int[] result = new int[length];

            for (int i = 0; i < length; i++)
                result[i] = Int(min, max);

            return result;
        }

    }
}
