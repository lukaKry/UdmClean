﻿using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.UI.Contracts;

namespace UdmClean.UI.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage _storage;
        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "UdmClean"
            };
            _storage = new LocalStorage(config);

        }

        public void ClearStorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                _storage.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            return _storage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return _storage.Get<T>(key);
        }

        public void SetSTorageValue<T>(string key, T value)
        {
            _storage.Store(key, value);
            _storage.Persist();
        }
    }
}