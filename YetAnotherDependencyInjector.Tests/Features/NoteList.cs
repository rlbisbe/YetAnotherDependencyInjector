using System;
using YetAnotherDependencyInjector.Tests.Services;

namespace YetAnotherDependencyInjector.Tests.Features
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
