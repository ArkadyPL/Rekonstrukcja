﻿using System;
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
            var amoutOfVertices = "7";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
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
            var amoutOfVertices = "9";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
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
            var amoutOfVertices = "18";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
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
            var amoutOfVertices = "16";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
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

        [TestMethod]
        public void Large1()
        {
            // GIVEN
            var distanceMatrix = new int[7, 7] { {  0,  4,  8,  9, 14, 15, 16 },
                                                 {  4,  0,  8,  9, 14, 15, 16 },
                                                 {  8,  8,  0,  7, 12, 13, 14 },
                                                 {  9,  9,  7,  0,  9, 10, 11 },
                                                 { 14, 14, 12,  9,  0,  9, 10 },
                                                 { 15, 15, 13, 10,  9,  0,  5 },
                                                 { 16, 16, 14, 11, 10,  5,  0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var amoutOfVertices = "30";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
                "7",
                "9",
                "18",
                "21",
                "27",
                "10",
                "13",
                "0;8",
                "7;9;14",
                "1;8",
                "5;11",
                "10;12;26",
                "11;13",
                "6;12",
                "8;15",
                "14;16",
                "15;17;19",
                "16;18",
                "2;17",
                "16;20",
                "19;21;22",
                "3;20",
                "20;23",
                "22;24",
                "23;25;29",
                "24;26",
                "11;25",
                "4;28",
                "27;29",
                "24;28"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        public void Large2()
        {
            // GIVEN
            var distanceMatrix = new int[7, 7] { {  0,  6, 10, 13, 18, 24, 18 },
                                                 {  6,  0,  6,  9, 14, 20, 14 },
                                                 { 10,  6,  0,  9, 14, 20, 14 },
                                                 { 13,  9,  9,  0,  9, 15,  9 },
                                                 { 18, 14, 14,  9,  0, 14,  8 },
                                                 { 24, 20, 20, 15, 14,  0,  8 },
                                                 { 18, 14, 14,  9,  8,  8,  0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var amoutOfVertices = "36";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
                "7",
                "11",
                "15",
                "20",
                "26",
                "30",
                "29",
                "0;8",
                "7;9",
                "8;10",
                "9;11",
                "1;10;12",
                "11;13",
                "12;14;16",
                "13;15",
                "2;14",
                "13;17",
                "16;18",
                "17;19",
                "18;20;21",
                "3;19",
                "19;22",
                "21;23",
                "22;24;27",
                "23;25",
                "24;26",
                "4;25",
                "23;28",
                "27;29",
                "6;28;35",
                "5;31",
                "30;32",
                "31;33",
                "32;34",
                "33;35",
                "29;34",
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        public void Validation_15_06_2019()
        {
            // GIVEN
            var distanceMatrix = new int[10, 10] { { 0, 3, 2, 3, 5, 3, 4, 5, 5, 3 },
                                                   { 3, 0, 3, 2, 4, 2, 3, 4, 4, 2 },
                                                   { 2, 3, 0, 3, 5, 3, 4, 5, 5, 3 },
                                                   { 3, 2, 3, 0, 4, 2, 3, 4, 4, 2 },
                                                   { 5, 4, 5, 4, 0, 4, 3, 2, 2, 4 },
                                                   { 3, 2, 3, 2, 4, 0, 3, 4, 4, 2 },
                                                   { 4, 3, 4, 3, 3, 3, 0, 3, 3, 3 },
                                                   { 5, 4, 5, 4, 2, 4, 3, 0, 2, 4 },
                                                   { 5, 4, 5, 4, 2, 4, 3, 2, 0, 4 },
                                                   { 3, 2, 3, 2, 4, 2, 3, 4, 4, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var amoutOfVertices = "14";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
                "10",
                "11",
                "10",
                "11",
                "13",
                "11",
                "12",
                "13",
                "13",
                "11",
                "0;2;11",
                "1;3;5;9;10;12",
                "6;11;13",
                "4;7;8;12"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Wrong input table!")]
        public void Validation_05_11_2019_detecting_incorrect_matrix()
        {
            // GIVEN
            var distanceMatrix = new int[3, 3] { { 0, 3, 2 },
                                                 { 3, 0, 2 },
                                                 { 2, 2, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            // expect exception
        }

        [TestMethod]
        public void Validation_02_02_2020()
        {
            // GIVEN
            var distanceMatrix = new int[9, 9] { { 0, 5, 4, 6, 5, 6, 5, 6, 7 },
                                                 { 5, 0, 5, 7, 2, 7, 6, 7, 4 },
                                                 { 4, 5, 0, 4, 5, 4, 3, 4, 7 },
                                                 { 6, 7, 4, 0, 7, 4, 5, 4, 9 },
                                                 { 5, 2, 5, 7, 0, 7, 6, 7, 4 },
                                                 { 6, 7, 4, 4, 7, 0, 5, 2, 9 },
                                                 { 5, 6, 3, 5, 6, 5, 0, 5, 8 },
                                                 { 6, 7, 4, 4, 7, 2, 5, 0, 9 },
                                                 { 7, 4, 7, 9, 4, 9, 8, 9, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var amoutOfVertices = "20";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
                "15",
                "9",
                "13",
                "11",
                "9",
                "10",
                "14",
                "10",
                "19",
                "1;4;17;18",
                "5;7;12",
                "3;12",
                "10;11;13",
                "2;12;14;16",
                "6;13",
                "0;16",
                "13;15;17",
                "9;16",
                "9;19",
                "8;18"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }

        [TestMethod]
        public void Validation_02_03_2020()
        {
            // GIVEN
            var distanceMatrix = new int[14, 14] { { 0, 7, 7, 4,  8, 6, 4,  8,  4, 5,  8,  4, 7, 2 },
                                                   { 7, 0, 4, 5,  5, 3, 5,  5,  9, 8,  5,  9, 4, 7 },
                                                   { 7, 4, 0, 5,  3, 3, 5,  5,  9, 8,  5,  9, 4, 7 },
                                                   { 4, 5, 5, 0,  6, 4, 2,  6,  6, 5,  6,  6, 5, 4 },
                                                   { 8, 5, 3, 6,  0, 4, 6,  6, 10, 9,  6, 10, 5, 8 },
                                                   { 6, 3, 3, 4,  4, 0, 4,  4,  8, 7,  4,  8, 3, 6 },
                                                   { 4, 5, 5, 2,  6, 4, 0,  6,  6, 5,  6,  6, 5, 4 },
                                                   { 8, 5, 5, 6,  6, 4, 6,  0, 10, 9,  2, 10, 5, 8 },
                                                   { 4, 9, 9, 6, 10, 8, 6, 10,  0, 7, 10,  4, 9, 4 },
                                                   { 5, 8, 8, 5,  9, 7, 5,  9,  7, 0,  9,  7, 8, 5 },
                                                   { 8, 5, 5, 6,  6, 4, 6,  2, 10, 9,  0, 10, 5, 8 },
                                                   { 4, 9, 9, 6, 10, 8, 6, 10,  4, 7, 10,  0, 9, 4 },
                                                   { 7, 4, 4, 5,  5, 3, 5,  5,  9, 8,  5,  9, 0, 7 },
                                                   { 2, 7, 7, 4,  8, 6, 4,  8,  4, 5,  8,  4, 7, 0 } };

            // WHEN
            var result = new TreeFinder().FindTree(distanceMatrix);

            // THEN
            var neighborsList = Utils.ConvertTreeToNeighboursList(result);
            var stringResult = Utils.WriteNeighborsListToString(neighborsList);
            var amoutOfVertices = "UNKNOWN";
            var expectedResult = string.Join("\n", new string[] { amoutOfVertices,
                "UNKNOWN"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }
    }
}
