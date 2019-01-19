using System.Windows;
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.IO;
using System.Windows.Input;

namespace mr_power
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        private static void d()
        {
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            Microsoft.Office.Interop.Excel.Range oRng;
            object misvalue = System.Reflection.Missing.Value;
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "First Name";
                oSheet.Cells[1, 2] = "Last Name";
                oSheet.Cells[1, 3] = "Full Name";
                oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "D1").Font.Bold = true;
                oSheet.get_Range("A1", "D1").VerticalAlignment =
                    Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[5, 2];

                saNames[0, 0] = "John";
                saNames[0, 1] = "Smith";
                saNames[1, 0] = "Tom";

                saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "B6").Value2 = saNames;

                //Fill C2:C6 with a relative formula (=A2 & " " & B2).
                oRng = oSheet.get_Range("C2", "C6");
                oRng.Formula = "=A2 & \" \" & B2";

                //Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                oRng = oSheet.get_Range("D2", "D6");
                oRng.Formula = "=RAND()*100000";
                oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "D1");
                oRng.EntireColumn.AutoFit();

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs("c:\\test\\test505.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
            }
            catch(Exception e)
            {

            }
           
        }


        static String dataB = "";

        static string filePath = "C:\\MRPower\\" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";

        private static void CreateFileOfDate()
        {


            //Debug.WriteLine(DateTime.Now.ToString("dd-MM-yyyy"));

            try
            {


                //File.OpenWrite(filePath);


            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally
            {

                

            }

        }



        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);



        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;

        const uint SC_CLOSE = 0xF060;

        const int WM_SHOWWINDOW = 0x00000018;
        const int WM_CLOSE = 0x10;

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(this.hwndSourceHook));
            }
        }


        IntPtr hwndSourceHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SHOWWINDOW)
            {
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
            else if (msg == WM_CLOSE)
            {
                handled = true;
            }
            return IntPtr.Zero;
        }
        
        private static NotifyIcon ni = new NotifyIcon();

        public static void Ini()
        {
            if(Util.battery==null)
            {
                Util.battery = new Battery[Util.cou];
            }

            string path = @"C:\MRPower";

            try
            {
                if (Directory.Exists(path))
                {
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);

                Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
            

        }
        

        public static int count = 0;

        PowerStatus pw = SystemInformation.PowerStatus;

        private async void BatteryMonitor()
        {

            await Task.Run(() => GetBatteryDetails());
        }

        

        internal void GetBatteryDetails()
        {


            PowerStatus ps = SystemInformation.PowerStatus;
            while (true)
            {
                this.Dispatcher.Invoke(() =>
                {
                    float value = ps.BatteryLifePercent * 100f;


                    batteryParcentage.Value = value;
                    if(value<15)
                    {
                        batteryParcentage.Foreground = Brushes.Red;
                    }
                    else if(value<40)
                    {

                        batteryParcentage.Foreground = Brushes.Red;
                    }
                    else
                    {
                        batteryParcentage.Foreground = Brushes.LawnGreen;

                    }
                    

                    if(ps.BatteryChargeStatus.ToString().Split(',').Length>0)
                    {
                        dataB += DateTime.Now.ToString("HH:mm:ss") + " --- not charging --- " + value+"\n";
                        File.WriteAllText(filePath, dataB);
                        //File.AppendText(filePath);
                        Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " --- not charging --- " + value);
                    }
                    else
                    {
                        dataB += DateTime.Now.ToString("HH:mm:ss") + " --- charging --- " + value+"\n";
                        Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " --- charging --- "+value);
                    }
                    //Debug.WriteLine(ps.BatteryChargeStatus);
                    

                });
                System.Threading.Thread.Sleep(1000);





            }
        }


        private void WindowLoaded()
        {

            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = (desktopWorkingArea.Right - this.Width)/3;
            this.Top = 0;
        }


        public MainWindow()
        {


            d();

            dataB += File.ReadAllLines(filePath).ToString();
            InitializeComponent();
            WindowLoaded();
            Ini();
            CreateFileOfDate();
            BatteryMonitor();



            ni.Icon = new System.Drawing.Icon(@"F:\c_sharp\mr_power\mr_power\mr_power\faka.ico");
            ni.Visible = false;
            ni.Click +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };



        }


        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                this.Hide();

            base.OnStateChanged(e);
        }


        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("history");
            WindowHistory win2 = new WindowHistory();
            win2.Show();

        }
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {

            if(this.WindowState==WindowState.Minimized)
            {
                this.WindowState = WindowState.Maximized;
                ni.Visible = false;
            }
            else
            {
                this.WindowState = WindowState.Minimized;
                ni.Visible = true;
            }

            Debug.WriteLine("close");
        }

        

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("exit");
            Environment.Exit(0);
            //System.Windows.Application.Current.Shutdown();
        }
    }

}