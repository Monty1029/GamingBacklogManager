﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Game> _games { get; set; }
        private ObservableCollection<WishlistGame> _wishlistGames { get; set; }

        public DataStorage()
        {
            _games = new ObservableCollection<Game>();
            _wishlistGames = new ObservableCollection<WishlistGame>();
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

        public async Task<ObservableCollection<Game>> DeserializeGameAsync()
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
                        _games = JsonConvert.DeserializeObject<ObservableCollection<Game>>(textRead);
                    }
                }
            }
            return _games;
        }

        public async void SerializeWishlistGameAsync()
        {
            string json = JsonConvert.SerializeObject(_wishlistGames);
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync("wishlist.json", CreationCollisionOption.ReplaceExisting);
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

        public async Task<ObservableCollection<WishlistGame>> DeserializeWishlistGameAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            if (await localFolder.TryGetItemAsync("wishlist.json") != null)
            {
                StorageFile textFile = await localFolder.GetFileAsync("wishlist.json");
                string json = await FileIO.ReadTextAsync(textFile);
                var stream = await textFile.OpenAsync(FileAccessMode.Read);
                ulong size = stream.Size;
                using (var textStream = stream.GetInputStreamAt(0))
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint numBytesLoaded = await textReader.LoadAsync((uint)size);
                        textRead = textReader.ReadString(numBytesLoaded);
                        _wishlistGames = JsonConvert.DeserializeObject<ObservableCollection<WishlistGame>>(textRead);
                    }
                }
            }
            return _wishlistGames;
        }

        public string getTextRead()
        {
            return textRead;
        }
        
        public ObservableCollection<Game> Games
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

        public ObservableCollection<WishlistGame> WishlistGames
        {
            get
            {
                return this._wishlistGames;
            }
            set
            {
                this._wishlistGames = value;
            }
        }
    }
}
