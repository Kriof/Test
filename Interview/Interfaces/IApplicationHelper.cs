using Interview.Models;
using System;
using System.Threading.Tasks;

namespace Interview.Areas.Interfaces
{
    public interface IApplicationHelper
    {
        Task<Application> GetApplication(int id);
        Task<Boolean> PostApplication(Application app);
        Task<Boolean> PutApplication(Application app);
        Task<Boolean> RemoveApplication(int id);
        string GetInterViewFilePath();
        Task<string> ReadFromFileAsync(string filePath, int bufferSize);
        Task<Boolean> WriteToFileAsync(string filePath, string text);
    }
}
