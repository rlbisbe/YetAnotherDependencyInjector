using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace YetAnotherDependencyInjector.Tests
{
    public interface IDataStorage
    {
        void Save(string note);
    }

    public class DataStorage : IDataStorage
    {
        public void Save(string note)
        {
            using (StreamWriter writer = new StreamWriter("db.txt"))
            {
                writer.WriteLine(note);   
            }
        }
    }
}
