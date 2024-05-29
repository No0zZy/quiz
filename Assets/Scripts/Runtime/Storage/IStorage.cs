﻿namespace HGtest.Storage
{
    public interface IStorage
    {
        void Save<T>(string key, T data);
        T Load<T>(string key);
        bool IsExists(string key);
    }
}