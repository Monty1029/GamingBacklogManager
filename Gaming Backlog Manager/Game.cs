using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaming_Backlog_Manager
{
    class Game
    {
        private string gameTitle;
        private string system;
        private string region;
        private string ownership;
        private string distribution;

        private string status;
        private int achievements1;
        private int achievements2;
        private string notes;
        private bool nowPlaying;

        public Game()
        {
            
        }

        public string GetGameTitle()
        {
            return gameTitle;
        }
        public void SetGameTitle(string gameTitle)
        {
            this.gameTitle = gameTitle;
        }

        public string GetSystem()
        {
            return system;
        }
        public void SetSystem(string system)
        {
            this.system = system;
        }

        public string GetRegion()
        {
            return region;
        }
        public void SetRegion(string region)
        {
            this.region = region;
        }

        public string GetOwnership()
        {
            return ownership;
        }
        public void SetOwnership(string ownership)
        {
            this.ownership = ownership;
        }

        public string GetDistribution()
        {
            return distribution;
        }
        public void SetDistribution(string distribution)
        {
            this.distribution = distribution;
        }

        public string GetStatus()
        {
            return status;
        }
        public void SetStatus(string status)
        {
            this.status = status;
        }

        public int GetAchievements1()
        {
            return achievements1;
        }
        public void SetAchievements1(int achievements1)
        {
            this.achievements1 = achievements1;
        }

        public int GetAchievements2()
        {
            return achievements2;
        }
        public void SetAchievements2(int achievements2)
        {
            this.achievements2 = achievements2;
        }

        public string GetNotes()
        {
            return notes;
        }
        public void SetNotes(string notes)
        {
            this.notes = notes;
        }
    }
}
