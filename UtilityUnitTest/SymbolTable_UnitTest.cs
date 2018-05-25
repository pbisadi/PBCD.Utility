/*
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PBCD.Algorithms.DataStructure;

namespace UnitTestProject.DataStructureTests
{
    [TestClass]
    public class SymbolTable_UnitTest
	{
        [TestMethod]
        public void SymbolTable_Basic()
        {
            string input = "SEARCH EXAMPLE";
            var ST = new SymbolTable<char>();
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (!ST.Contains(c)) ST[c] = i;
                else ST[input[c]] = (int)ST[input[c]] + 1;
            }

            char max = '\b';
            ST[max] = 0;
            foreach (var c in ST.Keys())
            {
                if ((int)ST[c] > (int)ST[max]) max = c;
            }

            Assert.AreEqual('E', max);
        }

    }
}
*/