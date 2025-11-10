using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using lab2_4;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace lab2_4.tests
{
    [TestFixture]
    class DocumentWorkerTests
    {
        private StringWriter output;

        [SetUp]
        public void Setup()
        {
            output = new StringWriter();
            Console.SetOut(output);
        }
        
        [TearDown]
        public void TearDown()
        {
            output.Dispose();
        }

        private string RunProgramWithInput(string input)
        {
            var reader = new StringReader(input + Environment.NewLine);
            Console.SetIn(reader);
            Program.Run();
            return output.ToString();
        }

        [TestCase("", "Free")]
        [TestCase("pro123", "Pro")]
        [TestCase("exp456", "Expert")]
        [TestCase("wrong", "Free")]
        [TestCase("      exp456    ", "Expert")]
        [TestCase("          pro123", "Pro")]
        // [TestCase(null, "Free")]
        public void Test_Activation(string key, string expectedVersion)
        {
            string result = RunProgramWithInput(key);
            Assert.That(result, Does.Contain($"Активирована версия: {expectedVersion}"));
        }

        [Test]
        public void Test_Free_Output()
        {
            string result = RunProgramWithInput("");
            Assert.That(result, Does.Contain("Документ открыт"));
            Assert.That(result, Does.Contain("Редактирование документа доступно в версии Про"));
            Assert.That(result, Does.Contain("Сохранение документа доступно в версии Про"));
        }

        [Test]
        public void Test_Pro_Output()
        {
            string result = RunProgramWithInput("pro123");
            Assert.That(result, Does.Contain("Документ открыт"));
            Assert.That(result, Does.Contain("Документ отредактирован"));
            Assert.That(result, Does.Contain("Документ сохранён в старом формате"));
        }

        [Test]
        public void Test_Expert_Output()
        {
            string result = RunProgramWithInput("exp456");
            Assert.That(result, Does.Contain("Документ открыт"));
            Assert.That(result, Does.Contain("Документ отредактирован"));
            Assert.That(result, Does.Contain("Документ сохранён в новом формате"));
        }

        [Test]
        public void Test_Invalid_Key()
        {
            string result = RunProgramWithInput("invalid");
            Assert.That(result, Does.Contain("Ключ не найден. Активирована версия: Free"));
        }
    }
}
