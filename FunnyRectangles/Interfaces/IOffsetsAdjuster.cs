namespace FunnyRectangles.Interfaces
{
    interface IOffsetsAdjuster
    {
        void AdjustOffsets(IGraphicObject grahicObject, int dx, int dy, out int resDx, out int resDy);
    }
}
