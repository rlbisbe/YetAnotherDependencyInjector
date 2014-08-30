using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YetAnotherDependencyInjector.Tests.Features;
using YetAnotherDependencyInjector.Tests.Services;
using YetAnotherDependencyInjector.Tests.Services.Impl;

namespace YetAnotherDependencyInjector.Tests.Tests
{
    [TestClass]
    public class TestInjector
    {
        [TestInitialize]
        public void Init()
        {
            Injector.Clear();
        }

        [TestMethod]
        public void TestSimpleInjectionWorks()
        {
            //Arrange
            var dataStorage = new TextStorage();
            var noteList = new NoteList(dataStorage);
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("db.txt"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("db.txt");
        }

        [TestMethod]
        public void TestInjectorWorks()
        {
            //Set injector
            Injector.Register<IDataStorage, TextStorage>();

            //Arrange
            var dataStorage = Injector.Get<IDataStorage>();
            var noteList = new NoteList(dataStorage);
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("db.txt"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("db.txt");
        }

        [TestMethod]
        public void TestInjectorGetsNoteList()
        {
            //Set injector
            Injector.Register<IDataStorage, TextStorage>();

            //Arrange
            var noteList = Injector.Get<NoteList>();
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("db.txt"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("db.txt");
        }

        [TestMethod]
        public void TestInjectorGetsXmlImplementation()
        {
            //Set injector
            Injector.Register<IDataStorage, DataXmlStorage>();

            //Arrange
            var noteList = Injector.Get<NoteList>();
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("test.xml"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("test.xml");
        }

        [TestMethod]
        public void TestInjectorGetsFirstImplementationWithEmptyParams()
        {
            //Set injector
            Injector.Register<IDataStorage, DataXmlStorage>();
            Injector.Register<IDataStorage, TextStorage>();
            //Arrange
            var noteList = Injector.Get<NoteList>();
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("test.xml"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("test.xml");
        }

        [TestMethod]
        public void TestInjectorGetsImplementationOfOneParam()
        {
            //Set injector
            Injector.Register<IDataStorage, DataXmlStorage>("XmlStorage");
            Injector.Register<IDataStorage, TextStorage>("TextStorage");
            //Arrange
            var noteList = Injector.Get<NoteList>("TextStorage");
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (var reader = new StreamReader("db.txt"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("db.txt");
        }

        [TestMethod]
        public void TestInjectorGetsImplementationOfManyParams()
        {
            //Set injector
            Injector.Register<IDataStorage, DataXmlStorage>("XmlStorage");
            Injector.Register<IDataStorage, TextStorage>("TextStorage");
            //Arrange
            var noteList = Injector.Get<NoteListSomeStorage>("TextStorage", "XmlStorage");
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText, true);

            //Assert
            using (var reader = new StreamReader("test.xml"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("test.xml");
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestInjectorThrowsExceptionIfNotExistsParam()
        {
            //Set injector
            Injector.Register<IDataStorage, DataXmlStorage>("XmlStorage");
            Injector.Register<IDataStorage, TextStorage>("TextStorage");

            //Assert
            var noteList = Injector.Get<NoteListSomeStorage>("Teststorage", "XmlStorage");
           
        }
    }
}
