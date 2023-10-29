using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.NodeTypeImplementation
{
    public interface IEditImage
    {
        Stream Edit(IEnumerable<Stream> image, IEnumerable<DataInputValue> dataInputValues,IEnumerable<DataInput> dataInputs, string filename); 
    }
}
