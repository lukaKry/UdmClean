using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdmClean.UI.Contracts
{
    public interface ILocalStorageService
    {
        void ClearStorage(List<string> keys);
        bool Exists(string key);
        T GetStorageValue<T>(string key);
        void SetSTorageValue<T>(string key, T value);
    }
}
