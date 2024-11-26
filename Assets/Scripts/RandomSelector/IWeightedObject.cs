namespace slaughter.de.RandomSelector
{
    public interface IWeightedObject
    {
        public float BaseWeight { get; }
        public float Weight { get; set; }
    }
}