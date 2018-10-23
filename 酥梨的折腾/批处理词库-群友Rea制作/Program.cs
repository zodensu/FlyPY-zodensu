using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ConsoleApp4
{
    internal class Program
    {
        /// <summary>
        /// 定义一个结构体，存放码表
        /// </summary>
       public struct Word
        {
            public string Value { get; set; }
            public string Code { get; set; }
        }


        /// <summary>
        /// 从文本中读取数据到列表
        /// </summary>
        /// <param name="Filename">多多词库文本文件完整路径</param>
        /// <returns>返回一个列表</returns>
        public static List<Word> ReadTxtData(string Filename)
        {
            List<Word> word = new List<Word>();
            StreamReader reader = new StreamReader(new FileStream(Filename, FileMode.Open, FileAccess.Read), Encoding.UTF8);
            StringBuilder line=new StringBuilder();
            while (line.Append(reader.ReadLine()).ToString() != "")
            {
                Word w = new Word();
                w.Value= line.ToString().Split('\t')[0];
                w.Code = line.ToString().Split('\t')[1];
                word.Add(w);
                line.Clear();
            }
            return word;

        }

        public static void SaveWord(string Filename,List<Word> words)
        {
            if (File.Exists(Filename)) File.Delete(Filename);
            StringBuilder stringBuilder = new StringBuilder();
            for(int i=0;i<words.Count;i++)
            {
              stringBuilder.Append(words[i].Value + "\t" + words[i].Code + "\r\n");
            }
            Console.WriteLine(stringBuilder.ToString());
            File.WriteAllText(Filename, stringBuilder.ToString());
           
        }
        private static void Main(string[] args)
        {
            Console.WriteLine("读取码表...");
            List<Word> origin = ReadTxtData(@"d:\xhup.txt");
            List<Word> newList = ReadTxtData(@"d:\xhup.txt"); ;
            for (int i = 0; i < origin.Count; i++)
            {
                for (int j = newList.Count - 1; j >= 0; j--)
                {
                    if (newList[j].Value == origin[i].Value && newList[j].Code.Length > origin[i].Code.Length)
                    {
                        Console.WriteLine(string.Format("原词:{0} {1},删除:{2} {3},进度:{4}%",origin[i].Value,origin[i].Code,newList[j].Value,newList[j].Code,(float)(i*100/origin.Count)));
                        newList.Remove(newList[j]);
                    }
                }
            }
            SaveWord(@"d:\newxhup.txt", newList);
            Console.WriteLine("Job done!");
            Console.ReadKey();
        }




    }
}
