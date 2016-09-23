using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace File_3_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        long SplitFileSize;
        Int64 Size;
        string Extension;
        string FilePath;
        string SplitFilePath;
        int SplitFlag = 0;
        string MergeFileFolder;
        int NumberOfSplitFiles = 0;
        string[] MergeSplitFiles = new string[20];
        static string MergeFileName;
        int BytesRemaining;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
                Nullable<bool> result = FileDialog.ShowDialog();
                if (result == true)
                {
                    //Getting The File Path:
                    FilePath = FileDialog.FileName;
                    TextBoxFilePath.Text = FilePath;
                    //Getting The File Size:
                    FileInfo fi = new FileInfo(FilePath);
                    Size = fi.Length;
                    LabelFileSize.Content = Size;
                    //Getting The File Extension:
                    Extension = fi.Extension;
                    LabelFileExtension.Content = Extension;
                    //Getting The File Name:
                    string FileNameExtension = System.IO.Path.GetFileName(FilePath);
                    FileNameExtension.ToCharArray();
                    char[] FileNameTemp = new char[FileNameExtension.Length - 4];
                    for (int i = 0; i < FileNameExtension.Length; i++)
                    {
                        if (FileNameExtension[i] == '.')
                        {
                            break;
                        }
                        else
                        {
                            FileNameTemp[i] = FileNameExtension[i];
                        }
                    }
                    string FileName = new string(FileNameTemp);
                    LabelFileName.Content = FileName;
                }
                TextBlockFilePath.Visibility = Visibility.Collapsed;
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = Dialog.ShowDialog();
            SplitFilePath = Dialog.SelectedPath;
            TextBoxSplitFilePath.Text = SplitFilePath;
            TextBlockSplitFilePath.Visibility = Visibility.Collapsed;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cbxvalue = (ComboBoxSizeType.SelectedItem as ComboBoxItem).Content.ToString();
                if (cbxvalue == "Bytes")
                {
                    LabelFileSize.Content = Size.ToString("#.##");
                }
                else if (cbxvalue == "KiloBytes")
                {
                    double TempSize = Size;
                    TempSize = TempSize / 1024;
                    LabelFileSize.Content = TempSize.ToString("#.##");
                }
                else if (cbxvalue == "MegaBytes")
                {
                    double TempSize = Size;
                    TempSize = TempSize / 1048576;
                    LabelFileSize.Content = TempSize.ToString("#.##");
                }
                else if (cbxvalue == "GigaBytes")
                {
                    double TempSize = Size;
                    TempSize = TempSize / 1073741824;
                    LabelFileSize.Content = TempSize.ToString("#.##");
                }
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Format Error.\n\nException: " + ex.Message);
            }
        }

        private void TextBoxFilePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void RadioButtonFloppySplit_Checked(object sender, RoutedEventArgs e)
        {
            
        }

       

        private void ButtonSplit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBoxFilePath.Text != "" && TextBoxSplitFilePath.Text != "")
                {
                    if (RadioButtonFloppySplit.IsChecked == true)
                    {
                        //File Path:
                        FileInfo fi = new FileInfo(FilePath);
                        //Custom Size:
                        Int64 CustomSize = 1468006;
                        new Thread(delegate()
                        { 
                            System.Diagnostics.Stopwatch Watch = new System.Diagnostics.Stopwatch();
                            Watch.Start();
                            SplitFile(CustomSize);
                            Watch.Stop();
                            System.Windows.MessageBox.Show("Splitting Completed Successfully!\n\nTime Elapsed: " + Watch.Elapsed);
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                            {
                                TextBoxFilePath.IsEnabled = true;
                                TextBoxSplitFilePath.IsEnabled = true;
                                ButtonBrowse.IsEnabled = true;
                                ButtonSplitPath.IsEnabled = true;
                                TabMerge.IsEnabled = true;
                                ButtonSplit.IsEnabled = true;
                            }, null);
                        }).Start();
                        
                    }
                    else if (RadioButtonCDSplit.IsChecked == true)
                    {
                        //File Path:
                        FileInfo fi = new FileInfo(FilePath);
                        //Custom Size:
                        Int64 CustomSize = 681574400;
                        new Thread(delegate()
                        {
                            System.Diagnostics.Stopwatch Watch = new System.Diagnostics.Stopwatch();
                            Watch.Start();
                            SplitFile(CustomSize);
                            Watch.Stop();
                            System.Windows.MessageBox.Show("Splitting Completed Successfully!\n\nTime Elapsed: " + Watch.Elapsed);
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                            {
                                TextBoxFilePath.IsEnabled = true;
                                TextBoxSplitFilePath.IsEnabled = true;
                                ButtonBrowse.IsEnabled = true;
                                ButtonSplitPath.IsEnabled = true;
                                TabMerge.IsEnabled = true;
                                ButtonSplit.IsEnabled = true;
                            }, null);
                        }).Start();
                    }
                    else
                    {
                        //File Path:
                        FileInfo fi = new FileInfo(FilePath);
                        //Custom Size:
                        Int64 CustomSize = Convert.ToInt64(TextBoxCustomSplit.Text);
                        new Thread(delegate()
                        {
                            System.Diagnostics.Stopwatch Watch = new System.Diagnostics.Stopwatch();
                            Watch.Start();
                            SplitFile(CustomSize);
                            Watch.Stop();
                            System.Windows.MessageBox.Show("Splitting Completed Successfully!\n\nTime Elapsed: " + Watch.Elapsed);
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                            { 
                                TextBoxFilePath.IsEnabled = true;
                                TextBoxSplitFilePath.IsEnabled = true;
                                ButtonBrowse.IsEnabled = true;
                                ButtonSplitPath.IsEnabled = true;
                                TabMerge.IsEnabled = true;
                                ButtonSplit.IsEnabled = true;
                            }, null);
                        }).Start();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter The File Paths.");
                }
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("Please Enter a Valid File Path.\n\nException Message: " + ex.Message);
            }

            catch(FormatException ex)
            {
                MessageBox.Show("Please Enter the Custom Split Size.\n\nException Message: " + ex.Message);
            }
            catch(System.Security.SecurityException ex)
            {
                MessageBox.Show("Program is Not Authorized For Process.\n\nException: " + ex.Message);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("Program is Not Authorized For Process.\n\nException: " + ex.Message);
            }
            catch(System.IO.PathTooLongException ex)
            {
                MessageBox.Show("The Path is Too Long.\n\nException: " + ex.Message);
            }
            catch(System.NotSupportedException ex)
            {
                MessageBox.Show("File is Not Supported.\n\nException: " + ex.Message);
            }
            catch(System.Threading.ThreadStateException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch(System.OutOfMemoryException ex)
            {
                MessageBox.Show("The System is Out of Memory.\n\nException: " + ex.Message);
            }
        }

        private void SplitFile(Int64 CustomSize)
        {
            try
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                { 
                    TextBoxFilePath.IsEnabled = false;
                    TextBoxSplitFilePath.IsEnabled = false;
                    ButtonBrowse.IsEnabled = false;
                    ButtonSplitPath.IsEnabled = false;
                    ProgressBarSplit.Minimum = 1;
                    ProgressBarSplit.Maximum = Size;
                    ButtonSplit.IsEnabled = false;
                    TabMerge.IsEnabled = false;
                }, null);
                double value = 0;
                double Progress = 0;
                byte[] bytes = new byte[CustomSize];
                FileStream FsRead = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                //Creating Split Files:
                long Split = (Size / CustomSize) + 1;
                int number = 0;

                while (number < Split)
                {
                    if (number < (int)Split - 1)
                    {
                        using (File.Create(SplitFilePath + @"\split" + number + Extension)) { }
                        FsRead.Read(bytes, 0, (int)CustomSize);
                        FileStream fsWrite = new FileStream(SplitFilePath + @"\split" + number + Extension, FileMode.Open, FileAccess.Write);
                        fsWrite.Write(bytes, 0, (int)CustomSize);
                        fsWrite.Close();
                        value += CustomSize;
                        Progress = (value / Size) * 100;
                        {
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { ProgressBarSplit.SetValue(ProgressBar.ValueProperty, value); }, null);
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { TextBlockSplitProgress.Text = Convert.ToString(Progress.ToString("###.##")) + "%"; }, null);
                        }
                        FileInfo fi2 = new FileInfo(SplitFilePath + @"\split" + number + Extension);
                        SplitFileSize = fi2.Length;
                        number++;
                        BytesRemaining = ((int)Size - (((int)Split - 1) * (int)SplitFileSize));
                        Array.Clear(bytes, 0, (int)CustomSize);
                    }
                    else if (number < (int)Split)
                    {
                        using (File.Create(SplitFilePath + @"\split" + number + Extension)) { }
                        FsRead.Read(bytes, 0, BytesRemaining);

                        FileStream fsWrite = new FileStream(SplitFilePath + @"\split" + number + Extension, FileMode.Open, FileAccess.Write);
                        fsWrite.Write(bytes, 0, BytesRemaining);
                        fsWrite.Close();
                        value += BytesRemaining;
                        Progress = (value / Size) * 100;
                        {
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { ProgressBarSplit.SetValue(ProgressBar.ValueProperty, value); }, null);
                            Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { TextBlockSplitProgress.Text = Convert.ToString(Progress.ToString("###.##")) + "%"; }, null);
                        }
                        Array.Clear(bytes, 0, BytesRemaining);
                        FsRead.Close();
                        Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { ProgressBarSplit.SetValue(ProgressBar.ValueProperty, 100.0); }, null);
                        Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { TextBlockSplitProgress.Text = Convert.ToString(Progress.ToString("100")) + "%"; }, null);
                        SplitFlag++;
                        break;
                    }
                }
                
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File Not Found. Please Try Again.\n\nException: " + ex.Message);

            }
            catch(IndexOutOfRangeException ex)
            {
                MessageBox.Show("File Size is too Large.\n\nException: " + ex.Message);
            }
            catch(OverflowException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("The System is Out of Memory.\n\nException: " + ex.Message);
            }
            catch(System.ArgumentNullException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch(System.ComponentModel.InvalidEnumArgumentException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch(NotSupportedException ex)
            {
                MessageBox.Show("The File is Not Supported.\n\nException: " + ex.Message);
            }
            catch(System.Security.SecurityException ex)
            {
                MessageBox.Show("Security Exception.\n\nException: " + ex.Message);
            }
            catch(System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("The Directory was Not Found. Please Enter a Valid Directory.\n\nException: " + ex.Message);
            }
            catch(System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("The Program is Not Authorized for Process.\n\nException: " + ex.Message);
            }
            catch(System.IO.PathTooLongException ex)
            {
                MessageBox.Show("The File Path is Too Long.\n\nException: " + ex.Message);
            }
            catch(ObjectDisposedException ex)
            {
                MessageBox.Show("The Object is Disposed.\n\nException: " + ex.Message);
            }
            catch(System.FormatException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void ButtonMerge_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProgressBarMerge.Minimum = 1;
                bool DeleteSplitFiles;
                if (MessageBox.Show("Do You Want To Delete The Split Files?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    DeleteSplitFiles = false;
                }
                else
                {
                    DeleteSplitFiles = true;
                }

                if (TextBoxMergeFilePath.Text != "" && TextBoxMergeFileName.Text != "" && NumberOfSplitFiles!=0)
                {
                    new Thread(delegate()
                    {
                        System.Diagnostics.Stopwatch Watch = new Stopwatch();
                        Watch.Start();
                        Merge(DeleteSplitFiles);
                        Watch.Stop();
                        MessageBox.Show("Merging Successful.\n\nTime Elapsed: " + Watch.Elapsed);
                        Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                        {
                            ButtonMerge.IsEnabled = true;
                            ButtonAddSplitFiles.IsEnabled = true;
                            ButtonDeleteSplitFiles.IsEnabled = true;
                            TabSplit.IsEnabled = true;
                            TextBoxMergeFilePath.IsEnabled = true;
                            TextBoxMergeFileName.IsEnabled = true;
                            ButtonMergeBrowse.IsEnabled = true;
                        }, null);
                    }).Start();
                }
                else
                {
                    MessageBox.Show("Please Enter All The Details.");
                }
            }
            catch (System.ArgumentNullException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("The System is Out of Memory.\n\nException: " + ex.Message);
            }
            catch (System.Threading.ThreadStateException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void Merge(bool DeleteSplitFiles)
        {
            try
            {
                FileInfo fi = new FileInfo(MergeSplitFiles[0]);
                double ProgressMerge = 0;
                string MergeExtension = fi.Extension;
                using (File.Create(MergeFileFolder + @"\" + MergeFileName + MergeExtension)) { }
                for (int i = 0; i < NumberOfSplitFiles; i++)
                {
                    ProgressMerge = ((double)i / (double)NumberOfSplitFiles) * 100;
                    Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                    {
                        ProgressBarMerge.Minimum = 0;
                        ProgressBarMerge.Maximum = 100;
                        ProgressBarMerge.SetValue(ProgressBar.ValueProperty, ProgressMerge);
                        TextBlockMergeProgress.Text = Convert.ToString(ProgressMerge.ToString("###.##")) + "%";
                        ButtonMerge.IsEnabled = false;
                        ButtonAddSplitFiles.IsEnabled = false;
                        ButtonDeleteSplitFiles.IsEnabled = false;
                        TabSplit.IsEnabled = false;
                        TextBoxMergeFilePath.IsEnabled = false;
                        TextBoxMergeFileName.IsEnabled = false;
                        ButtonMergeBrowse.IsEnabled = false;
                    }, null);
                    FileInfo fi2 = new FileInfo(MergeSplitFiles[i]);
                    long SplitFileSize = fi2.Length;
                    string Extension = fi2.Extension;
                    long Remainder = SplitFileSize % 10;
                    long ChunkSize = SplitFileSize / 10;
                    byte[] bytes = new byte[ChunkSize + Remainder];
                    FileStream fsRead = new FileStream(MergeSplitFiles[i], FileMode.Open, FileAccess.Read);
                    FileStream fsWrite = new FileStream(MergeFileFolder + @"\" + MergeFileName + Extension, FileMode.Append, FileAccess.Write);
                    for (int j = 0; j < 10; j++)
                    {
                        fsRead.Read(bytes, 0, (int)ChunkSize);
                        fsWrite.Write(bytes, 0, (int)ChunkSize);
                    }
                    fsRead.Read(bytes, (int)ChunkSize, (int)Remainder);
                    fsWrite.Write(bytes, (int)ChunkSize, (int)Remainder);
                    if (DeleteSplitFiles == true)
                    {
                        fsRead.Dispose();
                        File.Delete(MergeSplitFiles[i]);
                    }
                    fsRead.Close();
                    Array.Clear(bytes, 0, (int)ChunkSize + (int)Remainder);
                    fsWrite.Close();
                }
                ProgressMerge = 100;
                Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { ProgressBarMerge.SetValue(ProgressBar.ValueProperty, ProgressMerge); }, null);
                Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate { TextBlockMergeProgress.Text = Convert.ToString(ProgressMerge.ToString("###.##")) + "%"; }, null);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("File Not Found. Please Try Again.\n\nException: " + ex.Message);

            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show("File Size is too Large.\n\nException: " + ex.Message);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (System.OutOfMemoryException ex)
            {
                MessageBox.Show("The System is Out of Memory.\n\nException: " + ex.Message);
            }
            catch (System.ArgumentNullException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (System.ComponentModel.InvalidEnumArgumentException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show("The File is Not Supported.\n\nException: " + ex.Message);
            }
            catch (System.Security.SecurityException ex)
            {
                MessageBox.Show("Security Exception.\n\nException: " + ex.Message);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show("The Directory was Not Found. Please Enter a Valid Directory.\n\nException: " + ex.Message);
            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("The Program is Not Authorized for Process.\n\nException: " + ex.Message);
            }
            catch (System.IO.PathTooLongException ex)
            {
                MessageBox.Show("The File Path is Too Long.\n\nException: " + ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                MessageBox.Show("The Object is Disposed.\n\nException: " + ex.Message);
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void RadioButtonCDSplit_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButtonCustomSplit_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxCustomSplit.Visibility = Visibility.Visible;
            TextBlockCustomSplit.Visibility = Visibility.Visible;
            TextBlockByte.Visibility = Visibility.Visible;
        }

        private void TextBoxFilePath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockFilePath.Visibility = Visibility.Hidden;
        }

        private void RadioButtonCustomSplit_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxCustomSplit.Visibility = Visibility.Collapsed;
            TextBlockByte.Visibility = Visibility.Collapsed;
            if (TextBoxCustomSplit.Text != "")
                TextBoxCustomSplit.Text = "";
            TextBlockCustomSplit.Visibility = Visibility.Collapsed;
        }

        private void TextBlockFilePath_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockFilePath.Visibility = Visibility.Collapsed;
            TextBoxFilePath.Focus();
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockCustomSplit.Visibility = Visibility.Collapsed;
            TextBoxCustomSplit.Focus();
        }

        private void TextBlockSplitFilePath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockSplitFilePath.Visibility = Visibility.Collapsed;
            TextBoxSplitFilePath.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //System.Windows.Application.Current.Shutdown();
            Environment.Exit(Environment.ExitCode);
            
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            //System.Windows.Application.Current.Shutdown();
        }

        private void TextBlockMergeFilePath_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockMergeFilePath.Visibility = Visibility.Collapsed;
            TextBoxMergeFilePath.Focus();
        }

        private void ButtonMergeBrowse_Click(object sender, RoutedEventArgs e)
        {
            var Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = Dialog.ShowDialog();
            MergeFileFolder = Dialog.SelectedPath;
            TextBoxMergeFilePath.Text = MergeFileFolder;
            TextBlockMergeFilePath.Visibility = Visibility.Collapsed;
            if(TextBoxMergeFilePath.Text != "")
            {
                TextBoxMergeFileName.Visibility = Visibility.Visible;
                TextBlockMergeFileName.Visibility = Visibility.Visible;
            }
        }

        private void TextBlockMergeFileName_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlockMergeFileName.Visibility = Visibility.Collapsed;
            TextBoxMergeFileName.Focus();
        }

        private void ButtonAddSplitFiles_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog FileDialog = new Microsoft.Win32.OpenFileDialog();
            FileDialog.Multiselect = true;
            Nullable<bool> result = FileDialog.ShowDialog();
            if(result==true)
            {
                foreach (string MultiFile in FileDialog.FileNames)
                {
                    MergeSplitFiles[NumberOfSplitFiles] = MultiFile;
                    ListViewSplitFiles.Items.Add(MergeSplitFiles[NumberOfSplitFiles]);
                    NumberOfSplitFiles++;
                }
            }
        }
        
        private void TextBoxMergeFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            MergeFileName = TextBoxMergeFileName.Text;
        }
        
        private void ButtonDeleteSplitFiles_Click(object sender, RoutedEventArgs e)
        {
            if (NumberOfSplitFiles > 0)
            {
                ListViewSplitFiles.Items.Remove(MergeSplitFiles[NumberOfSplitFiles-1]);
                NumberOfSplitFiles--;
            }
            else
            {
                MessageBox.Show("There Are No More Files To Delete.");
            }
        }
    }
}