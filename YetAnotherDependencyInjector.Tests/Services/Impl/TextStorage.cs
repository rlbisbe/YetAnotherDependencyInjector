using System.Diagnostics;
using System.IO;

namespace YetAnotherDependencyInjector.Tests.Services.Impl
{
    public class TextStorage : IDataStorage
    {
        public void Save(string note)
        {
            using (var writer = new StreamWriter("db.txt"))
            {
                writer.WriteLine(note);
            }
        }
    }
}
