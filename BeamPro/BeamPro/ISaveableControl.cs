using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamPro
{
    public interface ISaveableControl
    {
        IEnumerable<string> GetSaveParams();
        void LoadParams(IEnumerable<string> values);
    }
}
