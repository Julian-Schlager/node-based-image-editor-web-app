using NodeEditor.Entities;

namespace NodeEditor.NodeTypeImplementation
{
    public class EditBrightness : IEditImage
    {
        public Stream Edit(IEnumerable<Stream> image, IEnumerable<DataInputValue> dataInputValues)
        {
            return image.ElementAt(0); //ToDo Actully Edit Image
        }
    }
}