using Shell32;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Imaging;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using ExifLib;
using System.Globalization;
using System.Linq;

namespace CreationDate
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private IContainer components;
		private Hashtable m_FilesToRename = new Hashtable();
		private System.Windows.Forms.ProgressBar m_RenameProgressBar;
		private ArrayList m_CommandLinePath = new ArrayList();
		private string m_CurPath;
		private System.Windows.Forms.Timer mTimerLoad;
		public int mHourDiff = 0;
		Shell mShell = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.m_RenameProgressBar = new System.Windows.Forms.ProgressBar();
			this.mTimerLoad = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// m_RenameProgressBar
			// 
			this.m_RenameProgressBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_RenameProgressBar.Location = new System.Drawing.Point(0, 0);
			this.m_RenameProgressBar.Name = "m_RenameProgressBar";
			this.m_RenameProgressBar.Size = new System.Drawing.Size(426, 24);
			this.m_RenameProgressBar.TabIndex = 0;
			// 
			// mTimerLoad
			// 
			this.mTimerLoad.Tick += new System.EventHandler(this.mTimerLoad_Tick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(426, 24);
			this.Controls.Add(this.m_RenameProgressBar);
			this.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Processing...";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}

		public void FillRenameHashTable()
		{
			Application.DoEvents();

			m_CurPath = Convert.ToString(m_CommandLinePath[1]);
			m_CurPath = m_CurPath.TrimEnd('\\');
			m_CurPath = m_CurPath.TrimEnd('/');

			FileInfo curFileInfo = new FileInfo(m_CurPath);

			if (Convert.ToBoolean(curFileInfo.Attributes & FileAttributes.Directory))
			{
				//This is a directory, get the list of file paths
				DirectoryInfo curDirectoryInfo = new DirectoryInfo(m_CurPath);
				FileInfo[] fileInfoArray = curDirectoryInfo.GetFiles();

				m_RenameProgressBar.Maximum = 2 * fileInfoArray.Length;

				for (int j = 0; j < fileInfoArray.Length; j++)
				{

					Application.DoEvents();

					FileInfo curInfo = fileInfoArray[j];
					SaveNewPaths(curInfo.FullName);
				}
			}
			else
			{
				SaveNewPaths(Convert.ToString(m_CommandLinePath[1]));
			}
		}

		public void GetCommandLinePath()
		{
			m_CommandLinePath.AddRange(Environment.GetCommandLineArgs());
			m_RenameProgressBar.Minimum = 0;
			m_RenameProgressBar.Maximum = 2 * (m_CommandLinePath.Count - 1);
		}

		public void SaveNewPaths(string imageFullPath)
		{
			string newFileName = "";

			string imageFileName = Path.GetFileName(imageFullPath);
			string imagePath = Path.GetDirectoryName(imageFullPath);

			string ext = Path.GetExtension(imageFullPath);
			ext = ext.ToLower();

			try
			{
				if (ext.CompareTo(".jpg") == 0 || ext.CompareTo(".png") == 0)
				{
					try
					{
						using (ExifReader reader = new ExifReader(imageFullPath))
						{
							// Extract the tag data using the ExifTags enumeration
							DateTime datePictureTaken;
							if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized, out datePictureTaken))
							{
								datePictureTaken = datePictureTaken.AddHours(mHourDiff);
								newFileName = datePictureTaken.ToString("yyyyMMdd_HHmmss");
							}
						}
					}
					catch (Exception)
					{
						newFileName = Path.GetFileNameWithoutExtension(imageFileName);
					}
				}
				/*@@
                                else if(ext.CompareTo(".m2ts") == 0 || ext.CompareTo(".mts") == 0)
                                {
                                    FileInfo nonImageInfo = new FileInfo(imageFullPath);
                                    if (new FileInfo(imageFullPath + ".modd").Exists)
                                    {
                                        newFileName = Path.GetFileNameWithoutExtension(nonImageInfo.Name.Insert(8, "_"));
                                    }
                                    else if (ext.CompareTo(".mp4") == 0 || ext.CompareTo(".png") == 0)
                                    {
                                        newFileName = Path.GetFileNameWithoutExtension(nonImageInfo.Name);
                                    }
                                    else
                                    {
                                        DateTime creationTime = nonImageInfo.CreationTime;
                                        newFileName = creationTime.ToString("yyyyMMdd_HHmmss");
                                    }
                                }
                                else if (ext.CompareTo(".mp4") == 0)
                @@*/
				else if (imageFileName.StartsWith("PXL_") == true && imageFileName.EndsWith(".mp4"))
				{
					//For videos on Pixel phones
					if (mShell == null)
					{
						mShell = new Shell();
					}

					Folder folder = mShell.NameSpace(imagePath);
					FolderItem item = folder.ParseName(imageFileName);

					// Property index for "Media created"
					// This index is stable on modern Windows versions
					const int MediaCreatedIndex = 208;

					string mediaCreatedRaw = folder.GetDetailsOf(item, MediaCreatedIndex);
					string mediaCreated = new string(mediaCreatedRaw.Where(c => !char.IsControl(c) && c != '\u200E' && c != '\u200F').ToArray()).Trim();

					if (string.IsNullOrWhiteSpace(mediaCreated) == false)
					{
						DateTime.TryParse(mediaCreated, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime recordedLocalTime);
						
						// Get seconds from filename
						int seconds = 0;

						// Filename expected: PXL_YYYYMMDD_HHMMSSmmm.TS.mp4
						var match = System.Text.RegularExpressions.Regex.Match(
							imageFileName,
							@"PXL_\d{8}_\d{6}(\d{3})\.TS\.mp4",
							System.Text.RegularExpressions.RegexOptions.IgnoreCase);

						if (match.Success)
						{
							// Get HHMMSS from filename
							string timePart = imageFileName.Substring(13, 6); // HHMMSS
							if (int.TryParse(timePart.Substring(4, 2), out int sec))
							{
								seconds = sec;
							}
						}

						// Rebuild full DateTime with seconds from filename
						recordedLocalTime = new DateTime(
							recordedLocalTime.Year,
							recordedLocalTime.Month,
							recordedLocalTime.Day,
							recordedLocalTime.Hour,
							recordedLocalTime.Minute,
							seconds
						);

						newFileName = recordedLocalTime.ToString("yyyyMMdd_HHmmss");
					}
				}
				else
				{
					FileInfo nonImageInfo = new FileInfo(imageFullPath);
					DateTime lastWriteTime = nonImageInfo.LastWriteTime;
					lastWriteTime = lastWriteTime.AddHours(mHourDiff);
					newFileName = lastWriteTime.ToString("yyyyMMdd_HHmmss");
				}

				if (newFileName.Length > 0)
				{
					newFileName = newFileName.Replace("-", "_");
					newFileName = newFileName.Replace(".", "");
					newFileName = newFileName.Replace(":", "");
					newFileName = newFileName.Replace(" ", "_");
					newFileName = newFileName.Replace("VID_", "");
					newFileName = newFileName.Replace("PIX_", "");
					newFileName = newFileName.Replace("PXL_", "");
					newFileName = newFileName.Replace("Screenshot_", "");

					m_FilesToRename.Add(imageFullPath, newFileName + ext);
					this.Text = "Creating new name: '" + newFileName + ext + "'";
				}

				newFileName = "";

				m_RenameProgressBar.Increment(1);
			}
			catch (Exception ex)
			{

			}
		}

		public void RenameFilesInList()
		{
			List<string> filesToRenameSorted = new List<string>(m_FilesToRename.Count);

			foreach (object key in m_FilesToRename.Keys)
			{
				filesToRenameSorted.Add(Convert.ToString(key));
			}

			filesToRenameSorted.Sort();

			m_CurPath += "/";
			string prevSaveFileName = string.Empty;
			int index = 0;

			foreach (string fileToRenamePath in filesToRenameSorted)
			{
				try
				{
					FileInfo curImageInfo = new FileInfo(fileToRenamePath);

					string saveFileName = Convert.ToString(m_FilesToRename[fileToRenamePath]);

					if (saveFileName == prevSaveFileName)
					{
						saveFileName = String.Format("{0}_{1}{2}",
							Path.GetFileNameWithoutExtension(saveFileName),
							index++,
							Path.GetExtension(saveFileName));
					}
					else
					{
						index = 0;
						prevSaveFileName = saveFileName;
					}

					string newFileName = saveFileName;

					this.Text = "Renaming '" + curImageInfo.Name + "' to: '" + newFileName + "'";

					Application.DoEvents();

					curImageInfo.CopyTo(m_CurPath + newFileName);
					Thread.Sleep(50);
					curImageInfo.Delete();

					m_RenameProgressBar.Increment(1);
				}
				catch (Exception) { }
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			mTimerLoad.Start();
		}

		private void mTimerLoad_Tick(object sender, EventArgs e)
		{
			mTimerLoad.Stop();

			FormWelcome formWelcome = new FormWelcome();
			if (formWelcome.ShowDialog(this) == DialogResult.OK)
			{
				mHourDiff = formWelcome.mHourDiff;
			}

			GetCommandLinePath();

			FillRenameHashTable();

			//Do a garbage collection, otherwise we cannot delete the files
			//after we copy them to a new name.
			//@@			GC.Collect();

			RenameFilesInList();

			this.Close();
		}
	}
}
