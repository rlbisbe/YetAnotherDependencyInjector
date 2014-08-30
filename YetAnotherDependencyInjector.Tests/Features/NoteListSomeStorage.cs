using System;
using YetAnotherDependencyInjector.Tests.Services;

namespace YetAnotherDependencyInjector.Tests.Features
{
    public class NoteListSomeStorage
    {
        private readonly IDataStorage _txtStorage;
        private readonly IDataStorage _xmlStorage;

        public NoteListSomeStorage(IDataStorage txtStorage, IDataStorage xmlStorage)
        {
            _txtStorage = txtStorage;
            _xmlStorage = xmlStorage;
        }


        public void Add(string note, bool xmlStorage)
        {
            if (string.IsNullOrEmpty(note))
            {
                throw new ArgumentException("Note cannot be empty");
            }

            if (xmlStorage)
                _xmlStorage.Save(note);
            
            _txtStorage.Save(note);

        }
    }
}
