using System;

namespace FileClient
{
    public delegate void SendFileDataEventHandler(object sender, SendFileDataEventArgs e);
    public class SendFileDataEventArgs : EventArgs
    {
        public string FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// 남은 파일 데이터 크기
        /// </summary>
        public long Remain
        {
            get;
            private set;
        }

        public SendFileDataEventArgs(string fname, long remain)
        {
            FileName = fname;
            Remain = remain;
        }
    }
}
