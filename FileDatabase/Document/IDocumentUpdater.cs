﻿using System;
using FileDatabase.API;

namespace FileDatabase.Document
{
    public interface IDocumentUpdater<T>
    {
        IDocument<T> Update(IDatabaseName name, IDocumentId id, T model);
    }
}
