using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ILastNumberRepository
    {
        void WriteLatest(int i);
        int ReadLatest();
    }
}
