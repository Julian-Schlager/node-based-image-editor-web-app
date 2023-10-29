using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.Entities;
using NodeEditor.NodeTypeImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Implementation
{
    public class ImageService : IImageService
    {
        private IDataInputRepository dataInputRepository;

        public ImageService(IDataInputRepository dataInputRepository)
        {
            this.dataInputRepository = dataInputRepository;
        }

        public async Task<IEnumerable<Stream>> EditImage(Stream image, Node firstNode, string fileName)
        {
            IEnumerable<DataInput> dataInputs = await dataInputRepository.GetAll();
            return EditImageRecursivly(image, firstNode,new List<Stream>(),dataInputs,fileName);
        }

        private IEditImage GetClassForNodeType(string name)
        {
            string assamblyName = typeof(IEditImage).Assembly.FullName;
            string nameSpaceName = typeof(IEditImage).Namespace;
            Type editImageType = Type.GetType($"{nameSpaceName}.{name}, {assamblyName}");
            IEditImage instance = (IEditImage)Activator.CreateInstance(editImageType);
            return instance;
        }

        private IEnumerable<Stream> EditImageRecursivly(Stream image, Node currentNode,List<Stream> images,IEnumerable<DataInput> dataInputs, string fileName)
        {
            if(currentNode.NodeType.ModificationType != ModificationType.Download &&
               currentNode.NodeType.ModificationType != ModificationType.Upload && 
               currentNode.NodeType.ModificationType != ModificationType.Logic)
            {
                IEditImage editNodeType = GetClassForNodeType(currentNode.NodeType.Name);
                Stream[] streams = new Stream[] { image };
                Stream editedImage = editNodeType.Edit(streams, currentNode.DataInputValues,dataInputs,fileName);
                foreach (Node nextNode in currentNode.NextNodes)
                {
                    EditImageRecursivly(editedImage, nextNode,images,dataInputs,fileName);
                }
            }
            else if(currentNode.NodeType.ModificationType == ModificationType.Upload)
            {
                foreach (Node nextNode in currentNode.NextNodes)
                {
                    EditImageRecursivly(image, nextNode,images,dataInputs,fileName);
                }
            }
            else if(currentNode.NodeType.ModificationType == ModificationType.Download)
            {
                
               images.Add(image);
            }
            return images;

        }
    }
}
