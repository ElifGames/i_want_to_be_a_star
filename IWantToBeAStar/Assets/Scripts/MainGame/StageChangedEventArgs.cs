using System;

namespace IWantToBeAStar.MainGame
{
    public class StageChangedEventArgs : EventArgs
    {
        public Stage ChangedStage { get; set; }

        public StageChangedEventArgs(Stage stage)
        {
            ChangedStage = stage;
        }
    }
}