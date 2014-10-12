namespace LectioTranscoder.Interfaces
{
    public interface IBitmap
    {
        System.Drawing.Bitmap ToBitmap(byte[] arrBytes);

        System.Drawing.Bitmap ReducedBitmap(System.Drawing.Bitmap original, int reducedWidth, int reducedHeight);
    }
}
