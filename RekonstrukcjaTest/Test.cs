using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rekonstrukcja;

namespace RekonstrukcjaTest
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Small1()
        {
            // GIVEN
            var distanceMatrix = new int[3,3]{ { 0, 4, 4},
                                               { 4, 0, 4},
                                               { 4, 4, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var expectedResult = string.Join("\n", new string[] {
                "7",
                "3",
                "5",
                "6",
                "0;4",
                "3;5;6",
                "1;4",
                "2;4"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        public void Small2()
        {
            // GIVEN
            var distanceMatrix = new int[3, 3]{ { 0, 7, 4 },
                                                { 7, 0, 5 },
                                                { 4, 5, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var expectedResult = string.Join("\n", new string[] {
                "9",
                "3",
                "6",
                "5",
                "0;4",
                "3;5",
                "2;4;8",
                "1;7",
                "6;8",
                "5;7"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }
    }
}
