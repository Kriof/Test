using Interview.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Scripts.Services
{
    public class ApplicationHelper
    {
        public async Task<Application> GetApplication(int id)
        {
            
            var interviewFile = await ReadFromFileAsync(GetInterViewFilePath(), 4096);
            var applications = JsonConvert.DeserializeObject<List<Application>>(interviewFile);
            return applications.Find(x => x.ApplicationId == id);
        }
        public async Task<Boolean> PostApplication(Application app)
        {
            var interviewFile = await ReadFromFileAsync(GetInterViewFilePath(), 4096);
            var applications = JsonConvert.DeserializeObject<List<Application>>(interviewFile);
            applications.Add(app);
            var interviewFileAppended = JsonConvert.SerializeObject(applications);

            return await WriteToFileAsync(interviewFile, interviewFileAppended);
        }

        public async Task<Boolean> PutApplication(Application app)
        {
            var interviewFile = await ReadFromFileAsync(GetInterViewFilePath(), 4096);
            var applications = JsonConvert.DeserializeObject<List<Application>>(interviewFile);
            var index = applications.FindLastIndex(x => x.Id == app.Id);
            if(index != -1)
            {
                applications[index].Amount = app.Amount;
                applications[index].IsCleared = app.IsCleared;
                applications[index].Summary = app.Summary;
                applications[index].PostingDate = app.PostingDate;
                applications[index].Type = app.Type;
            }
            var interviewFileAppended = JsonConvert.SerializeObject(applications);

            return await WriteToFileAsync(interviewFile, interviewFileAppended);
        }
        public async Task<Boolean> RemoveApplication(Guid id)
        {
            var interviewFile = await ReadFromFileAsync(GetInterViewFilePath(), 4096);
            var applications = JsonConvert.DeserializeObject<List<Application>>(interviewFile);
            applications.Remove(applications.Find(x=> x.Id == id));
            
            var interviewFileAppended = JsonConvert.SerializeObject(applications);

            return await WriteToFileAsync(interviewFile, interviewFileAppended);
        }
        public string GetInterViewFilePath()
        {
            return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\data.json"));
        }

        static async Task<string> ReadFromFileAsync(string filePath, int bufferSize)
        {

            if (bufferSize < 1024)
                throw new ArgumentNullException("bufferSize");

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            StringBuilder readBuffer = null;
            byte[] buffer = new byte[bufferSize];
            FileStream fileStream = null;

            try
            {
                readBuffer = new StringBuilder();
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read,
                FileShare.Read, bufferSize: bufferSize, useAsync: true);
                Int32 bytesRead = 0;

                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    readBuffer.Append(Encoding.Unicode.GetString(buffer, 0, bytesRead));
                }
            }
            catch (Exception ex)
            {
                readBuffer = null;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Dispose();
            }
            return readBuffer.ToString();
        }

        static async Task<Boolean> WriteToFileAsync(string filePath, string text)
        {
            Boolean isSuccess = true;
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("text");

            byte[] buffer = Encoding.Unicode.GetBytes(text);
            Int32 offset = 0;
            Int32 sizeOfBuffer = 4096;
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write,
                FileShare.None, bufferSize: sizeOfBuffer, useAsync: true);
                await fileStream.WriteAsync(buffer, offset, buffer.Length);
            }
            catch
            {
                isSuccess = false;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Dispose();
            }
            return isSuccess;
        }
    }
}
