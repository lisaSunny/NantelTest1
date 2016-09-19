using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QCAP.NET;
using System.IO;

namespace Nantels1
{

    public partial class Form1 : Form
    {
        public PictureBox[] pictureBoxVideo = new PictureBox[8];     //创建一个PictureBox数组,用于显示采集卡图像
        public PictureBox[] controlCloneVideo = new PictureBox[8];     //创建一个PictureBox数组，用于显示控制台克隆图像
        public PictureBox[] userCloneVideo = new PictureBox[8];     //创建一个PictureBox数组，用于显示用户克隆图像
        public uint[] pBoxVideo = new uint[8];
        public int[] pBoxTag = new int[8];
        // public PictureBox ShareRecordWindow;
        public uint[] m_hCapDev = new uint[8];          //创建一个设备流捕获数组，用于存放设备编号
        //public uint[] m_hCloneCapDev = new uint[2];     // 克隆流捕获装置
        string m_strChipName = "QP0203 PCI";
        string m_strChipName1 = "QP0203 PCI";
        public uint i = 0;
        public uint m_hRtspCapDev = 0;                  // RTSP流捕获装置             
        public bool m_bShowClone = false;


        public CollectionCardInfo cardInfo;


        public Form1()
        {
            InitializeComponent();
        }


        //获取采集卡信息监听事件，相当于初始化
        private void getDevice_Click(object sender, EventArgs e)
        {

            for (i = 0; i < 2; i++)
            {
                m_hCapDev[i] = 0x00000000;
            }


            for (i = 0; i < 2; i++)
            {
                pBoxVideo[i] = (uint)pictureBoxVideo[i].Handle.ToInt32();

            }

            cardInfo.getDevice(pBoxVideo, pictureBoxVideo);
            cardInfo.ShowControlCloneVideo(true, pictureBoxVideo, controlCloneVideo);
            //cardInfo.audioInput();      //开启音频


        }

        //停止视频播放或者克隆视频
        private void btn_stop_Click(object sender, EventArgs e)
        {
            cardInfo.stopVideo();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cardInfo.stopVideo();
        }

        //截图
        private void btn_snapShot_Click(object sender, EventArgs e)
        {
            cardInfo.snapShot();
        }
        uint pic1;
        //录像
        private void record_Click(object sender, EventArgs e)
        {
           
            cardInfo.startRecord(pic1,pszTitle,pszArtist,pszComments,pszGenre,pszComposer);

        }

        //停止录像
        private void stop_record_Click(object sender, EventArgs e)
        {
            cardInfo.stopRecord();
        }

        public PictureBox pic;
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
            #region
            cardInfo = new CollectionCardInfo();
            // cardInfo.ShowControlCloneVideo(true, pictureBoxVideo, controlCloneVideo);
            //控制台布局编辑显示
            controlCloneVideo[0] = controlClone1;
            controlCloneVideo[1] = controlClone2;
            controlCloneVideo[2] = controlClone3;
            controlCloneVideo[3] = controlClone4;
            controlCloneVideo[4] = controlClone5;
            controlCloneVideo[5] = controlClone6;
            controlCloneVideo[6] = controlClone7;
            controlCloneVideo[7] = controlClone8;
         
     
            //采集卡信息显示
            pictureBoxVideo[0] = pictureBox1;
            pictureBoxVideo[1] = pictureBox2;
            pictureBoxVideo[2] = pictureBox3;
            pictureBoxVideo[3] = pictureBox4;
            pictureBoxVideo[4] = pictureBox5;
            pictureBoxVideo[5] = pictureBox6;
            pictureBoxVideo[6] = pictureBox7;
            pictureBoxVideo[7] = pictureBox8;

            //用户直播画面显示
            userCloneVideo[0] = userClone1;
            userCloneVideo[1] = userClone2;
            userCloneVideo[2] = userClone3;
            userCloneVideo[3] = userClone4;
            userCloneVideo[4] = userClone5;
            userCloneVideo[5] = userClone6;
            userCloneVideo[6] = userClone7;
            userCloneVideo[7] = userClone8;


            pBoxTag[0] = Convert.ToInt32(pictureBoxVideo[0].Tag);
            pBoxTag[1] = Convert.ToInt32(pictureBoxVideo[1].Tag);
            pBoxTag[2] = Convert.ToInt32(pictureBoxVideo[2].Tag);
            pBoxTag[3] = Convert.ToInt32(pictureBoxVideo[3].Tag);
            pBoxTag[4] = Convert.ToInt32(pictureBoxVideo[4].Tag);
            pBoxTag[5] = Convert.ToInt32(pictureBoxVideo[5].Tag);
            pBoxTag[6] = Convert.ToInt32(pictureBoxVideo[6].Tag);
            pBoxTag[7] = Convert.ToInt32(pictureBoxVideo[7].Tag);
            #endregion

            
           
        }

        //停止分享录像
        private void stopShareRecord_Click(object sender, EventArgs e)
        {
            cardInfo.stopRecord();
        }


        int x;          //获取当前控件的X坐标
        int y;          //获取当前控件的Y坐标
        public bool isDrag;              //是否正在拖拽
        int width = 230;
        int height = 195;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            isDrag = true;
            snapShotPicture.Left = e.X;
            snapShotPicture.Top = e.Y;
            //截图
            Bitmap Pic = new Bitmap(143, 139);

            Graphics g = Graphics.FromImage(Pic);


            g.CopyFromScreen(pictureBox1.PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(143, 139));
            snapShotPicture.Image = Pic;
            g.Dispose();

            snapShotPicture.Visible = true;



            #region
            //int abc;
            //PictureBox pic;
            //pic = (PictureBox)sender;

            //abc = int.Parse(pic.Tag.ToString());
            // MessageBox.Show("",abc.ToString());
            #endregion
        }
      
        int x1, x2;
        int y1, y2;
        int width1, width2;
        int height1, height2;
      
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            isDrag = false;
            snapShotPicture.Visible = false;
            Point p = new Point();     //鼠标位置
            p = Cursor.Position;
            Point s = new Point();     //鼠标所在控件位置

            int a = Convert.ToInt32((sender as PictureBox).Tag) - 1;  //获取当前控件的Tag值           
            for (int i = 0; i < 8; i++)
            {
                //int tag = Convert.ToInt32(pictureBoxVideo[i].Tag) - 1;
                s.X = controlCloneVideo[i].Left;
                s.Y = controlCloneVideo[i].Top;
                s = controlCloneVideo[i].PointToScreen(new Point(0, 0));
               
                if (((s.X <= p.X) && (p.X <= (s.X + controlCloneVideo[i].Width))) && ((s.Y <= p.Y) && (p.Y <= (s.Y + controlCloneVideo[i].Height))))
                {
                                    
                    //将cloneVideo[i]与cloneVideo[a]位置交换
                     x1 = controlCloneVideo[i].Left;
                     y1 = controlCloneVideo[i].Top;
                     height1 = controlCloneVideo[i].Height;
                     width1 = controlCloneVideo[i].Width;                   
                    x2 = controlCloneVideo[a].Left;
                    y2 = controlCloneVideo[a].Top;
                    height2 = controlCloneVideo[a].Height;
                    width2 = controlCloneVideo[a].Width;
                    controlCloneVideo[i].Height = height2;
                    controlCloneVideo[i].Width = width2;
                    controlCloneVideo[i].Left = x2;
                    controlCloneVideo[i].Top = y2;
                    controlCloneVideo[a].Top = y1;
                    controlCloneVideo[a].Left = x1;
                    controlCloneVideo[a].Width = width1;
                    controlCloneVideo[a].Height = height1;
                    controlCloneVideo[a].Visible = true;
                   
                 

                    //将userCloneVideo[i]与userCloneVideo[a]位置交换(使用户直播与控制台同步)
                    int ux1 = userCloneVideo[i].Left;
                    int uy1 = userCloneVideo[i].Top;
                    int uheight1 = userCloneVideo[i].Height;
                    int uwidth1 = userCloneVideo[i].Width;
                    int ux2 = userCloneVideo[a].Left;
                    int uy2 = userCloneVideo[a].Top;
                    int uheight2 = userCloneVideo[a].Height;
                    int uwidth2 = userCloneVideo[a].Width;
                    userCloneVideo[i].Height = uheight2;
                    userCloneVideo[i].Width = uwidth2;
                    userCloneVideo[i].Left = ux2;
                    userCloneVideo[i].Top = uy2;
                    userCloneVideo[a].Top = uy1;
                    userCloneVideo[a].Left = ux1;
                    userCloneVideo[a].Width = uwidth1;
                    userCloneVideo[a].Height = uheight1;
                    userCloneVideo[a].Visible = true;
                    return;
                    userCloneVideo[i].Visible = false;
                    
                }
                
            }


        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrag)
            {

                int n = Convert.ToInt32((sender as PictureBox).Tag) - 1;
                for (int i = 0; i < 8; i++)
                {
                    if (n == i)
                    {
                        snapShotPicture.Left += pictureBoxVideo[i].Left + Convert.ToInt16(e.X - snapShotPicture.Left);
                        snapShotPicture.Top += pictureBoxVideo[i].Top + Convert.ToInt16(e.Y - snapShotPicture.Top);
                        return;
                    }

                }



            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            isDrag = true;
            #region
            //  snapShotPicture.Left = e.X;
            // snapShotPicture.Top = e.Y;
            //截图
            // Bitmap Pic = new Bitmap(138, 139);

            //   Graphics g = Graphics.FromImage(Pic);
            //Bitmap Pic = new Bitmap(pictureBoxVideo[2].Width, pictureBoxVideo[2].Height);

            //Graphics g = Graphics.FromImage(Pic);
            //snapShotPicture.Left = pictureBoxVideo[2].Left;
            //snapShotPicture.Top = pictureBoxVideo[2].Top;
            //g.CopyFromScreen(pictureBoxVideo[2].PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(pictureBoxVideo[2].Width, pictureBoxVideo[2].Height));
            //snapShotPicture.Image = Pic;
            //snapShotPicture.Visible = true;
            #endregion


            int s = Convert.ToInt32((sender as PictureBox).Tag) - 1;
            for (int i = 0; i < 8; i++)
            {
                if (s == i)
                {
                    Bitmap Pic = new Bitmap(pictureBoxVideo[i].Width, pictureBoxVideo[i].Height);

                    Graphics g = Graphics.FromImage(Pic);
                    snapShotPicture.Left = pictureBoxVideo[s].Left;
                    snapShotPicture.Top = pictureBoxVideo[s].Top;
                    g.CopyFromScreen(pictureBoxVideo[s].PointToScreen(new Point(0, 0)), new Point(0, 0), new Size(pictureBoxVideo[s].Width, pictureBoxVideo[s].Height));
                    snapShotPicture.Image = Pic;
                    snapShotPicture.Visible = true;
                    return;
                }
            }


        }
             
        //直播按钮
        private void userClone_Click(object sender, EventArgs e)
        {
            bool bShowUserClone = userClone.Checked;
            if (bShowUserClone)
            {
                for (int i = 0; i < 8; i++)
                {                    
                        cardInfo.ShowUserCloneVideo(true, controlCloneVideo, userCloneVideo);
                        userCloneVideo[i].Visible = true;
                }
                
            }
            else {
                for (int i = 0; i < 8; i++)
                {                  
                        cardInfo.ShowUserCloneVideo(false, controlCloneVideo, userCloneVideo);
                        userCloneVideo[i].Visible = false;
                       
                }
            }
        }

        //全屏
        private void button1_Click(object sender, EventArgs e)
        {
            
            cardInfo.fullScreen(controlCloneVideo);
            cardInfo.userFullScreen(userCloneVideo);
            for (int i = 0; i < 8; i++)
            {
                userCloneVideo[i].Visible = false;
            }
           
        }

        //画中画
        private void button2_Click(object sender, EventArgs e)
        {
            cardInfo.pop(controlCloneVideo);
            cardInfo.userPop(userCloneVideo);
            for (int i = 0; i < 8; i++)
            {
                userCloneVideo[i].Visible = false;
            }
        }


        //重置
        private void button5_Click(object sender, EventArgs e)
        {
            cardInfo.reStart(controlCloneVideo,userCloneVideo);
            for (int i = 0; i < 8; i++)
            {
                userCloneVideo[i].Visible = false;
            }
        }

        private void userClone2_Click(object sender, EventArgs e)
        {

        }
    

        private void button4_Click(object sender, EventArgs e)
        {
            cardInfo.fourShow(controlCloneVideo);
            cardInfo.userFourShow(userCloneVideo);
            for (int i = 0; i < 8; i++)
            {
                userCloneVideo[i].Visible = false;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            cardInfo.threeShow(controlCloneVideo);
            cardInfo.userThreeShow(userCloneVideo);
            for (int i = 0; i < 8; i++)
            {
                userCloneVideo[i].Visible = false;
            }
          
        }

        string pszTitle;
        string pszArtist;
        string pszComments;
        string pszGenre;
        string pszComposer;
        //用户输入信息
        private void ok_Click(object sender, EventArgs e)
        {
            pszTitle = hospital.Text;
            pszArtist = doctor.Text;
            pszComments = department.Text;
            pszGenre = surgery.Text;
            pszComposer = age.Text;
          
            if ((pszTitle != "") && (pszArtist != "") && (pszComments != "") && (pszGenre != "") && (pszComposer != ""))
            {
                MessageBox.Show("登录成功！", "");
                panel1.Visible = false;
              
            }
            else {
                MessageBox.Show("您有未输入的信息！", "");
            }


           
        }
        private void textbox_Click(object sender, EventArgs e)
        {
            pic1 = (uint)pictureBox9.Handle.ToInt32();
            string fileName = @"C:\test\Rdc.MP4";
            uint a1 = 0;
            uint a2 = 0;
            uint a3 = 0;
            uint a4 = 0;
            uint a5 = 0;
            double a6 = 0;
            uint a7 = 0;
            uint a8 = 0;
            uint a9 = 0;
            uint a10 = 0;
            double a11 = 0;
            uint a12 = 0;
            uint a13 = 0;
         

            EXPORTS.ResultOfFunction S;
            S = EXPORTS.QCAP_OPEN_FILE(ref fileName, ref a1, 0, ref a3, ref a4, ref a5, ref a6, ref a7, ref a8, ref a9, ref a10, ref a11, ref a12, ref a13, ref pic1, 0, 0);
            MessageBox.Show(S.ToString(), "");
            string ss1 = "";
            string ss2 = "";
            string ss3 = " ";
            string ss4 = " ";
            string ss5 = " ";
            S = EXPORTS.QCAP_GET_METADATA_FILE_HEADER(a1, ref ss1, ref ss2, ref ss3, ref ss4, ref ss5);
            MessageBox.Show(S.ToString(), "");

            textBox1.Text = a1 + "\r\n" + ss1 + "\r\n" + ss2 + "\r\n" + ss3 + "\r\n" + ss4 + "\r\n" + ss5;
        }

       

       





    }
}


