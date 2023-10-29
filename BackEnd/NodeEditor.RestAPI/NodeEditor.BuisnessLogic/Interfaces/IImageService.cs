using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Interfaces
{
    public interface IImageService
    {
        Task<IEnumerable<Stream>> EditImage(Stream image,Node firstNode,string fileName);
    }
}
