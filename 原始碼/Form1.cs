using OpenCvSharp;
using OpenCvSharp.Features2D;
using Point = System.Drawing.Point;

namespace 蝦皮總動員;

public partial class Form1 : Form
{

    private Point startPoint; // Starting point of the selection
    private Rectangle selectionRect; // To store the selection rectangle
    private bool selecting; // To indicate if the user is selecting
    private Bitmap capturedImage;
    public Form1()
    {
        InitializeComponent();
    }

    private void CaptureButton_Click(object sender, EventArgs e)
    {

        Rectangle totalBounds = System.Windows.Forms.SystemInformation.VirtualScreen;// 獲取所有螢幕的總邊界

        Form overlayForm = new Form();
        overlayForm.FormBorderStyle = FormBorderStyle.None;
        overlayForm.StartPosition = FormStartPosition.Manual;
        overlayForm.Bounds = totalBounds;
        overlayForm.TopMost = true;
        overlayForm.Opacity = 0.5;
        overlayForm.MouseDown += overlayForm_MouseDown;
        overlayForm.MouseMove += overlayForm_MouseMove;
        overlayForm.MouseUp += overlayForm_MouseUp;
        overlayForm.Paint += overlayForm_Paint;
        overlayForm.ShowDialog();
    }

    private void overlayForm_MouseDown(object sender, MouseEventArgs e)
    {
        // Start the selection
        selecting = true;
        startPoint = e.Location;
        selectionRect = new Rectangle(startPoint, System.Drawing.Size.Empty); // Initialize the rectangle
    }

    private void overlayForm_MouseMove(object sender, MouseEventArgs e)
    {
        if (selecting)
        {
            // Calculate the selection rectangle
            int x = Math.Min(e.X, startPoint.X);
            int y = Math.Min(e.Y, startPoint.Y);
            int width = Math.Abs(e.X - startPoint.X);
            int height = Math.Abs(e.Y - startPoint.Y);
            selectionRect = new Rectangle(x, y, width, height);
            (sender as Form).Invalidate();
        }
    }

    private async void overlayForm_MouseUp(object sender, MouseEventArgs e)
    {
        // End the selection
        selecting = false;

        // Hide the form temporarily to capture the screen
        (sender as Form).Hide();

        Bitmap bitmap = new Bitmap(selectionRect.Width, selectionRect.Height);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.CopyFromScreen(selectionRect.Location, System.Drawing.Point.Empty, selectionRect.Size);

            (sender as Form).Close();
            this.Activate();
            this.Focus();
            this.BringToFront();
            this.TopMost = true;
            this.TopMost = false;
            this.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Normal;
            this.WindowState = FormWindowState.Maximized;
            this.WindowState = FormWindowState.Normal;
            capturedImage = bitmap;
            Excute_Button_Click(this, EventArgs.Empty);
        }
    }
    private void SaveImage(Image image, string directory, string fileName)
    {
        // 檢查目錄是否存在，如果不存在，則建立目錄
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // 儲存圖片
        string filePath = Path.Combine(directory, fileName + ".png");
        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
    }


    private void overlayForm_Paint(object sender, PaintEventArgs e)
    {
        if (selecting)
        {
            // Draw the selection rectangle
            using (Pen pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, selectionRect);
            }
        }
    }

    private void Excute_Button_Click(object sender, EventArgs e)
    {
        // 檢查 capturedImage 是否為 null
        if (capturedImage == null)
        {
            // 如果 capturedImage 為 null，則不進行任何操作
            excuteButton.Enabled = false;
            return;
        }
        excuteButton.Enabled = true;
        string fileName;
        string directory;
        switch (comboBox1.SelectedIndex)
        {
            case 0:
                ExecuteFindFaultsFunction();
                directory = @"./assets/大家來找碴/";
                fileName = "大家來找碴_" + GetAndUpdateSerialNumber("大家來找碴").ToString("D4");
                SaveImage(capturedImage, directory, fileName);
                break;
            case 1:
                ExecuteGoldenBrainFunctionAsync();
                directory = @"./assets/金頭腦/";
                fileName = "金頭腦_" + GetAndUpdateSerialNumber("金頭腦").ToString("D4");
                SaveImage(capturedImage, directory, fileName);
                break;
        }
    }


    private int GetAndUpdateSerialNumber(string function)
    {
        int serialNumber = 0;
        string fileName = "SerialNumbers.txt";
        Dictionary<string, int> serialNumbers = new Dictionary<string, int>();

        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    int.TryParse(parts[1], out serialNumber);
                    serialNumbers[parts[0]] = serialNumber;
                }
            }
        }

        if (serialNumbers.ContainsKey(function))
        {
            serialNumber = serialNumbers[function];
            serialNumber++;
            serialNumbers[function] = serialNumber;
        }
        else
        {
            serialNumber = 1;
            serialNumbers[function] = serialNumber;
        }

        List<string> outputLines = new List<string>();
        foreach (KeyValuePair<string, int> entry in serialNumbers)
        {
            outputLines.Add(entry.Key + ":" + entry.Value.ToString());
        }

        File.WriteAllLines(fileName, outputLines);

        return serialNumber;
    }




    private void ExecuteFindFaultsFunction()
    {
        // 在這裡實現 "大家來找碴功能"
        Bitmap bitmap = capturedImage;

        (Bitmap bitmapA, Bitmap bitmapB) = DivideWithMiddle(bitmap);

        // 將Bitmap轉換為Mat
        Mat img1_color = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmapA);
        Mat img2_color = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmapB);

        // 創建灰度圖的淺拷貝
        Mat img1 = new Mat();
        Cv2.CvtColor(img1_color, img1, ColorConversionCodes.BGR2GRAY);
        Mat img2 = new Mat();
        Cv2.CvtColor(img2_color, img2, ColorConversionCodes.BGR2GRAY);

        // 使用SIFT來找到特徵點和描述符
        SIFT sift = SIFT.Create();
        KeyPoint[] keypoints1, keypoints2;
        Mat descriptors1 = new Mat(), descriptors2 = new Mat();
        sift.DetectAndCompute(img1, null, out keypoints1, descriptors1);
        sift.DetectAndCompute(img2, null, out keypoints2, descriptors2);

        // 使用FLANN來匹配描述符
        FlannBasedMatcher matcher = new FlannBasedMatcher();
        DMatch[][] matches = matcher.KnnMatch(descriptors1, descriptors2, 2);

        // 使用Lowe's ratio test來找到好的匹配
        List<DMatch> goodMatches = new List<DMatch>();
        foreach (DMatch[] match in matches)
        {
            if (match[0].Distance < 0.9 * match[1].Distance)
            {
                goodMatches.Add(match[0]);
            }
        }

        Dictionary<Point2f, int> displacementCounts = new Dictionary<Point2f, int>();
        foreach (DMatch match in goodMatches)
        {
            Point2f pt1 = keypoints1[match.QueryIdx].Pt;
            Point2f pt2 = keypoints2[match.TrainIdx].Pt;
            Point2f displacement = new Point2f(pt1.X - pt2.X, pt1.Y - pt2.Y);
            if (displacementCounts.ContainsKey(displacement))
            {
                displacementCounts[displacement]++;
            }
            else
            {
                displacementCounts[displacement] = 1;
            }
        }
        // 找到出現次數最多的位移
        Point2f modeDisplacement = new Point2f(0, 0);
        int maxCount = 0;
        foreach (KeyValuePair<Point2f, int> entry in displacementCounts)
        {
            if (entry.Value > maxCount)
            {
                maxCount = entry.Value;
                modeDisplacement = entry.Key;
            }
        }
        // 創建位移矩陣
        Mat M = Cv2.GetRotationMatrix2D(new Point2f(0, 0), 0, 1);
        M.Set<double>(0, 2, modeDisplacement.X);  // 使用眾數位移
        M.Set<double>(1, 2, modeDisplacement.Y);  // 使用眾數位移


        // 獲取圖片的大小
        int rows = img2_color.Rows;
        int cols = img2_color.Cols;
        // 創建一個新的Mat來存儲位移後的B圖像
        Mat img2_color_shifted = new Mat();
        // 將彩色圖片B位移回去
        Cv2.WarpAffine(img2_color, img2_color_shifted, M, new OpenCvSharp.Size(cols, rows));

        // 將這兩個彩色圖像分別設置到 pictureBoxA 和 pictureBoxB
        pictureBoxA.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(img1_color);
        pictureBoxB.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(img2_color_shifted);

        // 呼叫 Form2
        Form2 form2 = new Form2(pictureBoxA.Image, pictureBoxB.Image);
        form2.Show();
    }

    private async Task ExecuteGoldenBrainFunctionAsync()
    {
        // "金頭腦功能"
        Bitmap bitmap = capturedImage;
        // 將這兩個彩色圖像分別設置到 pictureBoxA 和 pictureBoxB
        pictureBoxA.Image = bitmap;
        pictureBoxB.Image = null;

        await GeminiApiService.CallApiAsync(bitmap);
    }
    private (Bitmap bitmapA, Bitmap bitmapB) DivideWithMiddle(Bitmap bitmap)
    {

        // 判斷應該如何分割圖片（垂直分割還是水平分割）
        bool splitVertically = bitmap.Width > bitmap.Height;

        // 計算兩張圖片的大小和位置
        Rectangle rectA, rectB;
        if (splitVertically)
        {
            rectA = new Rectangle(0, 0, bitmap.Width / 2, bitmap.Height);
            rectB = new Rectangle(bitmap.Width / 2, 0, bitmap.Width / 2, bitmap.Height);
        }
        else
        {
            rectA = new Rectangle(0, 0, bitmap.Width, bitmap.Height / 2);
            rectB = new Rectangle(0, bitmap.Height / 2, bitmap.Width, bitmap.Height / 2);
        }

        Bitmap bitmapA = bitmap.Clone(rectA, bitmap.PixelFormat);
        Bitmap bitmapB = bitmap.Clone(rectB, bitmap.PixelFormat);

        return (bitmapA, bitmapB);
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (comboBox1.SelectedIndex)
        {
            case 0:
                excuteButton.Text = "顯示差異圖片";
                break;
            case 1:
                excuteButton.Text = "呼叫API";
                break;
        }
    }
}


