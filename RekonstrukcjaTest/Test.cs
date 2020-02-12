using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rekonstrukcja;

namespace RekonstrukcjaTest
{
    [TestClass]
    public class Test
    {
        // todo: this test is wrong, it shows what is current output but its incorrect, see next test for the correct output
        [TestMethod]
        public void Small1_wrong()
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
                "4",
                "3",
                "3",
                "3",
                "0;1;2"
            }) + "\n";
            Assert.AreEqual(expectedResult, stringResult);
        }
    }
}
