using NodeEditor.Entities;
using SixLabors.ImageSharp.Formats;

namespace NodeEditor.NodeTypeImplementation
{
    public class EditSaturation : IEditImage
    {
        private const string SATURATION = "Saturation";

        public Stream Edit(IEnumerable<Stream> imageStream, IEnumerable<DataInputValue> dataInputValues, IEnumerable<DataInput> dataInputs, string fileName)
        {
            DataInput dataInput = dataInputs.FirstOrDefault(x=>x.Name == SATURATION);

            DataInputValue saturationValue = dataInputValues.FirstOrDefault(x => x.DataInputId == dataInput.Id);
            float saturationModifier = float.Parse(saturationValue.Value);

            using Image image = Image.Load(imageStream.ElementAt(0));
            image.Mutate(x => x.Saturate(saturationModifier/100));

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