﻿// lindexi
// 20:47

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using produproperty.ViewModel;

namespace produproperty
{
//    internal class model
//    {
//        public model(viewModel view)
//        {
//            this.view = view;
//            ran = new Random();
//            ce();

//            App.Current.Suspending += Current_Suspending;
//        }


//        public StorageFile file
//        {
//            set
//            {
//                _file = value;
//            }
//            get
//            {
//                return _file;
//            }
//        }

//        public StorageFolder folder
//        {
//            set
//            {
//                _folder = value;
//            }
//            get
//            {
//                return _folder;
//            }
//        }

//        public async Task<string> clipboard(DataPackageView con)
//        {
//            string str = string.Empty;
//            //文本
//            if (con.Contains(StandardDataFormats.Text))
//            {
//                str = await con.GetTextAsync();
//                return str;
//            }

//            //图片
//            if (con.Contains(StandardDataFormats.Bitmap))
//            {
//                RandomAccessStreamReference img = await con.GetBitmapAsync();
//                var imgstream = await img.OpenReadAsync();
//                BitmapImage bitmap = new BitmapImage();
//                bitmap.SetSource(imgstream);

//                WriteableBitmap src = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
//                src.SetSource(imgstream);

//                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imgstream);
//                PixelDataProvider pxprd =
//                    await
//                        decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight,
//                            new BitmapTransform(), ExifOrientationMode.RespectExifOrientation,
//                            ColorManagementMode.DoNotColorManage);
//                byte[] buffer = pxprd.DetachPixelData();

//                str = "image";
//                StorageFolder folder = await _folder.GetFolderAsync(str);

//                StorageFile file =
//                    await
//                        folder.CreateFileAsync(
//                            DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() +
//                            DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +
//                            (ran.Next()%10000).ToString() + ".png", CreationCollisionOption.GenerateUniqueName);

//                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
//                {
//                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);
//                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, decoder.PixelWidth,
//                        decoder.PixelHeight, decoder.DpiX, decoder.DpiY, buffer);
//                    await encoder.FlushAsync();

//                    str = $"![这里写图片描述](image/{file.Name})\n";
//                }
//            }

//            //文件
//            if (con.Contains(StandardDataFormats.StorageItems))
//            {
//                var filelist = await con.GetStorageItemsAsync();
//                StorageFile file = filelist.OfType<StorageFile>().First();
//                return await imgfolder(file);
//            }

//            return str;
//        }

//        public async void Current_Suspending(object sender, SuspendingEventArgs e)
//        {
//            //throw new NotImplementedException();
//            if (!string.IsNullOrEmpty(_text))
//            {
//                bool temp = _open;
//                _open = true;
//                await storage();
//                _open = temp;
//            }
//        }

//        public async void open_file(StorageFile file)
//        {
//            _open = true;
//            await storage();
//            this.file = file;

//            using (IRandomAccessStream readStream = await file.OpenAsync(FileAccessMode.Read))
//            {
//                using (DataReader dataReader = new DataReader(readStream))
//                {
//                    UInt64 size = readStream.Size;
//                    if (size <= UInt32.MaxValue)
//                    {
//                        UInt32 numBytesLoaded = await dataReader.LoadAsync((UInt32) size);
//                        text = dataReader.ReadString(numBytesLoaded);
//                        int i = text.IndexOf('\n');
//                        if (i > 0)
//                        {
//                            name = text.Substring(0, i);
//                            text = text.Substring(i + 2);
//                        }
//                        else
//                        {
//                            name = file.DisplayName;
//                        }
//                        i = text.LastIndexOf("http://blog.csdn.net/lindexi_gd");
//                        if (i > 0)
//                        {
//                            if (i - 2 > 0)
//                            {
//                                i = i - 2;
//                            }
//                            text = text.Substring(0, i);
//                        }
//                    }
//                }
//            }
//            //name = file.DisplayName;
//            reminder = "打开" + file.Path;
//        }

//        public async Task<string> imgfolder(StorageFile file)
//        {
//            string str = "image";
//            StorageFolder image = null;
//            try
//            {
//                image = await _folder.GetFolderAsync(str);
//            }
//            catch
//            {
//            }
//            if (image == null)
//            {
//                image = await _folder.CreateFolderAsync(str, CreationCollisionOption.OpenIfExists);
//            }
//            file = await file.CopyAsync(image, file.Name, NameCollisionOption.GenerateUniqueName);

//            if ((file.FileType == ".png") || (file.FileType == ".jpg"))
//            {
//                str = $"![这里写图片描述](image/{file.Name})\r\n\r\n";
//                return str;
//            }
//            else
//            {
//                str = $"[{file.Name}](image/{file.Name})\r\n\r\n";
//                return str;
//            }
//        }

//        public async void accessfolder(StorageFolder folder)
//        {
//            if (string.Equals(this.folder.Path, folder.Path))
//            {
//                return;
//            }

//            writetext = false;

//            StorageApplicationPermissions.FutureAccessList.Clear();
//            StorageApplicationPermissions.FutureAccessList.Add(folder);

//            //image 文件夹
//            string str = "image";
//            StorageFolder image = null;
//            try
//            {
//                image = await folder.GetFolderAsync(str);
//            }
//            catch
//            {
//            }
//            if (image == null)
//            {
//                image = await folder.CreateFolderAsync(str, CreationCollisionOption.OpenIfExists);
//            }
//            //if (!this.folder.Path.Equals(folder.Path))
//            //{
//            await storage();
//            //}
//            this.folder = folder;
//        }

//        public async Task storage()
//        {
//            if (string.IsNullOrEmpty(text))
//            {
//                return;
//            }

//            //string str = name + "\n" + text.Replace("\r\n", "\n");
//            string[] temp = text.Split(new char[]
//            {
//                '\r', '\n'
//            });
//            StringBuilder str = new StringBuilder();
//            str.Append(name + "\n\n");
//            foreach (var t in temp)
//            {
//                if (!string.IsNullOrEmpty(t))
//                {
//                    str.Append(t + "\n\n");
//                }
//            }

//            if (!_open)
//            {
//                str.Append("http://blog.csdn.net/lindexi_gd");
//            }

//            using (StorageStreamTransaction transaction = await file.OpenTransactedWriteAsync())
//            {
//                using (DataWriter dataWriter = new DataWriter(transaction.Stream))
//                {
//                    dataWriter.WriteString(str.ToString());
//                    transaction.Stream.Size = await dataWriter.StoreAsync();
//                    await transaction.CommitAsync();
//                }
//            }
//            if (string.IsNullOrWhiteSpace(name))
//            {
//                name = "请输入标题";
//                return;
//            }
//            if (!_open)
//            {
//                file = await file.CopyAsync(folder, name + ".md", NameCollisionOption.GenerateUniqueName);
//                try
//                {
//                    StorageFolder folder_storage = await ApplicationData.Current.LocalFolder.GetFolderAsync("text");
//                    Directory.Delete(folder_storage.Path, true);
//                }
//                catch
//                {
//                }
//                _open = true;
//            }

//            reminder =
//                reminder =
//                    "保存文件" + file.Path + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
//        }

//        private StorageFile _file;
//        private StorageFolder _folder;
//        public string _name;
//        private bool _open;


//        public string _text;
//        public bool _writetext;
//        private viewModel view;

//        private string text
//        {
//            set
//            {
//                if (view == null)
//                {
//                    _text = value;
//                }
//                else
//                {
//                    view.Text = value;
//                }
//            }
//            get
//            {
//                if (view == null)
//                {
//                    return _text;
//                }
//                else
//                {
//                    return view.Text;
//                }
//            }
//        }

//        private bool writetext
//        {
//            set
//            {
//                _writetext = value;
//            }
//            get
//            {
//                return _writetext;
//            }
//        }

//        private string name
//        {
//            set
//            {
//                if (view == null)
//                {
//                    _name = value;
//                }
//                else
//                {
//                    view.Name = value;
//                }
//            }
//            get
//            {
//                if (view == null)
//                {
//                    return _name;
//                }
//                else
//                {
//                    return view.Name;
//                }
//            }
//        }

//        private string reminder
//        {
//            set
//            {
//                view.Reminder = value;
//            }
//            get
//            {
//                return view.Reminder;
//            }
//        }

//        private Random ran
//        {
//            set;
//            get;
//        }

//        private async void ce() //2016年1月10日11:04:29
//        {
//            _open = false;
//            string str;
//            StorageFolder temp = null;
//            try
//            {
//                str = "text";
//                temp =
//                    await
//                        ApplicationData.Current.LocalFolder.CreateFolderAsync(str, CreationCollisionOption.OpenIfExists);
//            }
//            catch
//            {
//            }
//            //默认位置
//            try
//            {
//                folder =
//                    await
//                        StorageApplicationPermissions.FutureAccessList.GetFolderAsync(
//                            StorageApplicationPermissions.FutureAccessList.Entries[0].Token);
//                writetext = false;
//            }
//            catch
//            {
//                //str = "text";
//                folder = temp;
//                    //=await ApplicationData.Current.LocalFolder.CreateFolderAsync(str,CreationCollisionOption.OpenIfExists);
//                //没有默认位置

//                writetext = true;
//                _text = "请选择默认保存位置";
//            }

//            //image 文件夹
//            str = "image";
//            StorageFolder image = null;
//            try
//            {
//                image = await folder.GetFolderAsync(str);
//            }
//            catch
//            {
//            }
//            if (image == null)
//            {
//                image = await folder.CreateFolderAsync(str, CreationCollisionOption.OpenIfExists);
//            }

//            //没有上次保存
//            str = "请输入标题";
//            bool open = false;
//            try
//            {
//                file = (await temp.GetFilesAsync()).First<StorageFile>();
//                open = true;
//            }
//            catch
//            {
//                //新建
//                file = await temp.CreateFileAsync(str, CreationCollisionOption.GenerateUniqueName);
//                open = false;
//            }

//            if (open)
//            {
//                using (IRandomAccessStream readStream = await _file.OpenAsync(FileAccessMode.Read))
//                {
//                    using (DataReader dataReader = new DataReader(readStream))
//                    {
//                        UInt64 size = readStream.Size;
//                        if (size <= UInt32.MaxValue)
//                        {
//                            UInt32 numBytesLoaded = await dataReader.LoadAsync((UInt32) size);
//                            text = dataReader.ReadString(numBytesLoaded);
//                            int i = text.IndexOf('\n');
//                            if (i > 0)
//                            {
//                                text = text.Substring(i + 1);
//                            }
//                        }
//                        name = file.DisplayName;
//                    }
//                }
//            }
//            else
//            {
//                text = @"
//拖入图片自动生成![](图片)，粘贴自动生成![](图片)
//按ctrl+k快速输入代码";
//            }
//            //_name = file.DisplayName;
//        }
//    }
}