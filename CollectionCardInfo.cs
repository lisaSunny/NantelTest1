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
    public class CollectionCardInfo
    {        
  
        public uint[] m_hCapDev = new uint[2];          //创建一个设备流捕获数组，用于存放设备编号
        public uint[] m_hCloneCapDev = new uint[2];     // 克隆流捕获装置
        string m_strChipName = "QP0203 PCI";
        string[] metadata = new string[2];
        public uint i = 0;
        public uint m_hRtspCapDev = 0;                  // RTSP流捕获装置             
        public bool m_bShowClone = false;
    
        // FOURCC MARCO
        //
        uint MAKEFOURCC(uint ch0, uint ch1, uint ch2, uint ch3)
        {
            return ((uint)(byte)(ch0) | ((uint)(byte)(ch1) << 8) | ((uint)(byte)(ch2) << 16) | ((uint)(byte)(ch3) << 24));
        }

        //获取采集卡信息，对m_pDevice进行初始化
        public void getDevice(uint[] dhandle,PictureBox[] pictureBoxVideo) {

            for (i = 0; i < 2; i++)
            {
                m_hCapDev[i] = 0x00000000;
            }

            for (i = 0; i < 2;i++ )
            {
                m_hCloneCapDev[i] = 0x00000000;
            }

            //将视频显示在指定的picturebox上
            for (i = 0; i < 2; i++)
            {
                string str_chip_name = m_strChipName;
                EXPORTS.QCAP_CREATE(ref str_chip_name, i, dhandle[i], ref m_hCapDev[i], 1);
               
                //开启设备

                    EXPORTS.QCAP_SET_VIDEO_DEINTERLACE(m_hCapDev[i], 0);
                    EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[i], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
                    EXPORTS.QCAP_RUN(m_hCapDev[i]);
                    pictureBoxVideo[i].Visible = true;             //设置为可见  
                
               
            }

        }

        //开启音频
        //public void audioInput() { 

        //    if(m_hCapDev[0] != 0){
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[0],(uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[1] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[1], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[2] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[2], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[3] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[3], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[4] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[4], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[5] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[5], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[6] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[6], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
        //    if (m_hCapDev[7] != 0)
        //    {
        //        EXPORTS.QCAP_SET_AUDIO_INPUT(m_hCapDev[7], (uint)EXPORTS.InputAudioSourceEnum.QCAP_INPUT_TYPE_EMBEDDED_AUDIO);
        //    }
          
        //}

        //停止采集卡信息采集

        //

        //

        /// <summary>
        /// 关闭视频
        /// </summary>
        public void stopVideo() {

            EXPORTS.QCAP_DESTROY_BROADCAST_SERVER(m_hRtspCapDev);

            for (int i = 0; i < 2; i++)
            {
                if (m_hCloneCapDev[i] != 0)
                {
                    EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); m_hCloneCapDev[i] = 0;
                }
                if (m_hCapDev[i] != 0)
                {
                    EXPORTS.QCAP_STOP(m_hCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCapDev[i]);
                }
            }
            #region
            //if (m_hCloneCapDev[0] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[0]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[0]); }
            //if (m_hCloneCapDev[1] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[1]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[1]); }
            //if (m_hCloneCapDev[2] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[2]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[2]); }
            //if (m_hCloneCapDev[3] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[3]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[3]); }
            //if (m_hCloneCapDev[4] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[4]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[4]); }
            //if (m_hCloneCapDev[5] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[5]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[5]); }
            //if (m_hCloneCapDev[6] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[6]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[6]); }
            //if (m_hCloneCapDev[7] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[7]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[7]); }

            //if (m_hCapDev[0] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[0]); EXPORTS.QCAP_DESTROY(m_hCapDev[0]); }
            //if (m_hCapDev[1] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[1]); EXPORTS.QCAP_DESTROY(m_hCapDev[1]); }
            //if (m_hCapDev[2] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[2]); EXPORTS.QCAP_DESTROY(m_hCapDev[2]); }
            //if (m_hCapDev[3] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[3]); EXPORTS.QCAP_DESTROY(m_hCapDev[3]); }
            //if (m_hCapDev[4] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[4]); EXPORTS.QCAP_DESTROY(m_hCapDev[4]); }
            //if (m_hCapDev[5] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[5]); EXPORTS.QCAP_DESTROY(m_hCapDev[5]); }
            //if (m_hCapDev[6] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[6]); EXPORTS.QCAP_DESTROY(m_hCapDev[6]); }
            //if (m_hCapDev[7] != 0) { EXPORTS.QCAP_STOP(m_hCapDev[7]); EXPORTS.QCAP_DESTROY(m_hCapDev[7]); }
            #endregion
        }

        public void stopCloneVideo() {
            EXPORTS.QCAP_DESTROY_BROADCAST_SERVER(m_hRtspCapDev);

            for (int i = 0; i < 2; i++)
            {
                if (m_hCloneCapDev[i] != 0)
                {
                    EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); m_hCloneCapDev[i] = 0;
                }
              
            }
        }

   
        /// <summary>
        /// 显示控制台克隆的视频
        /// </summary>
        /// <param name="bShow">是否显示</param>
        /// <param name="picVideo">采集卡视图</param>
        /// <param name="conVideo">控制台视图</param>
        public void ShowControlCloneVideo(bool bShow, PictureBox[] picVideo, PictureBox[] conVideo)
        {
            if (bShow)
            {
                m_bShowClone = true;
                for (int i = 0; i < 2; i++)
                {
                    if (picVideo[i].Visible == true)
                    {
                        conVideo[i].Visible = true;

                        if (m_hCapDev[i] != 0)
                        {
                            EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[i], (uint)conVideo[i].Handle.ToInt32(), ref m_hCloneCapDev[i], 1);

                            if (m_hCloneCapDev[i] != 0)
                            {
                                EXPORTS.QCAP_RUN(m_hCloneCapDev[i]);

                                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[i], 0);
                            }
                        }
                    }
                }
                #region
                //if (picVideo[0].Visible == true && position == 0)
                //{
                //    conVideo[0].Visible = true;

                //    if (m_hCapDev[0] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[0], (uint)conVideo[0].Handle.ToInt32(), ref m_hCloneCapDev[0], 1);

                //        if (m_hCloneCapDev[0] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[0]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[0], 0);
                //        }
                //    }
                //}

                //if (picVideo[1].Visible == true)
                //{
                //    conVideo[1].Visible = true;

                //    if (m_hCapDev[1] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[1], (uint)conVideo[1].Handle.ToInt32(), ref m_hCloneCapDev[1], 1);

                //        if (m_hCloneCapDev[1] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[1]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[1], 0);
                //        }
                //    }
                //}

                //if (picVideo[2].Visible == true && position == 2)
                //{
                //    conVideo[2].Visible = true;

                //    if (m_hCapDev[2] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[2], (uint)conVideo[2].Handle.ToInt32(), ref m_hCloneCapDev[2], 1);

                //        if (m_hCloneCapDev[2] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[2]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[2], 0);
                //        }
                //    }
                //}

                //if (picVideo[3].Visible == true && position == 3)
                //{
                //    conVideo[3].Visible = true;

                //    if (m_hCapDev[3] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[3], (uint)conVideo[3].Handle.ToInt32(), ref m_hCloneCapDev[3], 1);

                //        if (m_hCloneCapDev[3] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[3]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[3], 0);
                //        }
                //    }
                //}

                //if (picVideo[4].Visible == true && position == 4)
                //{
                //    conVideo[4].Visible = true;

                //    if (m_hCapDev[4] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[4], (uint)conVideo[4].Handle.ToInt32(), ref m_hCloneCapDev[4], 1);

                //        if (m_hCloneCapDev[4] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[4]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[4], 0);
                //        }
                //    }
                //}

                //if (picVideo[5].Visible == true && position == 5)
                //{
                //    conVideo[5].Visible = true;

                //    if (m_hCapDev[5] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[5], (uint)conVideo[5].Handle.ToInt32(), ref m_hCloneCapDev[5], 1);

                //        if (m_hCloneCapDev[5] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[5]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[5], 0);
                //        }
                //    }
                //}

                //if (picVideo[6].Visible == true && position == 6)
                //{
                //    conVideo[6].Visible = true;

                //    if (m_hCapDev[6] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[6], (uint)conVideo[6].Handle.ToInt32(), ref m_hCloneCapDev[6], 1);

                //        if (m_hCloneCapDev[6] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[6]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[6], 0);
                //        }
                //    }
                //}

                //if (picVideo[7].Visible == true && position == 7)
                //{
                //    conVideo[7].Visible = true;

                //    if (m_hCapDev[7] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[7], (uint)conVideo[7].Handle.ToInt32(), ref m_hCloneCapDev[7], 1);

                //        if (m_hCloneCapDev[7] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[7]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[7], 0);
                //        }
                //    }
                //}
                #endregion
            }
            else
            {
                m_bShowClone = false;
                for (int i = 0; i < 2; i++)
                {
                    if (picVideo[i].Visible == true)
                    {
                        conVideo[i].Visible = false;

                        if (m_hCloneCapDev[i] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); m_hCloneCapDev[i] = 0; }
                    }
                }
            }

           
        }

 

        //private void controlVideo(int position) {
 
        //}

        //显示用户克隆的视频(克隆控制台的视频) ---------------------controlCloneVideo待确定，应该是控制台布局调整以后的视频布局显示（用户布局应该与控制台布局一致）
        public void ShowUserCloneVideo(bool bShow, PictureBox[] controlCloneVideo, PictureBox[] userCloneVideo)
        {

            if (bShow)
            {
                m_bShowClone = true;
                for (i = 0; i < 2; i++)
                {
                    if (controlCloneVideo[i].Height > 1 && controlCloneVideo[i].Width > 1)
                    {
                        userCloneVideo[i].Visible = true;

                        if (m_hCapDev[i] != 0)
                        {
                            EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[i], (uint)userCloneVideo[i].Handle.ToInt32(), ref m_hCloneCapDev[i], 1);

                            if (m_hCloneCapDev[i] != 0)
                            {
                                EXPORTS.QCAP_RUN(m_hCloneCapDev[i]);

                                EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[i], 0);
                                //string sl="123", s2="123", s3="123", s4= "123",s5 = "123";
                               // EXPORTS.QCAP_GET_METADATA_FILE_HEADER(i,ref s1, ref s2, ref s3, ref s4, ref s5);
                               // EXPORTS.QCAP_GET_METADATA_FILE_HEADER(uint pFile, ref string ppszTitle, ref string ppszArtist, ref string ppszComments, ref string ppszGenre, ref string ppszComposer);
                            }
                        }
                    }

                }
            }
            //if (bShow)
            //{
            //    m_bShowClone = true;
            //    for (int i = 0; i < 8; i++)
            //    {
            //        if (controlCloneVideo[i].Visible == true)
            //        {
            //            userCloneVideo[i].Visible = true;

            //            if (m_hCapDev[i] != 0)
            //            {
            //                EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[i], (uint)userCloneVideo[i].Handle.ToInt32(), ref m_hCloneCapDev[i], 1);

            //                if (m_hCloneCapDev[i] != 0)
            //                {
            //                    EXPORTS.QCAP_RUN(m_hCloneCapDev[i]);

            //                    EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[i], 0);
            //                }
            //            }
            //        }

            //    }
           // }
            else
            {
                m_bShowClone = false;
                for (int i = 0; i < 2; i++)
                {
                    if (controlCloneVideo[i].Visible == true)
                    {
                        userCloneVideo[i].Visible = false;

                        if (m_hCloneCapDev[i] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[i]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[i]); m_hCloneCapDev[i] = 0; }
                    }
                                      
                }

              

            }
            #region
            //if (controlCloneVideo[0].Visible == true)
                //{
                //    userCloneVideo[0].Visible = true;

                //    if (m_hCapDev[0] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[0], (uint)userCloneVideo[0].Handle.ToInt32(), ref m_hCloneCapDev[0], 1);

                //        if (m_hCloneCapDev[0] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[0]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[0], 0);
                //        }
                //    }
                //}
              
                //if (controlCloneVideo[1].Visible == true)
                //{
                //    userCloneVideo[1].Visible = true;

                //    if (m_hCapDev[1] != 0)
                //    {
                //        EXPORTS.QCAP_CREATE_CLONE(m_hCapDev[1], (uint)userCloneVideo[1].Handle.ToInt32(), ref m_hCloneCapDev[1], 1);

                //        if (m_hCloneCapDev[1] != 0)
                //        {
                //            EXPORTS.QCAP_RUN(m_hCloneCapDev[1]);

                //            EXPORTS.QCAP_SET_AUDIO_VOLUME(m_hCloneCapDev[1], 0);
                //        }
                //    }               

                //}


            //    else
            //    {
            //        m_bShowClone = false;

            //        if (controlCloneVideo[0].Visible == true)
            //        {
            //            userCloneVideo[0].Visible = false;

            //            if (m_hCloneCapDev[0] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[0]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[0]); m_hCloneCapDev[0] = 0; }
            //        }

            //        if (controlCloneVideo[1].Visible == true)
            //        {
            //            userCloneVideo[1].Visible = false;

            //            if (m_hCloneCapDev[1] != 0) { EXPORTS.QCAP_STOP(m_hCloneCapDev[1]); EXPORTS.QCAP_DESTROY(m_hCloneCapDev[1]); m_hCloneCapDev[1] = 0; }
            //        }


            //    }
            //}
            #endregion
        }

        /// <summary>
        /// 截屏(以时间命名)
        /// </summary>
        public void snapShot() {
           
            string time = DateTime.Now.ToLocalTime().ToString("yyyymmddss") + ".BMP";
            string video1 = @"C:\workspace\" + time;
            string video2 = @"C:\workspace\" + time;

            for (int i = 0; i < 2; i++)
            {
                if(m_hCapDev[i]!=0){
                    EXPORTS.QCAP_SNAPSHOT_BMP(m_hCapDev[i], ref video1);
                }
                
            }
           // EXPORTS.QCAP_SNAPSHOT_BMP(m_hCapDev[0], ref video1);
          //  EXPORTS.QCAP_SNAPSHOT_BMP(m_hCapDev[1], ref video2);
            // MessageBox.Show("截图成功"+time);
          
        }

        private void uerInputDlg() { 
            
        }     
        public uint a1 = 0;
        /// <summary>
        /// 开始录像
        /// </summary>
        public void startRecord(uint pic, string pszTitle, string pszArtist, string pszComments, string pszGenre, string pszComposer)
        {
            string time = DateTime.Now.ToLocalTime().ToString() + ".MP4";
            string s = pszTitle +".MP4";
            string path = @"C:\test\" + time;
            string path1 = @"C:\workspace\" + time;
            string path2 = @"C:\test\Rddc.MP4";
            string path3 = @"C:\test\" + s;
 
            for (i = 0; i < 2; i++)
            {
                if (m_hCapDev[i] != 0)
                {
                    EXPORTS.QCAP_SET_VIDEO_RECORD_PROPERTY(m_hCapDev[i], i, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12 * 1024 * 1024, 30, 4, 3, (uint)EXPORTS.DownScaleModeEnum.QCAP_DOWNSCALE_MODE_OFF);
                    EXPORTS.QCAP_START_RECORD(m_hCapDev[i], i, ref path3);

                    Encoding ec = Encoding.GetEncoding("utf-8");

                    Byte[] bpszTitle = ec.GetBytes(pszTitle);
                    Byte[] bpszArtist = ec.GetBytes(pszArtist);
                    Byte[] bpszComments = ec.GetBytes(pszComments);
                    Byte[] bpszGenre = ec.GetBytes(pszGenre);
                    Byte[] bpszComposer = ec.GetBytes(pszComposer);
                    Encoding e = Encoding.GetEncoding("GBK");

                    string spszTitle = e.GetString(bpszTitle);
                    string spszArtist = e.GetString(bpszArtist);
                    string spszComments = e.GetString(bpszComments);
                    string spszGenre = e.GetString(bpszGenre);
                    string spszComposer = e.GetString(bpszComposer);
               
                  //  EXPORTS.QCAP_SET_METADATA_RECORD_HEADER(m_hCapDev[i], i, ref s1, ref s2, ref s3, ref s4, ref s5);



                    EXPORTS.QCAP_SET_METADATA_RECORD_HEADER(m_hCapDev[i], i, ref spszTitle, ref spszArtist, ref spszComments, ref spszGenre, ref spszComposer);
                    //MessageBox.Show(S,"测试2");
                }

            }
        }

        /// <summary>
        /// 停止录像
        /// </summary>
        public void stopRecord() {
            for (i = 0; i < 8; i++)
            {
                 if(m_hCapDev[i]!=0){
                     EXPORTS.QCAP_STOP_RECORD(m_hCapDev[i], i);
                 }
            }
            
           // if(m_hCapDev[0]!=0){
           //     EXPORTS.QCAP_STOP_RECORD(m_hCapDev[0], 0);
           // }
           //if(m_hCapDev[1] != 0){
           //    EXPORTS.QCAP_STOP_RECORD(m_hCapDev[1], 0);
           //}
            
        }

        //分享录像
        public void shareRecordVideo(PictureBox shareRecordWindow)
        {
       
            string time1 = DateTime.Now.ToLocalTime().ToString("yyyymmddmmss") + ".MP4";
            string path1 = @"C:\workspace\" + time1;
            
            //EXPORTS.QCAP_SET_VIDEO_SHARE_RECORD_PROPERTY(0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_INTEL_MEDIA_SDK, (uint)EXPORTS.VideoEncoderFormatEnum .QCAP_ENCODER_FORMAT_H264, (uint)EXPORTS.ColorSpaceTypeEnum.QCAP_COLORSPACE_TYEP_YUY2,
            //1280, 720, 60, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12 * 1024 * 1024, 30, 4, 3, (uint)shareRecordWindow.Handle.ToInt32(), 1);
            EXPORTS.QCAP_SET_AUDIO_SHARE_RECORD_PROPERTY(0,(uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE,(uint)EXPORTS.AudioEncoderFormatEnum.QCAP_ENCODER_FORMAT_AAC,2,16,48000);

            EXPORTS.QCAP_SET_VIDEO_SHARE_RECORD_PROPERTY(0, (uint)EXPORTS.EncoderTypeEnum.QCAP_ENCODER_TYPE_SOFTWARE, (uint)EXPORTS.VideoEncoderFormatEnum.QCAP_ENCODER_FORMAT_H264, MAKEFOURCC('Y','U','Y','2'),
            1280, 720, 60, (uint)EXPORTS.RecordModeEnum.QCAP_RECORD_MODE_CBR, 8000, 12 * 1024 * 1024, 30, 4, 3, (uint)shareRecordWindow.Handle.ToInt32(), 1);
           
            EXPORTS.QCAP_START_SHARE_RECORD(0,ref path1);
        }

        //停止分享录像
        private void stopShareRecord() {
            EXPORTS.QCAP_STOP_SHARE_RECORD(0);
        }

      
        public void fullScreen(PictureBox[] controlCloneVideo)
        {
                //定义一个点，用于获取控件的坐标
                Point n = new Point();
                for (int j = 0; j < 8; j++)
                {
                    //定义一个点，位于控制台第一个控件中心
                    Point p = new Point(); 
                    p.X = 100;
                    p.Y = 100;
                    n.X = controlCloneVideo[j].Left;
                    n.Y = controlCloneVideo[j].Top;
                  //  n = controlCloneVideo[j].PointToScreen(new Point(0, 0));
                    if (((n.X <= p.X) && (p.X <= controlCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= controlCloneVideo[j].Height + n.Y)))
                    {
                       
                        controlCloneVideo[j].Width = 447;
                        controlCloneVideo[j].Height = 428;
                    }
                    else
                    {
                       controlCloneVideo[j].Height = -1;
                       controlCloneVideo[j].Width = -1;
                       
                    }
                   
                }
                

        }

        //用户全屏
        public void userFullScreen(PictureBox[] userCloneVideo)
        {
            
             
                Point n = new Point();
                for (int j = 0; j < 8; j++)
                {
                    //定义一个点，位于用户第一个控件中心
                    Point p = new Point();
                    p.X = 900;
                    p.Y = 100;
                    n.X = userCloneVideo[j].Left;
                    n.Y = userCloneVideo[j].Top;
                    //  n = controlCloneVideo[j].PointToScreen(new Point(0, 0));
                    if (((n.X <= p.X) && (p.X <= userCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= userCloneVideo[j].Height + n.Y)))
                    {

                        userCloneVideo[j].Width = 447;
                        userCloneVideo[j].Height = 428;
                    }
                    else
                    {
                        userCloneVideo[j].Height = -1;
                        userCloneVideo[j].Width = -1;

                    }

                }
           
      }

        //画中画
        public void pop(PictureBox[] controlCloneVideo)
        {
            Point n = new Point();
            for (int j = 0; j < 8; j++)
            {
                Point p = new Point();
                p.X = 100;
                p.Y = 100;
                //定义一个点，位于控制台第二个控件中心
                Point u = new Point();
                u.X = 350;
                u.Y = 100;
                n.X = controlCloneVideo[j].Left;
                n.Y = controlCloneVideo[j].Top;
                //  n = controlCloneVideo[j].PointToScreen(new Point(0, 0));
                if (((n.X <= p.X) && (p.X <= controlCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= controlCloneVideo[j].Height + n.Y)))
                {
                    controlCloneVideo[j].Width = 447;
                    controlCloneVideo[j].Height = 428;
                }else if(((n.X <= u.X)&&(u.X <= (n.X + controlCloneVideo[j].Width)))&&((n.Y <= u.Y)&&(u.Y<= (n.Y +controlCloneVideo[j].Height)))){
                    controlCloneVideo[j].Width = 221;
                    controlCloneVideo[j].Height = 206;
                    controlCloneVideo[j].BringToFront();
                }
                else
                {
                    controlCloneVideo[j].Height = -1;
                    controlCloneVideo[j].Width = -1;

                }

            }
        }

        //用户画中画
        public void userPop( PictureBox[] userCloneVideo)
        {
            Point n = new Point();
            for (int j = 0; j < 8; j++)
            {
                Point p = new Point();
                p.X = 900;
                p.Y = 100;
                //定义一个点，位于用户第二个控件中心
                Point u = new Point();
                u.X = 1100;
                u.Y = 100;
                n.X = userCloneVideo[j].Left;
                n.Y = userCloneVideo[j].Top;
                //  n = controlCloneVideo[j].PointToScreen(new Point(0, 0));
                if (((n.X <= p.X) && (p.X <= userCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= userCloneVideo[j].Height + n.Y)))
                {
                    userCloneVideo[j].Width = 447;
                    userCloneVideo[j].Height = 428;
                }
                else if (((n.X <= u.X) && (u.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u.Y) && (u.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 206;
                    userCloneVideo[j].BringToFront();
                }
                else
                {
                    userCloneVideo[j].Height = -1;
                    userCloneVideo[j].Width = -1;

                }

            }
        }
      
        //显示三个
        public void threeShow(PictureBox[] controlCloneVideo) {
             Point n = new Point();
             for (int j = 0; j < 8; j++)
             {
                 Point p = new Point();
                 p.X = 100;
                 p.Y = 100;
                 Point u = new Point();
                 u.X = 350;
                 u.Y = 100;
                 //定义一个点，位于控制台第三个控件中心
                 Point c3 = new Point();
                 c3.X = 100;
                 c3.Y = 330;
                 n.X = controlCloneVideo[j].Left;
                 n.Y = controlCloneVideo[j].Top;
                 //定义一个点，位于控制台第四个控件中心                  
                 if (((n.X <= p.X) && (p.X <= controlCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= controlCloneVideo[j].Height + n.Y)))
                 {
                     controlCloneVideo[j].Width = 226;
                     controlCloneVideo[j].Height = 428;
                 }
                 else if (((n.X <= u.X) && (u.X <= (n.X + controlCloneVideo[j].Width))) && ((n.Y <= u.Y) && (u.Y <= (n.Y + controlCloneVideo[j].Height))))
                 {
                     controlCloneVideo[j].Width = 221;
                     controlCloneVideo[j].Height = 206;
                     //controlCloneVideo[j].BringToFront();
                 }
                 else if (((n.X <= c3.X) && (c3.X <= (n.X + controlCloneVideo[j].Width))) && ((n.Y <= c3.Y) && (c3.Y <= (n.Y + controlCloneVideo[j].Height))))
                 {                    
                     controlCloneVideo[j].Width = 221;
                     controlCloneVideo[j].Height = 214;
                     controlCloneVideo[j].Left = 239;
                     controlCloneVideo[j].Top = 226;
       
                 }               
                 else
                 {
                     controlCloneVideo[j].Height = -1;
                     controlCloneVideo[j].Width = -1;

                 }
             }
        }

        //用户显示三个
        public void userThreeShow(PictureBox[] userCloneVideo)
        {
            Point n = new Point();
            for (int j = 0; j < 8; j++)
            {
                Point p = new Point();
                p.X = 900;
                p.Y = 100;
                //定义一个点，位于用户第二个控件中心
                Point u = new Point();
                u.X = 1100;
                u.Y = 100;
              
                //定义一个点，位于控制台第三个控件中心
                Point u3 = new Point();
                u3.X = 1100;
                u3.Y = 330;
                n.X = userCloneVideo[j].Left;
                n.Y = userCloneVideo[j].Top;
                //定义一个点，位于控制台第四个控件中心                  
                if (((n.X <= p.X) && (p.X <= userCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= userCloneVideo[j].Height + n.Y)))
                {
                    userCloneVideo[j].Width = 226;
                    userCloneVideo[j].Height = 428;
                }
                else if (((n.X <= u.X) && (u.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u.Y) && (u.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 206;
                    //controlCloneVideo[j].BringToFront();
                }
                else if (((n.X <= u3.X) && (u3.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u3.Y) && (u3.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 214;
                    userCloneVideo[j].Left = 992;
                    userCloneVideo[j].Top = 226;

                }
                else
                {
                    userCloneVideo[j].Height = -1;
                    userCloneVideo[j].Width = -1;

                }
            }
        }


        //显示四个
        public void fourShow(PictureBox[] controlCloneVideo)
        {
            Point n = new Point();
            for (int j = 0; j < 8; j++)
            {
                Point p = new Point();
                p.X = 100;
                p.Y = 100;
                Point u = new Point();
                u.X = 350;
                u.Y = 100;
                //定义一个点，位于控制台第三个控件中心
                Point c3 = new Point();
                c3.X = 100;
                c3.Y = 330;
                n.X = controlCloneVideo[j].Left;
                n.Y = controlCloneVideo[j].Top;
                //定义一个点，位于控制台第四个控件中心
                Point c4 = new Point();
                c4.X = 330;
                c4.Y = 330;
                
                if (((n.X <= p.X) && (p.X <= controlCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= controlCloneVideo[j].Height + n.Y)))
                {
                    controlCloneVideo[j].Width = 221;
                    controlCloneVideo[j].Height = 206;
                }
                else if (((n.X <= u.X) && (u.X <= (n.X + controlCloneVideo[j].Width))) && ((n.Y <= u.Y) && (u.Y <= (n.Y + controlCloneVideo[j].Height))))
                {
                    controlCloneVideo[j].Width = 221;
                    controlCloneVideo[j].Height = 206;
                    //controlCloneVideo[j].BringToFront();
                }
                else if (((n.X <= c3.X) && (c3.X <= (n.X + controlCloneVideo[j].Width))) && ((n.Y <= c3.Y) && (c3.Y <= (n.Y + controlCloneVideo[j].Height))))
                {
                    controlCloneVideo[j].Width = 221;
                    controlCloneVideo[j].Height = 214;                   

                }
                else if (((n.X <= c4.X) && (c4.X <= (n.X + controlCloneVideo[j].Width))) && ((n.Y <= c4.Y) && (c4.Y <= (n.Y + controlCloneVideo[j].Height))))
                {
                    controlCloneVideo[j].Width = 221;
                    controlCloneVideo[j].Height = 214;           

                }
                else
                {
                    controlCloneVideo[j].Height = -1;
                    controlCloneVideo[j].Width = -1;

                }
            }
        }

        //用户显示四个
        public void userFourShow(PictureBox[] userCloneVideo)
        {
            Point n = new Point();
            for (int j = 0; j < 8; j++)
            {
                Point p = new Point();
                p.X = 900;
                p.Y = 100;
                //定义一个点，位于用户第二个控件中心
                Point u = new Point();
                u.X = 1100;
                u.Y = 100;

                //定义一个点，位于控制台第三个控件中心
                Point u3 = new Point();
                u3.X = 1100;
                u3.Y = 330;
                Point u4 = new Point();
                u4.X = 330;
                u4.Y = 330;
                n.X = userCloneVideo[j].Left;
                n.Y = userCloneVideo[j].Top;
                //定义一个点，位于控制台第四个控件中心                  
                if (((n.X <= p.X) && (p.X <= userCloneVideo[j].Width + n.X)) && ((n.Y <= p.Y) && (p.Y <= userCloneVideo[j].Height + n.Y)))
                {
                    userCloneVideo[j].Width = 226;
                    userCloneVideo[j].Height = 428;
                }
                else if (((n.X <= u.X) && (u.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u.Y) && (u.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 206;
                    //controlCloneVideo[j].BringToFront();
                }
                else if (((n.X <= u3.X) && (u3.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u3.Y) && (u3.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 214;
                    userCloneVideo[j].Left = 992;
                    userCloneVideo[j].Top = 226;

                }
                else if (((n.X <= u4.X) && (u4.X <= (n.X + userCloneVideo[j].Width))) && ((n.Y <= u4.Y) && (u4.Y <= (n.Y + userCloneVideo[j].Height))))
                {
                    userCloneVideo[j].Width = 221;
                    userCloneVideo[j].Height = 214;

                }
                else
                {
                    userCloneVideo[j].Height = -1;
                    userCloneVideo[j].Width = -1;

                }
            }
        }
        //重置
        public void reStart(PictureBox[] cloneVideo,PictureBox[] userClone)
        {
            cloneVideo[0].Left = 13;
            cloneVideo[0].Top = 12;
            cloneVideo[0].Width = 220;
            cloneVideo[0].Height = 206;
            cloneVideo[1].Left = 239;
            cloneVideo[1].Top = 12;
            cloneVideo[1].Width = 221;
            cloneVideo[1].Height = 206;
            cloneVideo[2].Left = 12;
            cloneVideo[2].Top = 224;
            cloneVideo[2].Width = 220;
            cloneVideo[2].Height = 216;
            cloneVideo[3].Left = 239;
            cloneVideo[3].Top = 224;
            cloneVideo[3].Width = 221;
            cloneVideo[3].Height = 216;
            cloneVideo[4].Height = -1;
            cloneVideo[4].Width = -1;
            cloneVideo[4].Width = 221;
            cloneVideo[4].Height = 216;
            cloneVideo[5].Height = -1;
            cloneVideo[5].Width = -1;
            cloneVideo[6].Height = -1;
            cloneVideo[6].Width = -1;
            cloneVideo[7].Height = -1;
            cloneVideo[7].Width = -1;

            userClone[0].Left = 766;
            userClone[0].Top = 12;
            userClone[0].Width = 220;
            userClone[0].Height = 206;
            userClone[1].Left = 992;
            userClone[1].Top = 12;
            userClone[1].Width = 219;
            userClone[1].Height = 206;
            userClone[2].Left = 766;
            userClone[2].Top = 224;
            userClone[2].Width = 220;
            userClone[2].Height = 216;
            userClone[3].Left = 992;
            userClone[3].Top = 224;
            userClone[3].Width = 219;
            userClone[3].Height = 216;
            userClone[4].Height = -1;
            userClone[4].Width = -1;
            userClone[4].Width = 221;
            userClone[4].Height = 216;
            userClone[5].Height = -1;
            userClone[5].Width = -1;
            userClone[6].Height = -1;
            userClone[6].Width = -1;
            userClone[7].Height = -1;
            userClone[7].Width = -1;

        }
    }
}
 