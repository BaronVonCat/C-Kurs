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
            List<string> candidates = new List<string>();

            OutputArray(listCandidatesNew);
            OutputArray(listCandidatesOld);
            candidates = CombineLists(listCandidatesOld, listCandidatesNew);
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
            List<string> filteredListDataset = new List<string>();

            CopyDataArrayToList(arrayDataset1, combinedListDataset);
            CopyDataArrayToList(arrayDataset2, combinedListDataset);

            foreach (var dataList in combinedListDataset)
            {
                if (filteredListDataset.Contains(dataList) == false)
                {
                    filteredListDataset.Add(dataList);
                }
            }

            return filteredListDataset;
        }

        static void CopyDataArrayToList(string[] arrayDataset, List<string> ListDataset)
        {
            for (int i = 0; i < arrayDataset.Length; i++)
            {
                ListDataset.Add(arrayDataset[i]);
            }
        }
    }
}
