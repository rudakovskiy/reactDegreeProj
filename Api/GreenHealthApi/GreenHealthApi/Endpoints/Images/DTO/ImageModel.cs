using System;
using System.IO;
using Newtonsoft.Json;

namespace Fixe.Api.Customer.Endpoints.Images.DTO
{
    public class ImageModel
    {
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("data")]
        public byte[] Data { get; }
        [JsonProperty("length")]
        public long Length { get; }

        public ImageModel(FileStream file)
        {
            Name = file.Name;
            Data = ReadFile(file);
            Length = file.Length;
        }
        
        [JsonConstructor]
        public ImageModel(string name, byte[] data, long length)
        {
            Name = name;
            Data = data;
            Length = length;
        }
        
        private byte[] ReadFile(FileStream fs)
        {
            int length = Convert.ToInt32(fs.Length);
            byte[] data = new byte[length];
            fs.Read(data, 0, length);
            fs.Close();   
            return data;
        }

        public ImageModel()
        {
            
        }
    }
}