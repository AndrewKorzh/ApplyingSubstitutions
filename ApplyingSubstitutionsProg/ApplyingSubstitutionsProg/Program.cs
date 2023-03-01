namespace ApplyingSubstitutionsProg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JustTest!");
        }
    }
    class SUBSTITUTION
    {

        public string from;
        public string to;
        public bool the_final;
        public SUBSTITUTION(string f, string t, bool t_f = false)
        {
            from = f;
            to = t;
            the_final = t_f;
        }

        public string ToStr()
        {
            return $"{(from == "" ? "_" : from)} ->{(the_final ? ". " : " ")}{(to == "" ? "_" : to)}";
        }
        public void Print()
        {
            Console.WriteLine(ToStr());
        }



    }
}