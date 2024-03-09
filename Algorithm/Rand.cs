namespace Algorithm
{
    public static class Rand
    {
        private readonly static System.Random r = new System.Random();

        public static int Int(int min = 0, int max = int.MaxValue)
        {
            return r.Next(min, max);
        }
    }
}
