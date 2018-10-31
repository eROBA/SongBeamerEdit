using SongBeamerEdit.Model;
using System.ComponentModel;

namespace SongBeamerEdit.ViewModel
{
    class SongViewModel: ViewModelBase
    {
        #region "Private Felder"
        private Song _song;
        private string _songFileText = string.Empty;
        #endregion

        #region "Konstruktor"
        public SongViewModel(Song song)
        {
            if (song != null)
            {
                _song = song;
            }
            else
            {
                _song = new Song();
            }
        }
        #endregion

        #region "Öffentliche Eigenschaften"
        //public string FileText
        //{
        //    get { return _songFileText; }
        //    set
        //    {
        //        _songFileText = value;
        //    }
        //}
        #endregion

    }
}
