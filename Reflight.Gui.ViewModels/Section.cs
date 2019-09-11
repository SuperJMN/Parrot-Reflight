namespace Reflight.Gui.ViewModels
{
    public class Section
    {
        public Section(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public object Icon { get; set; }
    }
}