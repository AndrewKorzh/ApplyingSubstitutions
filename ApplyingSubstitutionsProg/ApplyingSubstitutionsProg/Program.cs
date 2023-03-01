using System.Text;

namespace ApplyingSubstitutionsProg
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var words = new List<string>();
            words.Add("bbaab");
            words.Add("aabbbaa");
            words.Add("bababab");
            words.Add("aaaa");

            words.Add("bbbbb");
            words.Add("aabaabb");
            words.Add("abbbbba");
            words.Add("baab");

            words.Add("bbbaaa");
            words.Add("abbabba");
            words.Add("abbbaaab");
            words.Add("");

            var substitutions = new List<SUBSTITUTION>();
            substitutions.Add(new SUBSTITUTION("bb", "ba"));
            substitutions.Add(new SUBSTITUTION("ba", "a"));
            substitutions.Add(new SUBSTITUTION("a", ""));
            substitutions.Add(new SUBSTITUTION("", "b", true));


            foreach (var substitution in substitutions)
            {
                substitution.Print();
            }
            Console.WriteLine();
            foreach (var wo in words)
            {
                Console.WriteLine("Начало:");
                var w = new WORD(wo, substitutions);
                w.applying_substitutions();
                Console.WriteLine("-----Конец-----\n");
            }

        }



        static public List<SUBSTITUTION> entering_substitutions()
        {
            Console.WriteLine("Введите подстановку(если хотите завершить ввод - просто нажмите Enter)");
            Console.WriteLine("Для ввода завершающей подсстановки to начните с точки");
            Console.WriteLine("Пустая подстановка - пробел");
            var substitutions = new List<SUBSTITUTION>();
            while (true)
            {
                Console.WriteLine("From:");
                string from = Console.ReadLine();
                if (from == "")
                    break;
                Console.WriteLine("To:");
                string to = Console.ReadLine();
                if (to == "")
                    break;
                bool is_it_fin = false;
                if (to[0] == '.')
                {
                    is_it_fin = true;
                    if (to.Substring(1, to.Length - 1) == " ")
                    {
                        to = "";
                    }
                    else to = to.Substring(1, to.Length - 1);
                }
                else
                {
                    if (to == " ")
                        to = "";
                }
                if (from == " ")
                    from = "";
                substitutions.Add(new SUBSTITUTION(from, to, is_it_fin));
            }
            Console.WriteLine("Подстановки:");

            return substitutions;




        }
    }


    class WORD
    {
        public StringBuilder word = new StringBuilder("");
        public List<SUBSTITUTION> substitutions;
        public List<string> history_of_words;

        public WORD(string w, List<SUBSTITUTION> s)
        {
            word = new StringBuilder(w); this.substitutions = s; history_of_words = new List<string>();
        }

        /// <summary>
        /// Попытка применения подстановки
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public bool attempt_to_apply_substitution(SUBSTITUTION sub)
        {
            if (word.ToString().Contains(sub.from))
            {
                var i = word.ToString().IndexOf(sub.from);

                word.Remove(i, sub.from.Length);
                word.Insert(i, sub.to);
                if (sub.the_final)
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
        /// <summary>
        /// Попытка применения подстановок из списка
        /// </summary>
        /// <returns></returns>
        public bool attempt_to_apply_substitution_from_list()
        {
            for (int i = 0; i < substitutions.Count; i++)
            {

                if (word.ToString().Contains(substitutions[i].from))
                {
                    Print();
                    if (attempt_to_apply_substitution(substitutions[i]))
                        return true;
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Применение подстановок
        /// </summary>
        public void applying_substitutions()
        {
            while (true)
            {

                if (!attempt_to_apply_substitution_from_list())
                {
                    Print();
                    return;
                }
                //Замедление - чтобы в случае бесконечного увеличенияя слова - было понятно, что происходит
                System.Threading.Thread.Sleep(500);
                if (history_of_words.Contains(word.ToString()))
                {

                    Console.WriteLine($"слово {word.ToString()} уже встречалось");
                    return;
                }
                history_of_words.Add(word.ToString());
            }

        }
        public string ToStr()
        {
            if (word.ToString() == "")
                return "_";
            return word.ToString();
        }
        public void Print()
        {

            Console.WriteLine(ToStr());
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