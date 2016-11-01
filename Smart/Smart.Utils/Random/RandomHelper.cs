using System;

namespace Smart.Utils
{
    /// <summary>
    /// 作者：Arvin
    /// 日期：2016/3/2 15:57:18
    /// 描述：随机数的生成
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 数字、字母列表
        /// </summary>
        private static char[] constant =   {   
            '0','1','2','3','4','5','6','7','8','9',  
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
          };
        /// <summary>
        /// 生成指定长度的随机数
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandomNumber( int Length )
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder( 62 );
            Random rd = new Random();
            for ( int i = 0 ; i < Length ; i++ )
            {
                newRandom.Append( constant[ rd.Next( 62 ) ] );
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 生成32位uuid，不带连字符
        /// </summary>
        /// <returns></returns>
        public static string GetUUID()
        {
            return Guid.NewGuid().ToString().Replace( "-", "" ).ToLower();
        }

        #region 生成一个指定范围的随机整数
        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetRandomInt( int minNum, int maxNum )
        {
            return new Random().Next( minNum, maxNum );
        }
        #endregion

        #region 生成一个0.0到1.0的随机小数
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public static double GetRandomDouble()
        {
            return new Random().NextDouble();
        }
        #endregion

        #region 对一个数组进行随机排序
        /// <summary>
        /// 对一个数组进行随机排序
        /// </summary>
        /// <typeparam name="T">数组的类型</typeparam>
        /// <param name="arr">需要随机排序的数组</param>
        public static void GetRandomArray<T>( T[ ] arr )
        {
            //对数组进行随机排序的算法:随机选择两个位置，将两个位置上的值交换

            //交换的次数,这里使用数组的长度作为交换次数
            int count = arr.Length;

            //开始交换
            for ( int i = 0 ; i < count ; i++ )
            {
                //生成两个随机数位置
                int randomNum1 = GetRandomInt(0, arr.Length);
                int randomNum2 = GetRandomInt(0, arr.Length);

                //定义临时变量
                T temp;

                //交换两个随机数位置的值
                temp = arr[ randomNum1 ];
                arr[ randomNum1 ] = arr[ randomNum2 ];
                arr[ randomNum2 ] = temp;
            }
        }
        #endregion

    }
}
