using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusgramClient.Views
{
    public interface IPasswordSupplier
    {
        string GetPassword();
    }
}
