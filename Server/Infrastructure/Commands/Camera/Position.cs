namespace Infrastructure.Commands.Camera
{
    public class Position : ICameraCommand
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Theta { get; set; }
        public bool Identified { get; set; }
    }
}
