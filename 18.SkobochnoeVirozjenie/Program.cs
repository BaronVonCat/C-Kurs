using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _18.SkobochnoeVirozjenie
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string parenthesisExpression;
            char startParenthesis = '(';
            char endParenthesis = ')';
            int sum = 0;
            int sumDepthOfExpression = 0;
            int depthOfExpression = 0;
            int thresholdValue = 0;
            int trueValue = 0;

            Console.Write("Введите скобочное выражение: ");
            parenthesisExpression = Console.ReadLine();

            foreach (var symbol in parenthesisExpression)
            {
                depthOfExpression = sumDepthOfExpression;

                if (symbol == startParenthesis)
                {
                    sum++;
                }
                else if (symbol == endParenthesis)
                {
                    sum--;
                }

                if (sum < thresholdValue)
                {
                    break;
                }

                if (sum > depthOfExpression)
                {
                    sumDepthOfExpression++;
                }
            }

            if (sum != trueValue)
            {
                Console.WriteLine("Некоректное скобочное выражение.");
            }
            else
            {
                Console.WriteLine("Данное скобочное выражение является корректным");
                Console.WriteLine("Максимальная глубина скобочного выражения равна - "
                    + depthOfExpression);
            }
        }
    }
}
