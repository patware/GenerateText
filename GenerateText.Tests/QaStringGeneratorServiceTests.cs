using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenerateText.Services;

namespace GenerateText.Tests
{
    [TestClass]
    public class QaStringGeneratorServiceTests
    {
        private const string Six = "123456";

        [TestMethod]
        public void Generate_Negative_Count_Characters_Returns_AlphabetSoup()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(-1);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup, actual);
        }

        [TestMethod]
        public void Generate_0_Characters_Returns_AlphabetSoup()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(0);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup, actual);
        }

        [TestMethod]
        public void Generate_1_Characters_Returns_AlphabetSoup()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(1);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup.Substring(0,1), actual);
        }

        [TestMethod]
        public void Generate_Negative_Count_Characters_Returns_Six()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(-1, Six);

            //Assert
            Assert.AreEqual(Six, actual);
        }

        [TestMethod]
        public void Generate_0_Characters_Returns_Six()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(0, Six);

            //Assert
            Assert.AreEqual(Six, actual);
        }

        [TestMethod]
        public void Generate_1_Characters_Returns_1()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(1, Six);

            //Assert
            Assert.AreEqual("1", actual);
        }
        
        [TestMethod]
        public void Generate_7_Characters_Six_Returns_7()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(7, Six);

            //Assert
            Assert.AreEqual("1234561", actual);
        }


        [TestMethod]
        public void Generate_Negative_Count_Characters_Returns_AlphabetSoup_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();

            //Act
            var actual = service.Generate(-1,true);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup + "_30_", actual);
        }

        [TestMethod]
        public void Generate_0_Characters_Returns_AlphabetSoup_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();

            //Act
            var actual = service.Generate(0, true);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup + "_30_", actual);
        }

        [TestMethod]
        public void Generate_3_Characters_Returns_AlphabetSoup_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(3, true);

            //Assert
            Assert.AreEqual("_3_", actual);
        }

        [TestMethod]
        public void Generate_4_Characters_Returns_AlphabetSoup_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(4, true);

            //Assert
            Assert.AreEqual(QaStringGeneratorService.AlphabetSoup.Substring(0,1) + "_4_", actual);
        }
        [TestMethod]
        public void Generate_Negative_Count_Characters_Returns_Six_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();

            //Act
            var actual = service.Generate(-1, Six, true);
            
            //Assert
            Assert.AreEqual("123456_9_", actual);
        }

        [TestMethod]
        public void Generate_0_Characters_Returns_Six_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(0, Six, true);

            //Assert
            Assert.AreEqual("123456_9_", actual);
        }

        [TestMethod]
        public void Generate_3_Characters_Returns_Six_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(3, Six,true);

            //Assert
            Assert.AreEqual("_3_", actual);
        }

        [TestMethod]
        public void Generate_4_Characters_Returns_1_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(4, Six, true);

            //Assert
            Assert.AreEqual("1_4_", actual);
        }


        [TestMethod]
        public void Generate_7_Characters_Returns_4_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(7, Six, true);

            //Assert
            Assert.AreEqual("1234_7_", actual);
        }


        [TestMethod]
        public void Generate_10_Characters_With_Six_Returns_NoWrap_Six_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(10, Six, true);

            //Assert
            Assert.AreEqual("123456_10_", actual);
        }



        [TestMethod]
        public void Generate_11_Characters_With_Six_Returns_Wraps_Six_QaApproved()
        {
            //Arrange
            IQaStringGeneratorService service = new QaStringGeneratorService();
            //Act
            var actual = service.Generate(11, Six, true);

            //Assert
            Assert.AreEqual("1234561_11_", actual);
        }



    }
}
