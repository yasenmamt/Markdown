﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace produproperty
{
    class viewModel : ViewModel.notify_property
    {
        public viewModel()
        {
            _m = new model(this);
        }

        public string text
        {
            set
            {
                _m._text = value;
                OnPropertyChanged();
            }
            get
            {
                return _m._text;
            }
        }

        public string name
        {
            set
            {
                _m._name = value;
                OnPropertyChanged();
            }
            get
            {
                return _m._name;
            }
        }

        public bool writetext
        {
            set
            {
                _m._writetext = value;
                OnPropertyChanged();
            }
            get
            {
                return _m._writetext;
            }
        }

        public Action<int, int> selectchange;

        public int select;

        public string addressfolder
        {
            set
            {
                OnPropertyChanged();
            }
            get
            {
                return _m.folder.Path;
            }
        }

        public async void clipboard(TextControlPasteEventArgs e)
        {
            if (writetext)
            {
                return;
            }

            e.Handled = true;
            DataPackageView con = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();
            string str = await _m.clipboard(con);
            tianjia(str);
        }

        public void storage()
        {
            if (writetext)
            {
                return;
            }
            _m.storage();
        }

        public async void dropimg(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            if (writetext)
            {
                return;
            }
            var defer = e.GetDeferral();
            try
            {
                DataPackageView dataView = e.DataView;
                string str = await _m.clipboard(dataView);
                tianjia(str);
            }
            finally
            {
                defer.Complete();
            }
        }

        public async void accessfolder()
        {
            FolderPicker pick = new FolderPicker();
            pick.FileTypeFilter.Add("*");
            StorageFolder folder = await pick.PickSingleFolderAsync();
            if (folder != null)
            {
                _m.accessfolder(folder);
            }
        }

        public void file_open()
        {

        }

        private model _m;

        private void tianjia(string str)
        {
            int n;
            n = select;
            int i;
            for (i = 0; n > 0 && i < text.Length; i++)
            {
                if (text[i] != '\r' && text[i] != '\n')
                {
                    n--;
                }
            }
            text = text.Insert(i, str);
            selectchange(select + str.Length + 1, 0);

            //string t = text.Replace("\r\n", "\n");
            //t = t.Insert(select, str);
            //text = t.Replace("\n", "\r\n");
        }

    }
}
