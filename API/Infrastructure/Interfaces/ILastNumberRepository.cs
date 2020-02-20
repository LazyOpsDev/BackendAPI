using System;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ILastNumberRepository
    {
        Task WriteLatest(int i);
        Task<int> ReadLatest();
    }
}
