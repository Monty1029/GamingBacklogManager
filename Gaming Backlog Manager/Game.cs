using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Backlog_Manager
{
    class Game
    {
        private string _gameTitle { get; set; }
        private string _system { get; set; }
        private string _region { get; set; }
        private string _ownership { get; set; }
        private string _distribution { get; set; }

        private string _status { get; set; }
        private int _achievements1 { get; set; }
        private int _achievements2 { get; set; }
        private string _notes { get; set; }
        private string _nowPlaying { get; set; }

        public Game()
        {
            
        }

        public string GameTitle
        {
            get
            {
                return this._gameTitle;
            }
            set
            {
                this._gameTitle = value;
            }
        }

        public string System
        {
            get
            {
                return this._system;
            }
            set
            {
                this._system = value;
            }
        }

        public string Region
        {
            get
            {
                return this._region;
            }
            set
            {
                this._region = value;
            }
        }

        public string Ownership
        {
            get
            {
                return this._ownership;
            }
            set
            {
                this._ownership = value;
            }
        }

        public string Distribution
        {
            get
            {
                return this._distribution;
            }
            set
            {
                this._distribution = value;
            }
        }

        public string Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int Achievements1
        {
            get
            {
                return this._achievements1;
            }
            set
            {
                this._achievements1 = value;
            }
        }

        public int Achievements2
        {
            get
            {
                return this._achievements2;
            }
            set
            {
                this._achievements2 = value;
            }
        }

        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        public string NowPlaying
        {
            get
            {
                return this._nowPlaying;
            }
            set
            {
                this._nowPlaying = value;
            }
        }
    }
}
