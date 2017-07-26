using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Gaming_Backlog_Manager
{
    class DataStorage
    {
        private Game _gameo { get; set; }
        private string textRead;

        public DataStorage()
        {

        }

        public async void SerializeGameAsync()
        {
            string json = JsonConvert.SerializeObject(_gameo);
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync("backlogdata.json", CreationCollisionOption.ReplaceExisting);
            var stream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var textStream = stream.GetOutputStreamAt(0))
            {

                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(json);
                    await textWriter.StoreAsync();
                }
            }
        }

        public async void DeserializeGameAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.GetFileAsync("backlogdata.json");
            string json = await FileIO.ReadTextAsync(textFile);
            var stream = await textFile.OpenAsync(FileAccessMode.Read);
            ulong size = stream.Size;
            using (var textStream = stream.GetInputStreamAt(0))
            {
                using (DataReader textReader = new DataReader(textStream))
                {
                    uint numBytesLoaded = await textReader.LoadAsync((uint)size);
                    textRead = textReader.ReadString(numBytesLoaded);
                }
            }
        }

        public string getTextRead()
        {
            return textRead;
        }

        public Game GameO
        {
            get
            {
                return this._gameo;
            }
            set
            {
                this._gameo = value;
            }
        }
    }
}
