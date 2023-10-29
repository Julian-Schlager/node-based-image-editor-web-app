using System.ComponentModel;

namespace NodeEditor.Entities
{
    public class NodeType:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ModificationType ModificationType { get; set; }
        public ICollection<DataInput>? DataInputs { get; set; }
        public ICollection<Node>? Nodes { get; set; }

    }

    public enum ModificationType // ToDo: Change because partly Redundant
    {
        Brightness=0,
        Saturation=1,
        Contrast=2,
        Logic=3,
        Rotate=4,
        Resize=5,
        Crop=6,
        text=7,
        Upload=8,
        Download=9
    }
}