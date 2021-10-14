using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Syntax_Analyzer
{
    class PSA
    {
        ArrayList resultList = new ArrayList();

        Stack stack = new Stack();

        List<string> stackStringList= new List<string>();
        
        string EnteredString { get; set; }

        PSA(string enteredString)
        {
            EnteredString = enteredString;
        }
        
        public bool Analyz()
        {
            RowSet firstTmpRow = new RowSet();
            bool isErrorInFunc = false;
            bool isRunCycle = true;
            int F_to_brackets = 0;

            stack.Push("$");
            firstTmpRow.Stak = stack.Pop().ToString();
            firstTmpRow.Enter = EnteredString.Substring(0, 1);
            stack.Clear();

            while (isRunCycle)
            {
                ///Neterminal analyz
                /***
                * Set of instruction for <E>
                ***/
                if (firstTmpRow.Stak == "$")
                {
                    try
                    {
                        if (EnteredString.Substring(0, 1) != "+" && EnteredString.Substring(0, 1) != "*" && EnteredString.Substring(0, 1) != ")" && EnteredString.Substring(0, 1) != "$")
                        {
                            var secondTmpRow = new RowSet();

                            secondTmpRow.Stak = firstTmpRow.Stak;
                            secondTmpRow.Enter = EnteredString;
                            secondTmpRow.Product = firstTmpRow.Product = "<E>";
                            stack.Push("$");
                            stack.Push("<E>");
                            firstTmpRow.Stak = "";
                            
                        }
                        else
                        {
                        ShowError();
                        break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }

                var tempRow = new RowSet();
                /***
                * Set of instruction for <E>
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "<E>")
                {
                    try
                    {
                        if (EnteredString.Substring(0, 1) != "+" && EnteredString.Substring(0, 1) != "*" && EnteredString.Substring(0, 1) != ")" && EnteredString.Substring(0, 1) != "$")
                        {
                            tempRow.Stak = StackToString();
                            tempRow.Enter = EnteredString;
                            tempRow.Product = "<E>→<T><E2>";
                            stack.Pop();
                            resultList.Add(tempRow);
                            stack.Push("<E2>");
                            stack.Push("<T>");
                        }
                        else
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }

                /***
                * Set of instruction for <E2>
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "<E2>")
                {
                    try
                    {

                        if (EnteredString.Substring(0, 1) != "a" && EnteredString.Substring(0, 1) != "b" && EnteredString.Substring(0, 1) != "c" && EnteredString.Substring(0, 1) != "*" && EnteredString.Substring(0, 1) != "(")
                        {
                            if (EnteredString.Substring(0, 1) == ")" || EnteredString.Substring(0, 1) == "$")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<E2>→e";
                                stack.Pop();
                                resultList.Add(tempRow);

                            }
                            else if (EnteredString.Substring(0, 1) == "+")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<E2>→+<T><E2>";
                                stack.Pop();
                                resultList.Add(tempRow);
                                stack.Push("<E2>");
                                stack.Push("<T>");
                                stack.Push("+");
                            }
                        }
                        else
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }

                /***
                * Set of instruction for <T>
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "<T>")
                {
                    try
                    {
                        if (EnteredString.Substring(0, 1) != "+" && EnteredString.Substring(0, 1) != "*" && EnteredString.Substring(0, 1) != ")" && EnteredString.Substring(0, 1) != "$")
                        {
                            tempRow.Stak = StackToString();
                            tempRow.Enter = EnteredString;
                            tempRow.Product = "<T>→<F><T2>";
                            stack.Pop();
                            resultList.Add(tempRow);
                            stack.Push("<T2>");
                            stack.Push("<F>");
                        }
                        else
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }

                /***
                * Set of instruction for <T2>
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "<T2>")
                {
                    try
                    {
                        if (EnteredString.Substring(0, 1) != "a" && EnteredString.Substring(0, 1) != "b" && EnteredString.Substring(0, 1) != "c" && EnteredString.Substring(0, 1) != "(")
                        {
                            if (EnteredString.Substring(0, 1) == "+" || EnteredString.Substring(0, 1) == ")" || EnteredString.Substring(0, 1) == "$")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<T2>→e";
                                stack.Pop();
                                resultList.Add(tempRow);

                            }
                            else if (EnteredString.Substring(0, 1) == "*")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<Е2>→*<F><T2>";
                                stack.Pop();
                                resultList.Add(tempRow);
                                stack.Push("<T2>");
                                stack.Push("<F>");
                                stack.Push("*");
                            }
                        }
                        else
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }

                /***
                 * Set of instruction for <F>
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "<F>")
                {
                    try
                    {
                        if (EnteredString.Substring(0, 1) != "+" && EnteredString.Substring(0, 1) != "*" && EnteredString.Substring(0, 1) != ")" && EnteredString.Substring(0, 1) != "$")
                        {
                            if (EnteredString.Substring(0, 1) == "a")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<F>→a";
                                stack.Pop();
                                resultList.Add(tempRow);
                                stack.Push("a");
                                continue;
                            }
                            if (EnteredString.Substring(0, 1) == "b")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<F>→b";
                                stack.Pop();
                                resultList.Add(tempRow);
                                stack.Push("b");
                                continue;
                            }
                            if (EnteredString.Substring(0, 1) == "c")
                            {
                                tempRow.Stak = StackToString();
                                tempRow.Enter = EnteredString;
                                tempRow.Product = "<F>→c";
                                stack.Pop();
                                resultList.Add(tempRow);
                                stack.Push("c");
                                continue;
                            }
                            if (EnteredString.Substring(0, 1) == "(")
                            {
                                if (F_to_brackets != 0)
                                {
                                    tempRow.Stak = StackToString();
                                    tempRow.Enter = EnteredString;
                                    tempRow.Product = "<F>→" + EnteredString.Substring(1, 1);
                                    stack.Pop();
                                    resultList.Add(tempRow);
                                    stack.Push(EnteredString.Substring(1, 1));
                                    EnteredString = EnteredString.Substring(1);
                                    continue;
                                }
                                else
                                {
                                    tempRow.Stak = StackToString();
                                    tempRow.Enter = EnteredString;
                                    tempRow.Product = "<F>→(<E>)";
                                    stack.Pop();
                                    resultList.Add(tempRow);
                                    stack.Push(")");
                                    stack.Push("<E>");
                                    stack.Push("(");
                                    F_to_brackets++;
                                }
                            }
                            else
                            {
                                ShowError();
                                break;
                            }
                        }
                       
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                    continue;
                }


               
                ///Word analyz
                /***
                 * Set of instruction for 'a'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "a")
                {

                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    try
                    {
                        if (EnteredString.Substring(0,1)=="b" || EnteredString.Substring(0, 1) == "c")
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch 
                    {

                        ShowError();
                        break;
                    }
                resultList.Add(tempRow);

                    continue;
                }

                /***
                 * Set of instruction for 'b'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "b")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    try
                    {
                        if (EnteredString.Substring(0, 1) == "a" || EnteredString.Substring(0, 1) == "c")
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch 
                    {
                        ShowError();
                        break;
                    }

                resultList.Add(tempRow);
                    continue;
                }
                
                /***
                 * Set of instruction for 'c'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "c")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    //System.ArgumentOutOfRangeException
                    try
                    {
                        if (EnteredString.Substring(0, 1) == "a" || EnteredString.Substring(0, 1) == "b")
                        {
                            ShowError();
                            break;
                        }
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }

                    resultList.Add(tempRow);
                    continue;
                }

                /***
                 * Set of instruction for '+'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "+")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    resultList.Add(tempRow);
                    continue;
                }

                /***
                 * Set of instruction for '*'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "*")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    resultList.Add(tempRow);
                    continue;
                }

                /***
                 * Set of instruction for '('
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "(")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    resultList.Add(tempRow);
                    continue;
                }

                /***
                 * Set of instruction for ')'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == ")")
                {
                    tempRow.Stak = StackToString();
                    tempRow.Enter = EnteredString;
                    tempRow.Product = " ";
                    stack.Pop();
                    EnteredString = EnteredString.Substring(1);
                    resultList.Add(tempRow);
                    continue;
                }

                /***
                * Set of instruction for '$'
                ***/
                if (stack.Count != 0 && stack.Peek().ToString() == "$")
                {
                    try
                    {
                        if(EnteredString.Substring(0,1)=="$")
                        {
                            tempRow.Stak = StackToString();
                            tempRow.Enter = EnteredString;
                            tempRow.Product = " ";
                            stack.Pop();
                            EnteredString = EnteredString.Substring(1);
                            resultList.Add(tempRow);
                            isErrorInFunc = true;
                            break;
                        }
                        else 
                        {
                            ShowError();
                            break;
                        }
                        
                    }
                    catch
                    {
                        ShowError();
                        break;
                    }
                }

                else
                {
                    isRunCycle = false;
                }
            }
            return isErrorInFunc;
        }

        string NextStackString()
        {
            string nextString = "";
            foreach (var pop in stack)
            {
                nextString = nextString + pop;
            }
            return nextString;
        }

        string LastStackString()
        {
            int count = stack.Count;
            int i = 1;
            string firstString = "";
            foreach (var pop in stack)
            {
                if (count == i)
                {
                    firstString = pop.ToString();
                }
                i++;
            }
            return firstString;
        }

        string StackToString() // FILO
        {
            stackStringList.Clear();
            string str = "";
            foreach (var pop in stack)
            {
                stackStringList.Add(pop.ToString());
            }
            stackStringList.Reverse();
            foreach (var pop in stackStringList)
            {
                str += pop;

            }
            return str;
        }

        string StackNewString() 
        {
            foreach (var pop in stack)
            {
                stackStringList.Add(pop.ToString());
            }
            stack.Clear();
            foreach (var item in stackStringList)
            {
                stack.Push(item);
            }
            return NextStackString();
        }

        public void ShowResult()
        {
            int j = 1;
            Console.WriteLine("\n № \t |  Stack  \t\t\t\t\t|  Symbol      \t|\tProdukt  \n");
            foreach (var TmpRowObj in resultList)
            {
                RowSet TRow = (RowSet)TmpRowObj;
                if (TRow.Stak.Length <= 5)
                {
                    Console.WriteLine(" " + j + "\t |  " + TRow.Stak + "     \t\t\t\t\t|  " + TRow.Enter + "    \t|\t" + TRow.Product);
                }
                else if (TRow.Stak.Length >= 10 && TRow.Stak.Length < 17) 
                {
                    Console.WriteLine(" " + j + "\t |  " + TRow.Stak + "     \t\t\t\t|  " + TRow.Enter + "    \t|\t" + TRow.Product);
                }
                else if (TRow.Stak.Length >= 17)
                {
                    Console.WriteLine(" " + j + "\t |  " + TRow.Stak + "   \t\t\t|  " + TRow.Enter + "    \t|\t" + TRow.Product);
                }
                else
                {
                    Console.WriteLine(" " + j + "\t |  " + TRow.Stak + "     \t\t\t\t|  " + TRow.Enter + "    \t|\t" + TRow.Product);
                }
                j += 1;
            }
            Console.WriteLine();
        }

        public static void ShowError()
        {
            Console.WriteLine("\n--- You Have An Error!!! ---\n");
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Enter The Expression :\t");
                var psa = new PSA(Console.ReadLine());
                if (psa.Analyz())
                {
                    psa.ShowResult();
}   }   }   } 
}
