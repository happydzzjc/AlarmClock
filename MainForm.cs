using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AlarmClock
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private System.Timers.Timer? timer = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        /// <summary>
        /// 点击开始按钮
        /// 初始化定时器，开始定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, EventArgs e)
        {
            
            start.Enabled = false;
            stop.Enabled = true;
            promptInterval.Enabled = false;
            disposable.Enabled = false;
            repeat.Enabled = false;

            timer = new System.Timers.Timer();
            timer.Interval = (double)promptInterval.Value * 60 * 1000;
            timer.AutoReset = repeat.Checked;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Execute);

            timer.Start();
        }

        /// <summary>
        /// 定时器回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Execute(object sender, System.Timers.ElapsedEventArgs e)
        {
            string message = "你已经连续工作 " + promptInterval.Value + " 分钟了，\n请注意休息！";
            MessageBox.Show(message, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

            timer.Stop();
            if (disposable.Checked)
            {
                Action invokeAction = new Action(InvokeMethod);
                if (this.InvokeRequired) {
                    this.Invoke(invokeAction);
                }
            }
            else
            {
                timer.Start();
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, EventArgs e)
        {
            start.Enabled = true;
            stop.Enabled = true;
            promptInterval.Enabled = true;
            disposable.Enabled = true;
            repeat.Enabled = true;
            if (timer != null) {
                timer.Stop();
                timer.Dispose();
            }
        }

        /// <summary>
        /// 定时器调用主线程函数
        /// </summary>
        void InvokeMethod() 
        {
            start.Enabled = true;
            stop.Enabled = true;
            promptInterval.Enabled = true;
            disposable.Enabled = true;
            repeat.Enabled = true;
            timer.Dispose();
        }

        /// <summary>
        /// 当用户关闭窗体时，最小化到系统托盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                notifyIcon1.Visible = true;
                this.Hide();
            }
        }

        /// <summary>
        /// 系统托盘双击，还原窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                this.Activate();
                this.Show();
                base.WindowState = FormWindowState.Normal;


                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        /// <summary>
        /// 系统盘右键退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 系统盘右键还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Activate();
            this.Show();
            base.WindowState = FormWindowState.Normal;
        }
    }
}