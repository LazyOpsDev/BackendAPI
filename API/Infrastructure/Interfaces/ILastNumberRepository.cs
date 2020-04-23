namespace Infrastructure.Interfaces
{
    public interface ILastNumberRepository
    {
        void WriteLatest(int i);
        int ReadLatest();
    }
}
