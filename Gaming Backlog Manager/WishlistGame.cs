using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Backlog_Manager
{
    class WishlistGame
    {
        private string _gameTitle { get; set; }
        private string _system { get; set; }
        private string _releaseDate { get; set; }

        private int _id { get; set; }

        public WishlistGame()
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

        public string ReleaseDate
        {
            get
            {
                return this._releaseDate;
            }
            set
            {
                this._releaseDate = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
    }
}
