using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5._5.ObjedinenieVOdnuCollekciyu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] listCandidatesOld = { "Ходжа - Клерк", "Прифти - Шифровальщик", "Хубер - Куратор" };
            string[] listCandidatesNew = { "Бекташи - Ордината", "Прифти - Шифровальщик", "Ходжа - Клерк" };
            List<string> candidates = CombineLists(listCandidatesOld, listCandidatesNew);

            OutputArray(listCandidatesNew);
            OutputArray(listCandidatesOld);
            OutputList(candidates);
            Console.ReadKey();
        }

        static void OutputArray(string[] listDataset)
        {
            for (int i = 0; i < listDataset.Length; i++)
            {
                Console.WriteLine(listDataset[i]);
            }

            Console.WriteLine();
        }

        static void OutputList(List<string> listDataset)
        {
            foreach (var data in listDataset)
            {
                Console.WriteLine(data);
            }

            Console.WriteLine();
        }

        static List<string> CombineLists(string[] arrayDataset1, string[] arrayDataset2)
        {
            List<string> combinedListDataset = new List<string>();

            AddNonExistingData(arrayDataset1, combinedListDataset);
            AddNonExistingData(arrayDataset2, combinedListDataset);
            return combinedListDataset;
        }

        static void AddNonExistingData(string[] arrayDataset, List<string> listDataset)
        {
            foreach (var data in arrayDataset)
            {
                if (listDataset.Contains(data) == false)
                {
                    listDataset.Add(data);
                }
            }
        }
    }
}
