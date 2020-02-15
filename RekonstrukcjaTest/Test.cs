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
            var distanceMatrix = new int[3, 3]{ { 0, 4, 4},
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

        [TestMethod]
        public void Medium1()
        {
            // GIVEN
            var distanceMatrix = new int[5, 5]{ { 0,  5,  9,  9, 8 },
                                                { 5,  0, 10, 10, 9 },
                                                { 9, 10,  0,  8, 7 },
                                                { 9, 10,  8,  0, 3 },
                                                { 8,  9,  7,  3, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var expectedResult = string.Join("\n", new string[] {
                "18",
                "7",
                "10",
                "15",
                "5",
                "6",
                "3;6",
                "4;5;14",
                "0;8",
                "7;9;11",
                "8;10",
                "1;9",
                "8;12",
                "11;13",
                "12;14;17",
                "6;13",
                "2;16",
                "15;17",
                "13;16"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        public void Medium2()
        {
            // GIVEN
            var distanceMatrix = new int[5, 5]{ { 0, 6, 7, 7, 8 },
                                                { 6, 0, 7, 7, 8 },
                                                { 7, 7, 0, 6, 7 },
                                                { 7, 7, 6, 0, 3 },
                                                { 8, 8, 7, 3, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var expectedResult = string.Join("\n", new string[] {
                "16",
                "11",
                "14",
                "7",
                "5",
                "6",
                "3;6;10",
                "4;5",
                "2;8",
                "7;9",
                "8;10;13",
                "5;9",
                "0;12",
                "11;13",
                "9;12;15",
                "1;15",
                "13;14"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }
    }
}
