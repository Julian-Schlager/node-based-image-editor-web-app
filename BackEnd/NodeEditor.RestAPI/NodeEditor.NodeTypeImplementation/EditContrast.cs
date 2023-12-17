using NodeEditor.Entities;
using SixLabors.ImageSharp.Formats;

namespace NodeEditor.NodeTypeImplementation
{
    public class EditContrast : IEditImage
    {
        private const string CONTRAST = "Contrast";

        public Stream Edit(IEnumerable<Stream> imageStream, IEnumerable<DataInputValue> dataInputValues, IEnumerable<DataInput> dataInputs, string fileName)
        {
            DataInput dataInput = dataInputs.FirstOrDefault(x=>x.Name == CONTRAST);

            DataInputValue contrastValue = dataInputValues.FirstOrDefault(x => x.DataInputId == dataInput.Id);
            float contrastModifier = float.Parse(contrastValue.Value);

            using Image image = Image.Load(imageStream.ElementAt(0));
            image.Mutate(x => x.Contrast(contrastModifier/100));

            string tempFileName = $"{Path.GetTempFileName()}{Path.GetExtension(fileName)}";
            image.Save(tempFileName);

            MemoryStream resultStream = new MemoryStream();

            using (FileStream resultFileStream = File.OpenRead(tempFileName))
            {
                resultFileStream.CopyTo(resultStream);
                resultStream.Position = 0;
            }

            File.Delete(tempFileName);

            return resultStream; 
        }
    }
}