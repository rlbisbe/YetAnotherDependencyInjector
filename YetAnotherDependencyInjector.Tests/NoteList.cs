using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherDependencyInjector.Tests
{
public class NoteList
{
    private IDataStorage _dataStorage;
    
    public NoteList(IDataStorage dataStorage)
    {
        _dataStorage = dataStorage;
    }


    public void Add(string note)
    {
        if (string.IsNullOrEmpty(note))
        {
            throw new ArgumentException("Note cannot be empty");
        }

        _dataStorage.Save(note);
    }
}
}
