using System;  
using System.Collections.Generic;  
using System.Text;  
using System.IO;  
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using AlexPilotti.FTPS.Client;
using System.Globalization;



namespace Smart.Core.Utils
{
    /// <summary>
    ///  ftp链接协议枚举
    /// </summary>
    public enum FtpProtocolEnum
    {
        /// <summary>
        ///  普通ftp协议格式
        /// </summary>
        FTP = 1,
        /// <summary>
        ///  ftps，ssl通道的ftp
        /// </summary>
        FTPS = 2
    }


    /// <summary>
    ///  ftp处理帮助类（包括FTP协议和FTPS协议）
    ///  TODO：需再实现各个操作的异步版本
    ///  创  建  人：Arvin
    ///  创建日期：2016年10月31日17:02:42
    ///  更  新  人：张榕添
    ///  更新时间：2016-11-01
    ///  更新原因：新增下载所有文件(包括子文件)
    ///  更  新  人：Arvin
    ///  更新时间：2016-11-04
    ///  更新原因：判断远程服务器文件是否以给定的字符结尾（默认UTF-8编码）
    /// </summary>
    public class FtpHelper
    {

        #region 字段
        /// <summary>
        ///  ftp的URI
        /// </summary>
        private string ftpURI;
        /// <summary>
        ///  ftp的用户名
        /// </summary>
        private string ftpUserID;
        /// <summary>
        ///  ftp的IP地址
        /// </summary>
        private string ftpServerIP;
        /// <summary>
        ///  ftp对应用户名的密码
        /// </summary>
        private string ftpPassword;
        /// <summary>
        ///  请求的路径
        /// </summary>
        private string ftpRemotePath;
        /// <summary>
        ///  协议，ftp或ftps
        /// </summary>
        private FtpProtocolEnum ftpProtocol = FtpProtocolEnum.FTP;

        #endregion

        private AlexPilotti.FTPS.Client.FTPSClient _ftpsClient;

        public AlexPilotti.FTPS.Client.FTPSClient FtpsClient
        {
            get
            {
                return _ftpsClient;

            }
            private set { }

        }


        /// <summary>  
        /// 连接FTP服务器
        /// </summary>  
        /// <param name="FtpServerIP">FTP连接地址</param>  
        /// <param name="FtpRemotePath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>  
        /// <param name="FtpUserID">用户名</param>  
        /// <param name="FtpPassword">密码</param>  
        public FtpHelper( string FtpServerIP, string FtpRemotePath, string FtpUserID, string FtpPassword, FtpProtocolEnum FtpProtocol )
        {
            ftpServerIP = FtpServerIP;
            ftpRemotePath = FtpRemotePath;
            ftpUserID = FtpUserID;
            ftpPassword = FtpPassword;
            ftpProtocol = FtpProtocol;
            if ( FtpProtocolEnum.FTP == FtpProtocol )
            {
                //路径为空时，指定为默认的根目录
                if ( string.IsNullOrEmpty( ftpRemotePath ) )
                    ftpURI = "ftp://" + ftpServerIP + "/";// ftpProtocol.ToString() +
                else
                    ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";//ftpProtocol.ToString() +
            }
            if ( FtpProtocolEnum.FTPS == FtpProtocol )
            {
                AlexPilotti.FTPS.Client.FTPSClient fts = new AlexPilotti.FTPS.Client.FTPSClient();
                string[] Array = ftpServerIP.Split(':');
                string IpStr = Array[0];
                int PorStr = 990;
                if ( Array.Length >= 2 )
                {
                    PorStr = int.Parse( Array[ 1 ] );
                }
                fts.Connect(
                            IpStr,
                            PorStr,
                            new NetworkCredential( FtpUserID, FtpPassword ),
                            ESSLSupportMode.Implicit | ESSLSupportMode.DataChannelRequired,
                            new System.Net.Security.RemoteCertificateValidationCallback( ValidateServerCertificate ),
                            null, 0, 0, 0, 3000 );
                this._ftpsClient = fts;
            }
        }

        /// <summary>
        ///  获取FTP或FTPS协议格式的FtpWebRequest对象
        /// </summary>
        /// <returns></returns>
        private FtpWebRequest GetFtpWebRequest( Uri uri )
        {
            FtpWebRequest reqFtp = null;
            try
            {
                //根据URI创建FtpWebRequest
                reqFtp = ( FtpWebRequest )FtpWebRequest.Create( uri );
                //reqFtp = (FtpWebRequest)FtpWebRequest.Create(uri);
                //设置用户名和密码
                reqFtp.Credentials = new NetworkCredential( ftpUserID, ftpPassword );
                reqFtp.UsePassive = false;//被动模式
                ////如果为FTPS协议，需要执行支持安全套接字层
                //if (FtpProtocolEnum.FTPS == this.ftpProtocol)
                //{
                //    //如果要连接的 FTP 服务器要求凭据并支持安全套接字层 (SSL)，则应将 EnableSsl 设置为 true。
                //    //如果不写会报出421错误（服务不可用）
                //    reqFtp.EnableSsl = true;


                //    // 首次连接FTPS server时，会有一个证书分配过程。
                //    //如果没有下面的代码会报异常：
                //    //System.Security.Authentication.AuthenticationException: 根据验证过程，远程证书无效。
                //    //ServicePointManager.ServerCertificateValidationCallback =
                //    //   new RemoteCertificateValidationCallback(ValidateServerCertificate);
                //    ServicePointManager.ServerCertificateValidationCallback =
                //        new System.Net.Security.RemoteCertificateValidationCallback(ValidateServerCertificate);
                //}
            }
            catch ( Exception ex )
            {

            }
            return reqFtp;
        }

        /// <summary>
        ///  ssl验证委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate">X509Certificate</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="sslPolicyErrors">SslPolicyErrors</param>
        /// <returns></returns>
        private bool ValidateServerCertificate( object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors )
        {
            return true;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPathFileName">文件的完全限定名或相对文件名</param>
        /// <param name="remoteFileName">远程文件名</param>
        public void Upload( string localPathFileName, string remoteFileName )
        {

            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remoteFileName));
            //设置传输方法
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            //设置请求完成后关闭连接（true为不关闭连接）
            reqFTP.KeepAlive = false;
            //设置传输为二进制（false为文本）
            reqFTP.UseBinary = true;
            //reqFTP.ContentLength = fileInf.Length;
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            FileStream fs = null;
            Stream strm = null;

            try
            {
                //要上传的本地文件流
                fs = new FileInfo( localPathFileName ).OpenRead();
                //FTP的上传数据流
                strm = reqFTP.GetRequestStream();
                while ( ( contentLen = fs.Read( buff, 0, buffLength ) ) != 0 )
                {
                    strm.Write( buff, 0, contentLen );
                }
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
            finally
            {
                if ( fs != null )
                    fs.Close();
                if ( strm != null )
                    strm.Close();
            }

        }

        /// <summary>
        ///  下载文件
        /// </summary>
        /// <param name="localFilePath">要保存到的文件路径</param>
        /// <param name="localFileName">要保存到的文件名</param>
        /// <param name="remoteFileName">要获取的的文件名</param>
        public void Download( string localFilePath, string localFileName, string remoteFileName )
        {
            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remoteFileName));
            //设置传输方法-- 下载
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            //设置传输为二进制（false为文本）
            reqFTP.UseBinary = true;

            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            FileStream outPutStream = null;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                //本地文件流
                outPutStream = new FileStream( localFilePath + "\\" + localFileName, FileMode.Create );
                //获取服务器响应对象
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //获取服务器响应流
                ftpStream = response.GetResponseStream();
                //long cl = response.ContentLength;
                while ( ( readCount = ftpStream.Read( buffer, 0, bufferSize ) ) != 0 )
                {
                    outPutStream.Write( buffer, 0, readCount );
                }
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
            finally
            {
                if ( outPutStream != null )
                    outPutStream.Close();
                if ( response != null )
                    response.Close();
                if ( ftpStream != null )
                    ftpStream.Close();
            }

        }


        /// <summary>
        ///  判断远程服务器文件是否以给定的字符结尾（默认UTF-8编码）
        /// </summary>
        /// <param name="remoteFileName">要获取的的文件名</param>
        /// <param name="endStrs">结尾的字符数组</param>
        public bool IsEndByChar( string remoteFileName, params string[ ] endStrs )
        {
            bool isEndChar = false;
            //获取文件大小（字节）
            long fileSzie = GetFileSize(remoteFileName);
            //如果文件大小为0，则表示文件无内容
            if ( fileSzie <= 0 )
            {
                return isEndChar;
            }
            //默认从文件获取的字节数（小于3个字节，则去最后一个字节）
            long offsetCount = fileSzie - 3 < 0 ? fileSzie - 1 : fileSzie - 3;

            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remoteFileName));
            //设置传输方法-- 下载
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            //设置传输为二进制（false为文本）
            reqFTP.UseBinary = true;
            //由于全角和半角的存在，取utf-8的一个字符，3个字节获取

            reqFTP.ContentOffset = offsetCount;
            int bufferSize = 3;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                //获取服务器响应对象
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //获取服务器响应流
                ftpStream = response.GetResponseStream();
                ftpStream.Read( buffer, 0, bufferSize );
                // '\0' 是为空的ascii，byte数组初始化时，都为空
                string strContent = Encoding.UTF8.GetString(buffer).Trim('\0');
                //取最后一个字符
                string strEnd = strContent.Substring(strContent.Length - 1, 1);
                foreach ( string str in endStrs )
                {
                    if ( str.Equals( strEnd ) )
                    {
                        isEndChar = true;
                        break;
                    }
                }
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
            finally
            {
                if ( response != null )
                    response.Close();
                if ( ftpStream != null )
                    ftpStream.Close();
            }
            return isEndChar;

        }



        /// <summary>  
        /// 删除文件  
        /// </summary>  
        public void Delete( string remoteFileName )
        {
            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remoteFileName));
            //设置传输方法-- 删除文件
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
            //设置请求完成后关闭连接（true为不关闭连接）
            reqFTP.KeepAlive = false;
            FtpWebResponse response = null;
            try
            {
                //ftp服务器响应对象
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //response.GetResponseStream();
                //long size = response.ContentLength;
                //Stream datastream = response.GetResponseStream();
                //StreamReader sr = new StreamReader(datastream);
                //result = sr.ReadToEnd();
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
            finally
            {
                if ( response != null )
                    response.Close();
            }
        }


        /// <summary>
        ///  获取指定目录下明细(包含文件和文件夹)  
        ///  如果不在同一层级，需要先跳转到其它目录再获取
        /// </summary>
        /// <param name="remotePath">相对当前路径的path</param>
        /// <returns>远程FTP服务器上的明细列表（包括文件夹）</returns>
        public List<string> GetFilesDetailList( string remotePath )
        {
            List<string> fileNamesList = new List<string>();

            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remotePath));
            //设置传输方法-- 获取文件详细列表
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            string line = string.Empty;

            FtpWebResponse response = null;
            StreamReader reader = null;
            try
            {
                //获取Ftp响应对象
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //将响应的数据流转为StreamReader
                reader = new StreamReader( response.GetResponseStream() );
                while ( ( line = reader.ReadLine() ) != null )
                {
                    fileNamesList.Add( line );
                }
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
            finally
            {
                if ( reader != null )
                    reader.Close();
                if ( response != null )
                    response.Close();
            }
            return fileNamesList;
        }

        /// <summary>
        ///  获取FTP文件列表(包括文件夹)
        ///  如果不在同一层级，需要先跳转到其它目录再获取
        /// </summary>
        /// <param name="remotePath">相对当前路径的path</param>
        /// <returns>远程FTP服务器上的简短列表（包括文件夹）</returns>
        private List<string> GetAllList( string remotePath )
        {
            List<string> fileList = new List<string>();

            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remotePath));
            //设置传输方法-- 获取服务器上文件简短列表
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

            string line = string.Empty;

            FtpWebResponse res = null;
            StreamReader sr = null;
            try
            {
                //获取Ftp响应对象
                res = ( FtpWebResponse )reqFTP.GetResponse();
                //将响应的数据流转为StreamReader
                sr = new StreamReader( res.GetResponseStream() );
                while ( ( line = sr.ReadLine() ) != null )
                {
                    fileList.Add( line );
                }
            }
            catch ( Exception ex )
            {
                throw ( ex );
            }
            finally
            {
                if ( sr != null )
                    sr.Close();
                if ( res != null )
                    res.Close();
            }
            return fileList;
        }

        /// <summary>
        ///  获取目录下文件列表(不包括文件夹)  
        ///  如果不在同一层级，需要先跳转到其它目录再获取
        /// </summary>
        /// <param name="remotePath">相对当前路径的path</param>
        /// <returns>返回文件列表(不包括文件夹) </returns>
        public List<string> GetFileList( string remotePath )
        {
            List<string> filesList = new List<string>();
            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + remotePath));
            //设置传输方法-- 获取服务器上文件详细列表
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            FtpWebResponse response = null;
            StreamReader reader = null;
            string line = string.Empty;
            try
            {
                response = ( FtpWebResponse )reqFTP.GetResponse();
                reader = new StreamReader( response.GetResponseStream() );

                while ( ( line = reader.ReadLine() ) != null )
                {
                    if ( line.IndexOf( "<DIR>" ) == -1 )
                    {
                        filesList.Add( Regex.Match( line, @"[\S]+ [\S]+", RegexOptions.IgnoreCase ).Value.Split( ' ' )[ 1 ] );
                    }
                }

            }
            catch ( Exception ex )
            {
                throw ( ex );
            }
            finally
            {
                if ( reader != null )
                    reader.Close();
                if ( response != null )
                    response.Close();
            }
            return filesList;
        }

        /// <summary>  
        /// 判断当前目录下指定的文件是否存在  
        /// </summary>  
        /// <param name="remoteFileName">远程文件名</param>  
        public bool FileExist( string remoteFileName )
        {
            List<string> fileList = GetFileList("*.*");
            foreach ( string str in fileList )
            {
                if ( str.Trim() == remoteFileName.Trim() )
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 创建文件夹  
        /// </summary>
        /// <param name="dirName">当前目录的文件夹名称</param>
        public void MakeDir( string dirName )
        {
            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + dirName));
            //设置传输方法-- 获取服务器上创建目录
            reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;

            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //ftpStream = response.GetResponseStream();
            }
            catch ( Exception ex )
            { }
            finally
            {
                if ( response != null )
                    response.Close();
            }
        }

        /// <summary>
        ///  获取当前目录指定文件大小  
        /// </summary>
        /// <param name="filename">当前目录的文件名</param>
        /// <returns>返回文件大小（字节）</returns>
        public long GetFileSize( string filename )
        {
            long fileSize = 0;

            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + filename));
            //设置传输方法-- 获取文件大小
            reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
            FtpWebResponse response = null;
            Stream ftpStream = null;
            try
            {
                response = ( FtpWebResponse )reqFTP.GetResponse();
                ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;
            }
            catch ( Exception ex )
            { }
            finally
            {
                if ( ftpStream != null )
                    ftpStream.Close();
                if ( response != null )
                    response.Close();
            }
            return fileSize;
        }

        /// <summary>
        ///  更改当前目录下指定的文件名  
        /// </summary>
        /// <param name="currentFilename">旧文件名</param>
        /// <param name="newFilename">新文件名</param>
        public void ReName( string oldFilename, string newFilename )
        {
            //获取FTP或FTPS协议格式的FtpWebRequest对象
            FtpWebRequest reqFTP = GetFtpWebRequest(new Uri(ftpURI + oldFilename));
            //设置传输方法-- 重命名
            reqFTP.Method = WebRequestMethods.Ftp.Rename;
            reqFTP.RenameTo = newFilename;
            reqFTP.UseBinary = true;
            FtpWebResponse response = null;
            try
            {
                response = ( FtpWebResponse )reqFTP.GetResponse();
                //Stream ftpStream = response.GetResponseStream();
            }
            catch ( Exception ex )
            { }
            finally
            {
                if ( response != null )
                    response.Close();
            }
        }

        /// <summary>  
        /// 移动文件  
        /// </summary>  
        //public void MovieFile(string currentFilename, string newDirectory)
        //{
        //    ReName(currentFilename, newDirectory);
        //}

        /// <summary>  
        /// 切换当前目录  
        /// </summary>  
        /// <param name="IsRoot">true:绝对路径 false:相对路径</param>   
        public void GotoDirectory( string DirectoryName, bool IsRoot )
        {
            ftpRemotePath = IsRoot ? DirectoryName : ftpRemotePath + DirectoryName + "/";
            ftpURI = ftpProtocol.ToString() + "://" + ftpServerIP + "/" + ftpRemotePath + "/";
        }



        public List<string> filePath = new List<string>();
        /// <summary>
        /// 下载ftp上所有文件夹下面的所有文件，不限制文件类型
        /// </summary>
        /// <param name="localFilePath">保存在本地的位置</param>
        public bool DownFtpByAllDirAndFile( string localFilePath )
        {
            bool isOK = false;
            if ( !string.IsNullOrEmpty( localFilePath ) )
            {
                if ( !Directory.Exists( localFilePath ) )
                {
                    Directory.CreateDirectory( localFilePath );
                }

                string Path = "";
                GetAllDirAndFile( Path );//获取所有文件数据,第一次进入Path不需要赋值
                if ( filePath.Count > 0 )
                {
                    foreach ( string listPath in filePath )
                    {
                        string Name = listPath.Replace("/", @"\").Trim();

                        if ( Name.Contains( @"\" ) && !Directory.Exists( localFilePath + "\\" + Name.Substring( 0, Name.LastIndexOf( @"\" ) ) ) ) //判断是否有目录和是否存在
                            Directory.CreateDirectory( localFilePath + "\\" + Name.Substring( 0, Name.LastIndexOf( @"\" ) ) );
                        try
                        {
                            Download( localFilePath, Name, Name );
                            isOK = true;
                        }
                        catch
                        {
                            continue;
                        }

                    }
                }
            }
            return isOK;

        }
        /// <summary>
        /// 判断有文件夹<DIR>的时候讲进行不停的进入子目录,没有文件夹时停止返回，
        /// 并将每个文件夹内的文件放在List中，用于提供下载
        /// </summary>
        /// <param name="Path">有子目录的时候将不停的循环子目录传回来</param>
        /// <returns></returns>
        public string GetAllDirAndFile( string Path )
        {
            List<string> FilesDetailList = GetFilesDetailList(Path);

            // Add By Arvin 2016年11月18日19:11:00，根据数据格式来判断是Unix格式还是Windows格式
            FileListStyle fls = GuessFileListStyle(FilesDetailList.ToArray());
            foreach ( string str in FilesDetailList )
            {
                // Add By Arvin 2016年11月18日19:11:00，根据数据格式来判断是Unix格式还是Windows格式
                //统计行的话 跳过
                if ( str.IndexOf( "total" ) == 0 )
                    continue;
                FileStruct fs = new FileStruct();
                if ( FileListStyle.UnixStyle == fls )
                {
                    fs = ParseFileStructFromUnixStyleRecord( str );
                }
                else if ( FileListStyle.WindowsStyle == fls )
                {
                    fs = ParseFileStructFromWindowsStyleRecord( str );
                }

                if ( ".".Equals( fs.Name ) || "..".Equals( fs.Name ) )
                    continue;

                if ( fs.IsDirectory )//文件夹
                {
                    if ( !string.IsNullOrEmpty( Path ) ) //第一次进来已经存在"/"
                        GetAllDirAndFile( Path + "/" + fs.Name.ToString().Trim() + "/" );
                    else
                        GetAllDirAndFile( "/" + fs.Name.ToString().Trim() + "/" );
                }
                else
                {
                    //文件
                    if ( !string.IsNullOrEmpty( Path ) )//第一次进来已经存在"/"
                        filePath.Add( Path + "/" + fs.Name.Trim() );
                    else
                        filePath.Add( "/" + fs.Name.Trim() );
                }
            }
            return "";

        }

        #region Unix与Windows处理

        /// <summary>
        /// 从Unix格式中返回文件信息
        /// </summary>
        /// <param name="Record">文件信息</param>
        private FileStruct ParseFileStructFromUnixStyleRecord( string Record )
        {
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();
            f.Flags = processstr.Substring( 0, 10 );
            f.IsDirectory = ( f.Flags[ 0 ] == 'd' );
            processstr = ( processstr.Substring( 11 ) ).Trim();
            _cutSubstringFromStringWithTrim( ref processstr, ' ', 0 );   //跳过一部分
            f.Owner = _cutSubstringFromStringWithTrim( ref processstr, ' ', 0 );
            f.Group = _cutSubstringFromStringWithTrim( ref processstr, ' ', 0 );
            _cutSubstringFromStringWithTrim( ref processstr, ' ', 0 );   //跳过一部分
            string yearOrTime = processstr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[2];
            if ( yearOrTime.IndexOf( ":" ) >= 0 )  //time
            {
                processstr = processstr.Replace( yearOrTime, DateTime.Now.Year.ToString() );
            }
            f.CreateTime = DateTime.Parse( _cutSubstringFromStringWithTrim( ref processstr, ' ', 8 ) );
            f.Name = processstr;   //最后就是名称
            return f;
        }


        /// <summary>
        /// 从Windows格式中返回文件信息
        /// </summary>
        /// <param name="Record">文件信息</param>
        private FileStruct ParseFileStructFromWindowsStyleRecord( string Record )
        {
            FileStruct f = new FileStruct();
            string processstr = Record.Trim();
            string dateStr = processstr.Substring(0, 8);
            processstr = ( processstr.Substring( 8, processstr.Length - 8 ) ).Trim();
            string timeStr = processstr.Substring(0, 7);
            processstr = ( processstr.Substring( 7, processstr.Length - 7 ) ).Trim();
            DateTimeFormatInfo myDTFI = new CultureInfo("en-US", false).DateTimeFormat;
            myDTFI.ShortTimePattern = "t";
            f.CreateTime = DateTime.Parse( dateStr + " " + timeStr, myDTFI );
            if ( processstr.Substring( 0, 5 ) == "<DIR>" )
            {
                f.IsDirectory = true;
                processstr = ( processstr.Substring( 5, processstr.Length - 5 ) ).Trim();
            }
            else
            {
                string[] strs = processstr.Split(new char[] { ' ' }, 2);// StringSplitOptions.RemoveEmptyEntries);   // true);
                processstr = strs[ 1 ];
                f.IsDirectory = false;
            }
            f.Name = processstr;
            return f;
        }

        /// <summary>
        /// 判断文件列表的方式Window方式还是Unix方式
        /// </summary>
        /// <param name="recordList">文件信息列表</param>
        private FileListStyle GuessFileListStyle( string[ ] recordList )
        {
            foreach ( string s in recordList )
            {
                if ( s.Length > 10
                 && Regex.IsMatch( s.Substring( 0, 10 ), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)" ) )
                {
                    return FileListStyle.UnixStyle;
                }
                else if ( s.Length > 8
                 && Regex.IsMatch( s.Substring( 0, 8 ), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]" ) )
                {
                    return FileListStyle.WindowsStyle;
                }
            }
            return FileListStyle.Unknown;
        }

        /// <summary>
        /// 按照一定的规则进行字符串截取
        /// </summary>
        /// <param name="s">截取的字符串</param>
        /// <param name="c">查找的字符</param>
        /// <param name="startIndex">查找的位置</param>
        private string _cutSubstringFromStringWithTrim( ref string s, char c, int startIndex )
        {
            int pos1 = s.IndexOf(c, startIndex);
            string retString = s.Substring(0, pos1);
            s = ( s.Substring( pos1 ) ).Trim();
            return retString;
        }

        #endregion
    }


    #region 文件信息结构
    public struct FileStruct
    {
        public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime CreateTime;
        public string Name;
    }

    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }

    #endregion
}