using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YetAnotherDependencyInjector.Tests
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
            var dataStorage = new DataStorage();
            var noteList = new NoteList(dataStorage);
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (StreamReader reader = new StreamReader("db.txt"))
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
            Injector.Map<IDataStorage,DataStorage>();

            //Arrange
            var dataStorage = Injector.Get<IDataStorage>();
            var noteList = new NoteList(dataStorage);
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (StreamReader reader = new StreamReader("db.txt"))
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
            Injector.Map<IDataStorage, DataStorage>();

            //Arrange
            var noteList = Injector.Get<NoteList>();
            var noteText = "myCustomNote";

            //Act
            noteList.Add(noteText);

            //Assert
            using (StreamReader reader = new StreamReader("db.txt"))
            {
                var all = reader.ReadToEnd();
                Assert.IsTrue(all.Contains(noteText));
            }

            File.Delete("db.txt");
        }
    }
}
