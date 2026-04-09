using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using 坡度基滤波;

namespace 坡度基滤波
{
    public partial class MainForm : Form
    {
        List<Point3D> pointCloud = new List<Point3D>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void bl_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Point Cloud (*.txt)|*.txt";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            pointCloud.Clear();

            foreach (var line in File.ReadAllLines(ofd.FileName))
            {
                var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3) continue;

                double x = double.Parse(parts[0]);
                double y = double.Parse(parts[1]);
                double z = double.Parse(parts[2]);

                pointCloud.Add(new Point3D(x, y, z));
            }

            ib.Text = $"点云加载完成：{pointCloud.Count} 个点";
        }

        private void bf_Click(object sender, EventArgs e)
        {
            double radius = 1.5;
            double slopeThreshold = 15.0; // ★ 坡度阈值（度）

            SlopeFilter.Apply(pointCloud, radius, slopeThreshold);

            int ground = pointCloud.Count(p => p.IsGround);
            ib.Text = $"滤波完成：地面点 {ground} / {pointCloud.Count}";
        }
        private void ddd_Click(object sender, EventArgs e)
        {
            if (pointCloud == null || pointCloud.Count == 0)
            {
                MessageBox.Show("请先加载点云并滤波！");
                return;
            }

            // 只取地面点
            var groundPoints = pointCloud.Where(p => p.IsGround).ToList();

            if (groundPoints.Count == 0)
            {
                MessageBox.Show("没有检测到地面点！");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "TXT 文件 (*.txt)|*.txt";
            sfd.FileName = "ground_points.txt";
            sfd.InitialDirectory = @"D:\";

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    foreach (var p in groundPoints)
                    {
                        sw.WriteLine($"{p.X} {p.Y} {p.Z}");
                    }
                }

                MessageBox.Show($"地面点已导出：{groundPoints.Count} 个点");
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败！\n" + ex.Message);
            }
        }

    }
}
