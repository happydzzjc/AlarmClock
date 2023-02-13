using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace AlarmClock
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// ��ʱ��
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
        /// �����ʼ��ť
        /// ��ʼ����ʱ������ʼ��ʱ��
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
        /// ��ʱ���ص�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Execute(object sender, System.Timers.ElapsedEventArgs e)
        {
            string message = "���Ѿ��������� " + promptInterval.Value + " �����ˣ�\n��ע����Ϣ��";
            MessageBox.Show(message, "��ܰ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

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
        /// ֹͣ��ʱ��
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
        /// ��ʱ���������̺߳���
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
        /// ���û��رմ���ʱ����С����ϵͳ����
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
        /// ϵͳ����˫������ԭ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                //��ԭ������ʾ    
                this.Activate();
                this.Show();
                base.WindowState = FormWindowState.Normal;


                //����������ʾͼ��
                this.ShowInTaskbar = true;
                //������ͼ������
                notifyIcon1.Visible = false;
            }
        }

        /// <summary>
        /// ϵͳ���Ҽ��˳�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �˳�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("�Ƿ�ȷ���˳�����", "�˳�", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // �ر����е��߳�
                this.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// ϵͳ���Ҽ���ԭ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ��ԭToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Activate();
            this.Show();
            base.WindowState = FormWindowState.Normal;
        }
    }
}