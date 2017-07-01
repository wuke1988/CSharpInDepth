using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12_MandelbrotSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //建立新的Image
            pcbMS.Image = new Bitmap(pcbMS.Width, pcbMS.Height);

            //计算各点的等级
            int[,] temp = PointSet(23);

            //生成新的Bitmap，并设置各点颜色
            Bitmap bmpTemp = new Bitmap(pcbMS.Width, pcbMS.Height);
            for (int i = 0; i < bmpTemp.Width; i++)
            {
                for (int j = 0; j < bmpTemp.Height; j++)
                {
                    bmpTemp.SetPixel(i, j, GetColor(temp[i, j]));
                }
            }

            //赋值到PictureBox的Image上
            pcbMS.Image = bmpTemp;
        }

        /// <summary>
        /// 根据迭代次数分配颜色
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Color GetColor(int i)
        {
            return i == 0 ? Color.White : Color.Black;
        }
        //复数结构
        public struct Complex
        {
            public double Real; //实部
            public double Imag; //虚部

            /// <summary>
            /// 建立一个复数 x+yi
            /// </summary>
            /// <param name="x">实部</param>
            /// <param name="y">虚部</param>
            public Complex(double x, double y)
            {
                Real = x;
                Imag = y;
            }

            /// <summary>
            /// 调整图形在窗口中的位置
            /// </summary>
            /// <param name="a">原实部位置</param>
            /// <param name="b">原虚部位置</param>
            /// <returns>调整后的位置</returns>
            public static Complex Converse(double a, double b)
            {
                return new Complex(a / 380.0 - 2.4, b / 280.0 - 1.25);
            }
        }
        /// <summary>
        /// 点集求值
        /// </summary>
        /// <param name="maxiter">最大迭代次数：Zn+1=Zn^2+C</param>
        /// <returns>求值后的集合</returns>
        int[,] PointSet(int maxiter = 7)
        {
            int[,] MSet = new int[pcbMS.Width, pcbMS.Height];

            var iter = 0;
            var current = new Complex(0.0, 0.0);
            var temp = new Complex(0.0, 0.0);

            for (int x = 0; x < pcbMS.Width; x++)
            {
                for (int y = 0; y < pcbMS.Height; y++)
                {
                    iter = 0;
                    current = Complex.Converse(x, y);
                    temp = current;
                    while (temp.Real * temp.Real + temp.Imag * temp.Imag <= 4 && iter < maxiter)
                    {
                        temp = new Complex(
                            temp.Real * temp.Real - temp.Imag * temp.Imag + current.Real,
                            2 * temp.Real * temp.Imag + current.Imag);
                        iter = iter + 1;
                    }

                    MSet[x, y] = iter == maxiter ? 1 : 0;
                }
            }

            return MSet;
        }
    }
}
