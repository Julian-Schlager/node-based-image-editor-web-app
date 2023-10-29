using NodeEditor.Entities;
using SixLabors.ImageSharp.Formats;

namespace NodeEditor.NodeTypeImplementation
{
    public class EditBrightness : IEditImage
    {
        private const string BRIGHTNESS = "Brightness";

        public Stream Edit(IEnumerable<Stream> imageStream, IEnumerable<DataInputValue> dataInputValues, IEnumerable<DataInput> dataInputs, string fileName)
        {
            DataInput dataInput = dataInputs.FirstOrDefault(x=>x.Name == BRIGHTNESS);

            DataInputValue brightnessValue = dataInputValues.FirstOrDefault(x => x.DataInputId == dataInput.Id);
            float brightnessModifier = float.Parse(brightnessValue.Value);

            using Image image = Image.Load(imageStream.ElementAt(0));
            image.Mutate(x => x.Brightness(brightnessModifier));

            string tempFileName = $"{Path.GetTempFileName()}{Path.GetExtension(fileName)}";
            image.Save(tempFileName);

            MemoryStream resultStream = new MemoryStream();

            using (FileStream resultFileStream = File.OpenRead(tempFileName))
            {
                resultFileStream.CopyTo(resultStream);
                resultStream.Position = 0;
            }

            File.Delete(tempFileName);

            return resultStream; //ToDo Actully Edit Image
        }
    }
}