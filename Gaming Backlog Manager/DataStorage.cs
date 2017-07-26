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
        private string textRead = "";
        private List<Game> _games { get; set; }

        public DataStorage()
        {
            
        }

        public async void SerializeGameAsync()
        {
            string json = JsonConvert.SerializeObject(_games);
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync("backlog.json", CreationCollisionOption.ReplaceExisting);
            var stream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            using (var textStream = stream.GetOutputStreamAt(0))
            {

                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(json);
                    await textWriter.StoreAsync();
                    await textStream.FlushAsync();
                }
            }
        }

        public async Task<List<Game>> DeserializeGameAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            if (await localFolder.TryGetItemAsync("backlog.json") != null)
            {
                StorageFile textFile = await localFolder.GetFileAsync("backlog.json");
                string json = await FileIO.ReadTextAsync(textFile);
                var stream = await textFile.OpenAsync(FileAccessMode.Read);
                ulong size = stream.Size;
                using (var textStream = stream.GetInputStreamAt(0))
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint numBytesLoaded = await textReader.LoadAsync((uint)size);
                        textRead = textReader.ReadString(numBytesLoaded);
                        _games = JsonConvert.DeserializeObject<List<Game>>(textRead);
                    }
                }
            }
            return _games;
        }

        public string getTextRead()
        {
            return textRead;
        }
        
        public List<Game> Games
        {
            get
            {
                return this._games;
            }
            set
            {
                this._games = value;
            }
        }
    }
}
