﻿using VietSearch.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VietSearchWebService.Models;
using System.Collections.Generic;
using VietSearchWebService.Models.AutoCompleteProvider;

namespace TestProject
{
    
    
    /// <summary>
    ///This is a test class for StringHelperTest and is intended
    ///to contain all StringHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringHelperTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for StandardizeString
        ///</summary>
        [TestMethod()]
        public void StandardizeStringTest()
        {
            AutoCompleteProvider a = new AutoCompleteProvider();
            List<string> strs = a.Suggest("cafe duong an duong vuong");
            string s = "Lý   Thường    Kiệt"; // TODO: Initialize to an appropriate value
            string expected = "lythuongkiet"; // TODO: Initialize to an appropriate value
            string actual;
            actual = StringHelper.StandardizeString(s);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
